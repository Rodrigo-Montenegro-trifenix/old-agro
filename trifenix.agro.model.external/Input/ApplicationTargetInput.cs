﻿using System.ComponentModel.DataAnnotations;

namespace trifenix.agro.model.external.Input {

    public class ApplicationTargetInput : InputBaseName {

        [Required, Unique]
        public string Abbreviation { get; set; }
        
    }

    public class TargetSwaggerInput {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }
    }

}