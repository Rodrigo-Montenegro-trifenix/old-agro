﻿using Cosmonaut;
using Cosmonaut.Attributes;

namespace trifenix.agro.db.model.agro {

    [SharedCosmosCollection("agro", "Role")]
    public class Role : DocumentBase, ISharedCosmosEntity {

        public override string Id { get; set; }
        public string Name { get; set; }

    }
}