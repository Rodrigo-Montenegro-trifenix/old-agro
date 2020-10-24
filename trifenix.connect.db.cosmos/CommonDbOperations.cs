﻿using Cosmonaut.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using trifenix.connect.entities.cosmos;
using trifenix.connect.interfaces.db.cosmos;

namespace trifenix.connect.db.cosmos
{
    public class CommonDbOperations<T> : ICommonDbOperations<T> where T : DocumentBase
    {

        public async Task<T> FirstOrDefaultAsync(IQueryable<T> list, Expression<Func<T, bool>> predicate) {
            if (list == null) return (T)(object)null;
            return await list.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> TolistAsync(IQueryable<T> list) {
            if (list == null)
                return new List<T>();
            return await list.ToListAsync();
        }

        public IQueryable<T> WithPagination(IQueryable<T> list, int page, int totalElementsOnPage) {
            return list.WithPagination(page, totalElementsOnPage);
        }

    }
}