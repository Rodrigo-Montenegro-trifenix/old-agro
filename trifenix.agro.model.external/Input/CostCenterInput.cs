﻿using System.ComponentModel.DataAnnotations;
using trifenix.agro.attr;
using trifenix.agro.db.model.core;
using trifenix.agro.enums.searchModel;

namespace trifenix.agro.model.external.Input {

    [ReferenceSearchHeader(EntityRelated.COSTCENTER)]
    public class CostCenterInput : InputBase {

        [Required, Unique]
        [StringSearch(StringRelated.GENERIC_NAME)]
        public string Name { get; set; }
        [Required,Reference(typeof(BusinessName))]
        [ReferenceSearch(EntityRelated.BUSINESSNAME)]
        public string IdBusinessName { get; set; }

    }

  

}