﻿using Microsoft.Azure.Documents.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class BarrackOperations : MainOperation<Barrack,BarrackInput>, IGenericOperation<Barrack, BarrackInput> {

        private readonly ICommonQueries commonQueries;

        public BarrackOperations(IMainGenericDb<Barrack> repo, IExistElement existElement, IAgroSearch search, ICommonQueries commonQueries, ICommonDbOperations<Barrack> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) {
            this.commonQueries = commonQueries;
        }

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(Barrack barrack) {
            await repo.CreateUpdate(barrack);
            search.DeleteElementsWithRelatedElement(EntityRelated.GEOPOINT, EntityRelated.BARRACK, barrack.Id);
            search.AddDocument(barrack);
            return new ExtPostContainer<string> {
                IdRelated = barrack.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(BarrackInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var barrack = new Barrack {
                Id = id,
                Name = input.Name,
                Hectares = input.Hectares,
                IdPlotLand = input.IdPlotLand,
                IdPollinator = input.IdPollinator,
                IdRootstock = input.IdRootstock,
                IdVariety = input.IdVariety,
                NumberOfPlants = input.NumberOfPlants,
                PlantingYear = input.PlantingYear,
                SeasonId = input.SeasonId
            };
            if (input.GeographicalPoints != null && input.GeographicalPoints.Any())
                barrack.GeographicalPoints = input.GeographicalPoints.Select(geoPoint => new Point(geoPoint.Longitude, geoPoint.Latitude)).ToArray();
            if (!isBatch)
                return await Save(barrack);
            await repo.CreateEntityContainer(barrack);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}