﻿using System;
using System.Threading.Tasks;
using trifenix.agro.db.interfaces;
using trifenix.agro.db.interfaces.common;
using trifenix.agro.external.interfaces;
using trifenix.agro.search.interfaces;
using trifenix.connect.agro_model;
using trifenix.connect.agro_model_input;
using trifenix.connect.mdm.containers;
using trifenix.connect.mdm.enums;

namespace trifenix.agro.external.operations.entities.main
{
    public class BusinessNameOperations<T> : MainOperation<BusinessName, BusinessNameInput,T>, IGenericOperation<BusinessName, BusinessNameInput> {

        public BusinessNameOperations(IMainGenericDb<BusinessName> repo, IAgroSearch<T> search, ICommonDbOperations<BusinessName> commonDb, IValidatorAttributes<BusinessNameInput, BusinessName> validator) : base(repo, search, commonDb, validator) { }

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(BusinessName businessName) {
            await repo.CreateUpdate(businessName);
            search.AddDocument(businessName);
            return new ExtPostContainer<string> {
                IdRelated = businessName.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(BusinessNameInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var businessName = new BusinessName {
                Id = id,
                Name = input.Name,
                Email = input.Email,
                Giro = input.Giro,
                Phone = input.Phone,
                Rut = input.Rut,
                WebPage = input.WebPage
            };
            if (!isBatch)
                return await Save(businessName);
            await repo.CreateEntityContainer(businessName);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}