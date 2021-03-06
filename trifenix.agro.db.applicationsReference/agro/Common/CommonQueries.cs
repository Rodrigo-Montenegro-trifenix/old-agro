﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trifenix.agro.db.interfaces.agro.common;
using trifenix.agro.db.model;
using trifenix.agro.db.model;
using trifenix.agro.db.model.orders;
using trifenix.agro.enums;
using trifenix.agro.enums.query;

namespace trifenix.agro.db.applicationsReference.agro.Common {

    public class CommonQueries : BaseQueries, ICommonQueries {

        public CommonQueries(AgroDbArguments dbArguments) : base(dbArguments) { }

        public async Task<string> GetSpecieAbbreviation(string idSpecie) => await SingleQuery<Specie, string>(Queries(DbQuery.SPECIEABBREVIATION_FROM_SPECIEID), idSpecie);

        public async Task<string> GetSpecieAbbreviationFromVariety(string idVariety) {
            var idSpecie = await SingleQuery<Variety, string>(Queries(DbQuery.SPECIEID_FROM_VARIETYID), idVariety);
            return await GetSpecieAbbreviation(idSpecie);
        }

        public async Task<string> GetSpecieAbbreviationFromBarrack(string idBarrack) {
            var result = await SingleQuery<Barrack, string>(Queries(DbQuery.VARIETYID_FROM_BARRACKID), idBarrack);
            return await GetSpecieAbbreviationFromVariety(result);
        }

        public async Task<string> GetSpecieAbbreviationFromOrder(string idOrder) {
            var result = await SingleQuery<ApplicationOrder, string>(Queries(DbQuery.IDBARRACK_FROM_ORDERID), idOrder);
            return await GetSpecieAbbreviationFromBarrack(result);
        }

        public async Task<List<string>> GetUsersMailsFromRoles(List<string> idsRoles) {
            var result = await MultipleQuery<User, string>(Queries(DbQuery.MAILUSERS_FROM_ROLES),  string.Join(",", idsRoles.Select(idRole => $"'{idRole}'").ToArray()));
            List<string> emails = result.ToList();
            return emails;
        }

        public async Task<string> GetSeasonId(string idBarrack) => await SingleQuery<Barrack, string>(Queries(DbQuery.SEASONID_FROM_BARRACKID), idBarrack);

        public async Task<string> GetUserIdFromAAD(string idAAD) => await SingleQuery<User, string>(Queries(DbQuery.USERID_FROM_IDAAD), idAAD);

        public async Task<string> GetDefaultDosesId(string idProduct) => await SingleQuery<Dose, string>(Queries(DbQuery.DEFAULTDOSESID_BY_PRODUCTID), idProduct);

        public async Task<IEnumerable<string>> GetActiveDosesIdsFromProductId(string idProduct) => await MultipleQuery<Dose, string>(Queries(DbQuery.ACTIVEDOSESIDS_FROM_PRODUCTID), idProduct);

        public async Task<string> GetEntityName<T>(string id) where T : DocumentBaseName => await SingleQuery<T, string>(Queries(DbQuery.NAME_BY_ID), id);

    }

}