﻿using System.ComponentModel.DataAnnotations;
using trifenix.agro.attr;
using trifenix.agro.enums.searchModel;

namespace trifenix.agro.model.external.Input {


    [ReferenceSearchHeader(EntityRelated.SPECIE)]
    public class SpecieInput : InputBase {

        [Required, Unique]
        [StringSearch(StringRelated.GENERIC_NAME)]
        public string Name { get; set; }

        [Required, Unique]
        [StringSearch(StringRelated.GENERIC_ABBREVIATION)]
        public string Abbreviation { get; set; }
    }

 

}