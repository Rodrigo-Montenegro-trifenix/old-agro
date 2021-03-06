﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trifenix.agro.db.exceptions;
using trifenix.agro.db.interfaces;
using trifenix.agro.db.interfaces.agro.common;
using trifenix.agro.db.interfaces.common;
using trifenix.agro.db.model;
using trifenix.agro.db.model.orders;
using trifenix.agro.enums;
using trifenix.agro.enums.input;
using trifenix.agro.enums.searchModel;
using trifenix.agro.external.interfaces;
using trifenix.agro.model.external;
using trifenix.agro.model.external.Input;
using trifenix.agro.search.interfaces;
using trifenix.agro.search.model;
using trifenix.agro.validator.interfaces;

namespace trifenix.agro.external.operations.entities.orders {

    public class ApplicationOrderOperations : MainOperation<ApplicationOrder, ApplicationOrderInput>, IGenericOperation<ApplicationOrder, ApplicationOrderInput> {

        private readonly ICommonQueries commonQueries;

        public ApplicationOrderOperations(IMainGenericDb<ApplicationOrder> repo, IExistElement existElement, IAgroSearch search, ICommonQueries commonQueries, ICommonDbOperations<ApplicationOrder> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) {
            this.commonQueries = commonQueries;
        }

        public async Task Remove(string id) { }

        public override async Task Validate(ApplicationOrderInput applicationOrderInput) {
            await base.Validate(applicationOrderInput);
            List<string> errors = new List<string>();
            if (applicationOrderInput.OrderType == OrderType.PHENOLOGICAL && !applicationOrderInput.IdsPreOrder.Any())
                    errors.Add("Si la orden es fenológica, deben existir preordenes fenologicas asociadas.");
            foreach (var doses in applicationOrderInput.DosesOrder) {
                bool exists = await existElement.ExistsById<Dose>(doses.IdDoses);
                if (!exists)
                    errors.Add($"No existe dosis con id '{doses.IdDoses}'.");
            }
            foreach (var barrack in applicationOrderInput.Barracks) {
                bool exists = await existElement.ExistsById<Barrack>(barrack.IdBarrack);
                if (!exists)
                    errors.Add($"No existe cuartel con id '{barrack.IdBarrack}'.");
                if (barrack.IdNotificationEvents != null && barrack.IdNotificationEvents.Any()) {
                    foreach (var idNotification in barrack.IdNotificationEvents) {
                        bool existsEvent = await existElement.ExistsById<NotificationEvent>(idNotification);
                        if (!existsEvent)
                            errors.Add($"No existe notificacion con id '{idNotification}'.");
                    }
                }
            }
            if (applicationOrderInput.StartDate > applicationOrderInput.EndDate)
                errors.Add("La fecha inicial no puede ser mayor a la final.");
            if (errors.Count > 0)
                throw new Validation_Exception { ErrorMessages = errors };
        }


        public async Task<ExtPostContainer<string>> Save(ApplicationOrder applicationOrder) {
            await repo.CreateUpdate(applicationOrder);
            var relatedEntities = new List<RelatedId>();
            
            search.DeleteElementsWithRelatedElement(EntityRelated.BARRACK_EVENT, EntityRelated.ORDER, applicationOrder.Id);
            search.DeleteElementsWithRelatedElement(EntityRelated.DOSES_ORDER, EntityRelated.ORDER, applicationOrder.Id);
            search.AddDocument(applicationOrder);
            return new ExtPostContainer<string> {
                IdRelated = applicationOrder.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(ApplicationOrderInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var order = new ApplicationOrder {
                Id = id,
                Barracks = input.Barracks,
                DosesOrder = input.DosesOrder,
                EndDate = input.EndDate,
                StartDate = input.StartDate,
                IdsPreOrder = input.IdsPreOrder,
                Name = input.Name,
                OrderType = input.OrderType,
                Wetting = input.Wetting
            };
            if (!isBatch)
                return await Save(order);
            await repo.CreateEntityContainer(order);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}