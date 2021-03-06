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

namespace trifenix.agro.external.operations.entities.fields {

    public class SectorOperations : MainOperation<Sector, SectorInput>, IGenericOperation<Sector, SectorInput> {
        public SectorOperations(IMainGenericDb<Sector> repo, IExistElement existElement, IAgroSearch search, ICommonDbOperations<Sector> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) { }

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(Sector sector) {
            await repo.CreateUpdate(sector);
            search.AddDocument(sector);
            return new ExtPostContainer<string> {
                IdRelated = sector.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(SectorInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var sector = new Sector {
                Id = id,
                Name = input.Name
            };
            if (!isBatch)
                return await Save(sector);
            await repo.CreateEntityContainer(sector);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }
   
    }

}