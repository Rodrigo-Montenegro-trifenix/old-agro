﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using trifenix.agro.db.model.agro.orders;
using trifenix.agro.model.external;
using trifenix.agro.model.external.Input;
using trifenix.agro.model.external.output;

namespace trifenix.agro.external.interfaces.entities.orders
{
    public interface IApplicationOrderOperations
    {

        Task<ExtPostContainer<string>> SaveNewApplicationOrder(ApplicationOrderInput input);

        Task<ExtPostContainer<OutPutApplicationOrder>> SaveEditApplicationOrder(string id, ApplicationOrderInput input);

        Task<ExtGetContainer<OutPutApplicationOrder>> GetApplicationOrder(string id);

        Task<ExtGetContainer<List<OutPutApplicationOrder>>> GetApplicationOrders();

        Task<ExtGetContainer<OrderResult>> GetApplicationOrdersByPage(int page, int quantity, bool orderByDesc);






    }
}
