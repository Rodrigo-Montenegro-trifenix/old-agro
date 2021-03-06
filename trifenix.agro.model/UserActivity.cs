﻿using Cosmonaut;
using Cosmonaut.Attributes;
using System;

using trifenix.agro.enums;
using trifenix.agro.enums.model;

namespace trifenix.agro.db.model {

    [SharedCosmosCollection("agro", "UserActivity")]
    public class UserActivity : DocumentBase, ISharedCosmosEntity {

        public override string Id { get; set; }

        
        public string UserId { get; set; }

        
        public UserActivityAction Action { get; set; }

        
        public DateTime Date { get; set; }


        public string EntityName { get; set; }
        public string EntityId { get; set; }

    }

}