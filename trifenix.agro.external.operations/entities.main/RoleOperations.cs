﻿using System;
using System.Threading.Tasks;
using trifenix.agro.db.interfaces;
using trifenix.agro.db.interfaces.agro.common;
using trifenix.agro.db.interfaces.common;
using trifenix.agro.db.model;
using trifenix.agro.enums.input;
using trifenix.agro.external.interfaces;
using trifenix.agro.model.external;
using trifenix.agro.model.external.Input;
using trifenix.agro.search.interfaces;
using trifenix.agro.validator.interfaces;

namespace trifenix.agro.external.operations.entities.main {
    public class RoleOperations : MainOperation<Role, RoleInput>, IGenericOperation<Role, RoleInput> {
        public RoleOperations(IMainGenericDb<Role> repo, IExistElement existElement, IAgroSearch search, ICommonDbOperations<Role> commonDb, IValidator validators) : base(repo, existElement, search, commonDb, validators) { }

        public Task Remove(string id) {
            throw new NotImplementedException();
        }

        public async Task<ExtPostContainer<string>> Save(Role role) {
            await repo.CreateUpdate(role);
            search.AddDocument(role);
            return new ExtPostContainer<string> {
                IdRelated = role.Id,
                MessageResult = ExtMessageResult.Ok
            };
        }

        public async Task<ExtPostContainer<string>> SaveInput(RoleInput input, bool isBatch) {
            await Validate(input);
            var id = !string.IsNullOrWhiteSpace(input.Id) ? input.Id : Guid.NewGuid().ToString("N");
            var role = new Role {
                Id = id,
                Name = input.Name
            };
            if (!isBatch)
                return await Save(role);
            await repo.CreateEntityContainer(role);
            return new ExtPostContainer<string> {
                IdRelated = id,
                MessageResult = ExtMessageResult.Ok
            };
        }

    }

}