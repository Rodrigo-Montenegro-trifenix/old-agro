﻿using Microsoft.Spatial;
using System;
using System.Threading.Tasks;
using trifenix.agro.db.interfaces;
using trifenix.agro.db.interfaces.agro.common;
using trifenix.agro.db.interfaces.common;
using trifenix.agro.external.interfaces;
using trifenix.agro.search.interfaces;
using trifenix.agro.validator.interfaces;
using trifenix.connect.agro_model;
using trifenix.connect.agro_model_input;
using trifenix.connect.mdm.containers;
using trifenix.connect.mdm.enums;

namespace trifenix.agro.external.operations.entities.main
{
    public class NebulizerOperations<T> : MainOperation<Nebulizer, NebulizerInput,T>, IGenericOperation<Nebulizer, NebulizerInput> {
        public NebulizerOperations(IMainGenericDb<Nebulizer> repo, IAgroSearch<T> search, ICommonDbOperations<Nebulizer> commonDb, IValidatorAttributes<NebulizerInput, Nebulizer> validator) : base(repo, search, commonDb, validator) { }

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