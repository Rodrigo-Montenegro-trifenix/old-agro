﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trifenix.agro.db.interfaces;
using trifenix.agro.db.interfaces.agro.common;
using trifenix.agro.db.interfaces.common;
using trifenix.agro.db.model;
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
    public class OrderFolderOperations : MainOperation<OrderFolder, OrderFolderInput>, IGenericOperation<OrderFolder, OrderFolderInput> {
        private readonly ICommonQueries commonQueries;

        public OrderFolderOperations(IMainGenericDb<OrderFolder> repo, IExistElement existElement, IAgroSearch search, ICommonQueries commonQueries, ICommonDbOperations<OrderFolder> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) {
            this.commonQueries = commonQueries;
        }

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(OrderFolder orderFolder) {
            await repo.CreateUpdate(orderFolder);
            search.AddDocument(orderFolder);
            
            return new ExtPostContainer<string> {
                IdRelated = orderFolder.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(OrderFolderInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var orderFolder = new OrderFolder {
                Id = id,
                IdApplicationTarget = input.IdApplicationTarget,
                IdIngredientCategory = input.IdIngredientCategory,
                IdIngredient = input.IdIngredient,
                IdPhenologicalEvent = input.IdPhenologicalEvent,
                IdSpecie = input.IdSpecie
            };
            if (!isBatch)
                return await Save(orderFolder);
            await repo.CreateEntityContainer(orderFolder);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }
        
    }

}