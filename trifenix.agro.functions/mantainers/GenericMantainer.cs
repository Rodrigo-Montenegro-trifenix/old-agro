﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using trifenix.agro.db;
using trifenix.agro.enums.input;
using trifenix.agro.external.interfaces;
using trifenix.agro.functions.Helper;
using trifenix.agro.model.external;
using trifenix.agro.model.external.Input;
using trifenix.agro.servicebus.operations;

namespace trifenix.agro.functions.mantainers {

    public static class GenericMantainer {

        private static readonly ServiceBus ServiceBus = new ServiceBus(Environment.GetEnvironmentVariable("ServiceBusConnectionString", EnvironmentVariableTarget.Process), Environment.GetEnvironmentVariable("QueueName", EnvironmentVariableTarget.Process));

        public static async Task<ActionResultWithId> SendInternalHttp<DbElement, InputElement>(HttpRequest req, ILogger log, Func<IAgroManager, IGenericOperation<DbElement, InputElement>> repo, string id = null) where DbElement : DocumentBase where InputElement : InputBase {
            var claims = await Auth.Validate(req);
            if (claims == null)
                return new ActionResultWithId {
                    Id = null,
                    JsonResult = new UnauthorizedResult()
                };
            string ObjectIdAAD = claims.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            return await HttpProcessing(req, log, ObjectIdAAD, repo, id);
        }

        public static async Task<ActionResultWithId> HttpProcessing<DbElement, InputElement>(HttpRequest req, ILogger log, string ObjectIdAAD, Func<IAgroManager, IGenericOperation<DbElement, InputElement>> repo, string id = null) where DbElement : DocumentBase where InputElement : InputBase {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var method = req.Method.ToLower();
            var inputElement = ConvertToElement<InputElement>(body, id, method);
            return await HttpProcessing(req, log, ObjectIdAAD, repo, inputElement);
        }

        public static InputElement ConvertToElement<InputElement>(string body, string id, string method) where InputElement : InputBase {
            var requestBody = body;
            InputElement element;
            if (method.Equals("get"))
                element = (InputElement)Activator.CreateInstance(typeof(InputElement));
            else
                try {
                    element = JsonConvert.DeserializeObject<InputElement>(requestBody);
                }
                catch (Exception) {
                    throw;
                }
            element.Id = id;
            return element;
        }

        public static async Task<ActionResultWithId> HttpProcessing<DbElement, InputElement>(HttpRequest req, ILogger log, string ObjectIdAAD, Func<IAgroManager, IGenericOperation<DbElement, InputElement>> repo, InputElement element) where DbElement : DocumentBase where InputElement : InputBase {
            var method = req.Method.ToLower();
            switch (method) {
                case "get":
                    if (!string.IsNullOrWhiteSpace(element.Id)) {
                        var manager = await ContainerMethods.AgroManager(ObjectIdAAD, false);
                        var elementDb = await repo(manager).Get(element.Id);
                        return new ActionResultWithId {
                            Id = element.Id,
                            JsonResult = ContainerMethods.GetJsonGetContainer(elementDb, log)
                        };
                    }
                    return new ActionResultWithId {
                        Id = null,
                        JsonResult = ContainerMethods.GetJsonGetContainer(new ExtGetContainer<string> { ErrorMessage = "Id obligatorio", StatusResult = ExtGetDataResult.Error }, log)
                    };
                default:
                    //ExtPostContainer<string> saveReturn;
                    string EntityName = ((DbElement)Activator.CreateInstance(typeof(DbElement))).CosmosEntityName;
                    var opInstance = new OperationInstance<InputElement>(element, element.Id, EntityName, method, ObjectIdAAD);
                    await ServiceBus.PushElement(opInstance, EntityName);
                    return new ActionResultWithId {
                        Id = null,
                        JsonResult = new JsonResult("Operacion en cola.")
                    };
                    //try {
                    //    saveReturn = await repo.SaveInput(element, false);
                    //    await recordAcitvity.SaveInput(new UserActivityInput {
                    //        Action = method.Equals("post") ? UserActivityAction.CREATE : UserActivityAction.MODIFY,
                    //        Date = DateTime.Now,
                    //        EntityName = ((DbElement)Activator.CreateInstance(typeof(DbElement))).CosmosEntityName,
                    //        EntityId = saveReturn.IdRelated
                    //    }, false);
                    //}
                    //catch (Exception ex) {
                    //    var extPostError = new ExtPostErrorContainer<string> {
                    //        InternalException = ex,
                    //        Message = ex.Message,
                    //        MessageResult = ExtMessageResult.Error
                    //    };
                    //    if (ex is Validation_Exception)
                    //        extPostError.ValidationMessages = ((Validation_Exception)ex).ErrorMessages;
                    //    return new ActionResultWithId {
                    //        Id = null,
                    //        JsonResult = ContainerMethods.GetJsonPostContainer(extPostError, log)
                    //    };
                    //}
                    //return new ActionResultWithId {
                    //    Id = saveReturn.IdRelated,
                    //    JsonResult = ContainerMethods.GetJsonPostContainer(saveReturn, log)
                    //};
            }
        }

    }

    public class ActionResultWithId {

        public string Id { get; set; }
        public ActionResult JsonResult { get; set; }

    }

}