﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using trifenix.agro.attr;
using trifenix.agro.db.model;
using trifenix.agro.enums.searchModel;

namespace trifenix.agro.model.external.Input {

    [ReferenceSearchHeader(EntityRelated.USER)]
    public class UserApplicatorInput : InputBase {

        [Required, Unique]
        [StringSearch(StringRelated.GENERIC_NAME)]
        public string Name { get; set; }

        [Required, Unique]
        [StringSearch(StringRelated.GENERIC_RUT)]
        public string Rut { get; set; }

        [Unique]
        [StringSearch(StringRelated.GENERIC_EMAIL)]
        public string Email { get; set; }

        [Reference(typeof(Job))]
        [ReferenceSearch(EntityRelated.JOB)]
        public string IdJob { get; set; }

        [Required, Reference(typeof(Role))]
        [ReferenceSearch(EntityRelated.ROLE)]
        public List<string> IdsRoles { get; set; }


        [ReferenceSearch(EntityRelated.NEBULIZER)]
        [Reference(typeof(Nebulizer))]
        public string IdNebulizer { get; set; }


        [ReferenceSearch(EntityRelated.TRACTOR)]
        [Reference(typeof(Tractor))]
        public string IdTractor { get; set; }
    }

   

}