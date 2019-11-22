﻿using trifenix.agro.db.interfaces.agro.events;
using trifenix.agro.db.interfaces.agro.ext;
using trifenix.agro.db.interfaces.agro.fields;
using trifenix.agro.db.interfaces.agro.orders;

namespace trifenix.agro.db.interfaces.agro
{
    public interface IAgroRepository
    {


        AgroDbArguments DbArguments { get; }
        IApplicationTargetRepository Targets { get; }

        IIngredientCategoryRepository Categories { get; }

        IIngredientRepository Ingredients { get; }

        IPhenologicalEventRepository PhenologicalEvents { get; }

        ISpecieRepository Species { get; }

        IRootstockRepository Rootstock { get; }

        ISeasonRepository Seasons { get; }


        IVarietyRepository Varieties { get; }

        IOrderFolderRepository OrderFolder { get; }
        
        IPhenologicalPreOrderRepository PhenologicalPreOrders { get; }

        INotificationEventRepository NotificationEvents { get; }

        IBarrackRepository Barracks { get; }

        IPlotLandRepository PlotLands { get; }

        ISectorRepository Sectors { get; }


        IProductRepository Products { get; }

        ICertifiedEntityRepository CertifiedEntities { get; }


        IApplicationOrderRepository Order { get;  }







    }
}
