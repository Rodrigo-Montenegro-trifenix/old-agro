﻿using Cosmonaut;
using Cosmonaut.Attributes;
using System;
using trifenix.agro.attr;
using trifenix.agro.enums.searchModel;

namespace trifenix.agro.db.model.orders {

    [SharedCosmosCollection("agro", "ApplicationOrder")]
    [ReferenceSearchHeader(EntityRelated.ORDER, Kind = EntityKind.PROCESS, PathName = "orders")]
    public class ApplicationOrder : DocumentBaseName<long>, ISharedCosmosEntity {
        /// <summary>
        /// Identificador de la entidad
        /// </summary>
        public override string Id { get; set; }

        [AutoNumericSearch(NumRelated.GENERIC_CORRELATIVE)]
        public override long ClientId { get; set; }

        [EnumSearch(EnumRelated.ORDER_TYPE)]
        public OrderType OrderType { get; set; }

        [SuggestSearch(StringRelated.GENERIC_NAME)]
        public override string Name { get; set; }

        [DateSearch(DateRelated.START_DATE_ORDER)]
        public DateTime StartDate { get; set; }

        [DateSearch(DateRelated.END_DATE_ORDER)]
        public DateTime EndDate { get; set; }

        [DoubleSearch(DoubleRelated.WETTING)]
        public double Wetting { get; set; }

        [ReferenceSearch(EntityRelated.DOSES_ORDER, true)]
        public DosesOrder[] DosesOrder { get; set; }

        [ReferenceSearch(EntityRelated.PREORDER)]
        public string[] IdsPreOrder { get; set; }

        [ReferenceSearch(EntityRelated.BARRACK_EVENT, true)]
        public BarrackOrderInstance[] Barracks { get; set; }

    }


    public class BarrackOrderInstance {

        [ReferenceSearch(EntityRelated.BARRACK)]
        public string  IdBarrack { get; set; }
        [ReferenceSearch(EntityRelated.NOTIFICATION_EVENT)]
        public string[] IdNotificationEvents { get; set; }

    }

    public class DosesOrder {

        [ReferenceSearch(EntityRelated.DOSES)]
        public string IdDoses { get; set; }


        [DoubleSearch(DoubleRelated.QUANTITY_APPLIED)]
        public double QuantityByHectare { get; set; }

    }

}