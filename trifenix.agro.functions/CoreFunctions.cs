using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using trifenix.agro.functions.mantainers;
using trifenix.agro.model.external.Input;
using trifenix.agro.db.model.agro;
using trifenix.agro.db.model.agro.core;
using trifenix.agro.functions.Helper;
using System.Net;
using trifenix.agro.model.external;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using trifenix.agro.swagger.model.input;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using trifenix.agro.external.operations.helper;
using System.Collections.Generic;
using trifenix.agro.db.model.agro.orders;
using trifenix.agro.db.model;

namespace trifenix.agro.functions
{
    /// <summary>
    /// Funciones 
    /// </summary>
    public static class CoreFunctions
    {


       


        /// <summary>
        /// Login, donde usa usuario y contrase�a para obtener el token.
        /// </summary>
        /// <param name="req">cabecera que debe incluir el modelo de entrada </param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("login")]
        [RequestHttpHeader("Authorization", isRequired: true)]

        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            [RequestBodyType(typeof(LoginInput), "Nombre de usuario y contrase�a")]HttpRequest req,
            ILogger log)
        {

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic credenciales = JsonConvert.DeserializeObject(requestBody);

            string clientId = "a81f0ad4-912b-46d3-ba3e-7bf605693242";
            string scope = "https://sprint3-jhm.trifenix.io/App.access";
            string clientSecret = "gUjIa4F=NXlAwwMCF2j2SFMMj3?m@=FM";
            string username = (string)credenciales["username"];
            string password = (string)credenciales["password"];
            string grantType = "password";
            string endPoint = "https://login.microsoftonline.com/jhmad.onmicrosoft.com/oauth2/v2.0/token";

            HttpClient client = new HttpClient();
            var parametros = new Dictionary<string, string> {
                {"client_id",clientId},
                {"scope",scope},
                {"client_secret",clientSecret},
                {"username",username},
                {"password",password},
                {"grant_type",grantType}
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endPoint);
            requestMessage.Content = new FormUrlEncodedContent(parametros);
            var response = await client.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(responseBody);
            client.Dispose();
            string accessToken = json.access_token;
            return ContainerMethods.GetJsonGetContainer(OperationHelper.GetElement(accessToken), log);
        }



        


        /// <summary>
        /// Creaci�n de Sector
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("sector_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> SectorPost(
            
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sectors")]
            [RequestBodyType(typeof(SectorSwaggerInput), "Sector")]
        HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Sectors, string.Empty);
            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n del Sector
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("sector_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> SectorPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "sectors/{id}")]
            [RequestBodyType(typeof(SectorSwaggerInput), "Sector")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Sectors, id);
            return result.JsonResult;
        }



        /// <summary>
        /// Creaci�n de parcela
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("plotland_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> PlotLandsPost(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "plotlands")]
            [RequestBodyType(typeof(PlotLandSwaggerInput), "Parcela")]
            HttpRequest req,
            
            ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.PlotLands, string.Empty);
            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de parcela
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("plotland_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> PlotLandsPut(
            
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "plotlands/{id}")]
            [RequestBodyType(typeof(PlotLandSwaggerInput), "Parcela")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.PlotLands, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Especie
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("specie_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> SpeciesPost(
            
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "species")]
            [RequestBodyType(typeof(SpecieSwaggerInput), "Especie")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Species, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificar Especie
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("specie_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> SpeciesPut(

            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "species/{id}")]
            [RequestBodyType(typeof(SpecieSwaggerInput), "Especie")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Species, id);

            return result.JsonResult;
        }

        /// <summary>
        /// A�adir Variedad
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("variety_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> VarietyPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "varieties")]
            [RequestBodyType(typeof(VarietySwaggerInput), "Variedad")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Varieties, string.Empty);

            return result.JsonResult;
        }

        /// <summary>
        /// A�adir Variedad
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("variety_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> VarietyPut(

            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "varieties/{id}")]
            [RequestBodyType(typeof(VarietySwaggerInput), "Variedad")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Varieties, id);

            return result.JsonResult;
        }

        /// <summary>
        /// A�adir Objetivo de aplicaci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("target_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> TargetPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "targets")]
            [RequestBodyType(typeof(TargetSwaggerInput), "Objetivo de aplicaci�n")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ApplicationTargets, string.Empty);

            return result.JsonResult;
        }

        /// <summary>
        /// Modificar objetivo de aplicaci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("target_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> TargetPut(

            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "targets/{id}")]
            [RequestBodyType(typeof(TargetSwaggerInput), "Objetivo de aplicaci�n")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ApplicationTargets, id);
            return result.JsonResult;
        }

        /// <summary>
        /// A�adir Evento fenol�gico
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("phenological_event_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> PhenologicalEventPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "phenological_events")]
            [RequestBodyType(typeof(PhenologicalEventSwaggerInput), "Evento Fenol�gico")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.PhenologicalEvents, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de evento fenol�gico
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("phenological_event_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> PhenologicalEventPut(

            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "phenological_events/{id}")]
            [RequestBodyType(typeof(PhenologicalEventSwaggerInput), "Evento Fenol�gico")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.PhenologicalEvents, id);

            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Entidad Certificadora
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("certified_entities_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> CertifiedEntityPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "certified_entities")]
            [RequestBodyType(typeof(CertifiedEntitySwaggerInput), "Entidad Certificadora")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.CertifiedEntities, string.Empty);

            return result.JsonResult;
        }

        /// <summary>
        /// Modificaci�n de Entidad Certificadora
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("certified_entities_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> CertifiedEntityPut(

            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "certified_entities/{id}")]
            [RequestBodyType(typeof(CertifiedEntitySwaggerInput), "Entidad Certificadora")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.CertifiedEntities, id);

            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Categor�a de ingrediente
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("ingredient_categories_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> CategoryIngredientPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ingredient_categories")]
            [RequestBodyType(typeof(IngredientCategorySwaggerInput), "Categor�a de Ingrediente")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.IngredientCategories, string.Empty);

            return result.JsonResult;
        }

        /// <summary>
        /// Modificaci�n de Categor�a de ingredientes
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("ingredient_categories_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> CategoryIngredientPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ingredient_categories/{id}")]
            [RequestBodyType(typeof(IngredientCategorySwaggerInput), "Categor�a de Ingrediente")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.IngredientCategories, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Ingredientes
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("ingredients_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> IngredientsPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "ingredients")]
            [RequestBodyType(typeof(IngredientSwaggerInput), "Ingrediente")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Ingredients, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Ingrediente
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("ingredients_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> IngredientPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "ingredients/{id}")]
            [RequestBodyType(typeof(IngredientSwaggerInput), "Ingrediente")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Ingredients, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Producto
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("products_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> ProductsPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")]
            [RequestBodyType(typeof(ProductSwaggerInput), "Producto")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Products, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Productos
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("products_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> ProductPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products/{id}")]
            [RequestBodyType(typeof(ProductSwaggerInput), "Producto")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Products, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Rol
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("roles_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> RolePost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "roles")]
            [RequestBodyType(typeof(RoleSwaggerInput), "Rol")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Roles, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Rol
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("roles_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> RolePut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "roles/{id}")]
            [RequestBodyType(typeof(RoleSwaggerInput), "Rol")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Roles, id);
            return result.JsonResult;
        }



        /// <summary>
        /// A�adir Trabajo
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("jobs_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> JobPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "jobs")]
            [RequestBodyType(typeof(JobSwaggerInput), "Trabajo")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Jobs, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Trabajo
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("jobs_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> JobPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "jobs/{id}")]
            [RequestBodyType(typeof(JobSwaggerInput), "Trabajo")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Jobs, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Usuario
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("users_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> UserPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")]
            [RequestBodyType(typeof(UserApplicatorSwaggerInput), "Usuario aplicador")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Users, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Usuario
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("users_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> UsersPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id}")]
            [RequestBodyType(typeof(UserApplicatorSwaggerInput), "Usuario aplicador")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Users, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Nebulizador
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("nebulizers_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> NebulizersPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "nebulizers")]
            [RequestBodyType(typeof(NebulizerSwaggerInput), "Nebulizador")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Nebulizers, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Nebulizador
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("nebulizer_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> NebulizerPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "nebulizers/{id}")]
            [RequestBodyType(typeof(NebulizerSwaggerInput), "Nebulizador")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Nebulizers, id);
            return result.JsonResult;
        }

        /// <summary>
        /// A�adir Tractor
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("tractors_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> TractorPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tractors")]
            [RequestBodyType(typeof(TractorSwaggerInput), "Tractor")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Tractors, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Trabajo
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("tractors_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> TractorPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "tractors/{id}")]
            [RequestBodyType(typeof(TractorSwaggerInput), "Tractor")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Tractors, id);
            return result.JsonResult;
        }



        /// <summary>
        /// A�adir Raz�n social
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("business_names_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> BusinessNamePost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "business_names")]
            [RequestBodyType(typeof(BusinessNameSwaggerInput), "Raz�n social")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.BusinessNames, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Raz�n Social
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("business_names_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> BusinessNamePut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "business_names/{id}")]
            [RequestBodyType(typeof(TractorSwaggerInput), "Raz�n Social")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.BusinessNames, id);
            return result.JsonResult;
        }



        /// <summary>
        /// A�adir centro de costos
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("cost_centers_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> CostCenterPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cost_centers")]
            [RequestBodyType(typeof(CostCenterSwaggerInput), "Centro de Costos")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.CostCenters, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de centro de costos
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("cost_centers_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> CostCenterPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "cost_centers/{id}")]
            [RequestBodyType(typeof(CostCenterSwaggerInput), "Centro de Costos")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.CostCenters, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Temporada
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("seasons_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> SeasonPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "seasons")]
            [RequestBodyType(typeof(SeasonSwaggerInput), "Temporada")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Seasons, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Temporada
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("seasons_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> SeasonPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "seasons/{id}")]
            [RequestBodyType(typeof(SeasonSwaggerInput), "Temporada")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Seasons, id);
            return result.JsonResult;
        }

        /// <summary>
        /// A�adir Ra�z
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("rootstock_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> RootStockPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "rootstock")]
            [RequestBodyType(typeof(RootStockSwaggerInput), "Ra�z")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Rootstock, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Ra�z
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("rootstock_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> RootStockPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "rootstock/{id}")]
            [RequestBodyType(typeof(RootStockSwaggerInput), "Ra�z")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Rootstock, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Carpeta de �rdenes
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("order_folders_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> OrderFolderPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order_folders")]
            [RequestBodyType(typeof(OrderFolderSwaggerInput), "Carpeta de �rdenes")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.OrderFolder, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Carpeta de �rdenes
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("order_folders_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> OrderFolderPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "order_folders/{id}")]
            [RequestBodyType(typeof(OrderFolderSwaggerInput), "Carpeta de �rdenes")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.OrderFolder, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir cuartel
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("barracks_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> BarracksPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "barracks")]
            [RequestBodyType(typeof(BarrackSwaggerInput), "Cuartel")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Barracks, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Cuartel
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("barracks_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> BarrackPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "barracks/{id}")]
            [RequestBodyType(typeof(BarrackSwaggerInput), "Cuartel")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.Barracks, id);
            return result.JsonResult;
        }





        /// <summary>
        /// A�adir Orden de aplicaci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("orders_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> OrderPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "orders")]
            [RequestBodyType(typeof(ApplicationOrderSwaggerInput), "Orden de aplicaci�n")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ApplicationOrders, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Orden de aplicaci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("orders_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> OrderPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "orders/{id}")]
            [RequestBodyType(typeof(ApplicationOrderSwaggerInput), "Orden de aplicaci�n")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ApplicationOrders, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Pre Orden de aplicaci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("pre_orders_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> PreOrderPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "pre_orders")]
            [RequestBodyType(typeof(PreOrderSwaggerInput), "Pre Orden de aplicaci�n")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.PreOrders, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Pre Orden de aplicaci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("pre_orders_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> PreOrderPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "pre_orders/{id}")]
            [RequestBodyType(typeof(PreOrderSwaggerInput), "Pre Orden de aplicaci�n")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.PreOrders, id);
            return result.JsonResult;
        }


        /// <summary>
        /// A�adir Ejecuci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("executions_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> ExecutionsPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "executions")]
            [RequestBodyType(typeof(ExecutionOrderSwaggerInput), "Ejecuci�n aplicaci�n")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ExecutionOrders, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Ejecuci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("executions_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> ExecutionsPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "executions/{id}")]
            [RequestBodyType(typeof(ExecutionOrderSwaggerInput), "Ejecuci�n aplicaci�n")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ExecutionOrders, id);
            return result.JsonResult;
        }



        /// <summary>
        /// A�adir Ejecuci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("executions_status_post")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> ExecutionsStatusPost(

            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "executions_status")]
            [RequestBodyType(typeof(ExecutionOrderStatusSwaggerInput), "Estatus Ejecuci�n aplicaci�n")]
            HttpRequest req, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ExecutionStatus, string.Empty);

            return result.JsonResult;
        }


        /// <summary>
        /// Modificaci�n de Estatus Ejecuci�n
        /// </summary>
        /// <return>
        /// Retorna un contenedor con el id
        /// </return>
        [FunctionName("executions_status_put")]
        [RequestHttpHeader("Authorization", isRequired: true)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExtGetContainer<string>))]
        public static async Task<IActionResult> ExecutionsStatusPut(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "executions_status/{id}")]
            [RequestBodyType(typeof(ExecutionOrderStatusSwaggerInput), "Ejecuci�n aplicaci�n")]
            HttpRequest req, string id, ILogger log)
        {
            var result = await GenericMantainer.SendInternalHttp(req, log, s => s.ExecutionStatus, id);
            return result.JsonResult;
        }


    }
}
