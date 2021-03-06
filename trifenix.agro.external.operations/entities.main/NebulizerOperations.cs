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

namespace trifenix.agro.external.operations.entities.main {
    public class NebulizerOperations : MainOperation<Nebulizer, NebulizerInput>, IGenericOperation<Nebulizer, NebulizerInput> {
        public NebulizerOperations(IMainGenericDb<Nebulizer> repo, IExistElement existElement, IAgroSearch search, ICommonDbOperations<Nebulizer> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) { }

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(Nebulizer nebulizer) {
            await repo.CreateUpdate(nebulizer);
            search.AddDocument(nebulizer);
            return new ExtPostContainer<string> {
                IdRelated = nebulizer.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(NebulizerInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var nebulizer = new Nebulizer {
                Id = id,
                Brand = input.Brand,
                Code = input.Code
            };
            if (!isBatch)
                return await Save(nebulizer);
            await repo.CreateEntityContainer(nebulizer);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}