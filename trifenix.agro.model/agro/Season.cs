﻿using Cosmonaut;
using Cosmonaut.Attributes;
using System;

namespace trifenix.agro.db.model.agro {
    [SharedCosmosCollection("agro", "Season")]
    public class Season : DocumentBase, ISharedCosmosEntity {
        public override string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool Current { get; set; }

        public string IdCostCenter { get; set; }



    }
}