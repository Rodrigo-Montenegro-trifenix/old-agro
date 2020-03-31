﻿using Cosmonaut;
using Cosmonaut.Attributes;
using trifenix.agro.attr;
using trifenix.agro.enums;
using trifenix.agro.enums.searchModel;

namespace trifenix.agro.db.model
{
    [SharedCosmosCollection("agro", "Variety")]
    [ReferenceSearch(EntityRelated.VARIETY)]
    [ReferenceSearch(EntityRelated.POLLINATOR)]
    public class Variety : DocumentBaseName, ISharedCosmosEntity
    {
    
        public override string Id { get; set; }


        [StringSearch(StringRelated.GENERIC_NAME)]
        public override string Name { get; set; }

        [StringSearch(StringRelated.GENERIC_ABBREVIATION)]
        public string Abbreviation { get; set; }


        [ReferenceSearch(EntityRelated.SPECIE)]
        public string IdSpecie { get; set; }

    }
}
