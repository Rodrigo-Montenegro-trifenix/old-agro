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

    public class IngredientOperations : MainOperation<Ingredient, IngredientInput>, IGenericOperation<Ingredient, IngredientInput> {

        public IngredientOperations(IMainGenericDb<Ingredient> repo, IExistElement existElement, IAgroSearch search, ICommonDbOperations<Ingredient> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) {}

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(Ingredient ingredient) {
            await repo.CreateUpdate(ingredient);
            search.AddDocument(ingredient);
            return new ExtPostContainer<string> {
                IdRelated = ingredient.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(IngredientInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var ingredient = new Ingredient {
                Id = id,
                Name = input.Name,
                idCategory = input.idCategory
            };
            if (!isBatch)
                return await Save(ingredient);
            await repo.CreateEntityContainer(ingredient);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}