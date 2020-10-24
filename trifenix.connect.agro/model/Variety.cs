﻿using Cosmonaut;
using Cosmonaut.Attributes;
using trifenix.connect.agro.index_model.enums;
using trifenix.connect.agro.index_model.props;
using trifenix.connect.agro.mdm_attributes;
using trifenix.connect.entities.cosmos;
using trifenix.connect.mdm.enums;

namespace trifenix.connect.agro_model
{
    [SharedCosmosCollection("agro", "Variety")]    
    [ReferenceSearchHeader(EntityRelated.VARIETY, Kind = EntityKind.ENTITY, PathName = "varieties")]
    [GroupMenu(MenuEntityRelated.MANTENEDORES, PhisicalDevice.ALL, SubMenuEntityRelated.ESPECIES)]
    public class Variety : DocumentBaseName, ISharedCosmosEntity
    {
        /// <summary>
        /// identificador
        /// </summary>
        public override string Id { get; set; }

        /// <summary>
        /// Nombre de la variedad
        /// </summary>
        [StringSearch(StringRelated.GENERIC_NAME)]
        public override string Name { get; set; }

        /// <summary>
        /// abreviación.
        /// </summary>
        [StringSearch(StringRelated.GENERIC_ABBREVIATION)]
        public string Abbreviation { get; set; }


        /// <summary>
        /// Identificador de una especie.
        /// </summary>
        [ReferenceSearch(EntityRelated.SPECIE)]
        public string IdSpecie { get; set; }

        /// <summary>
        /// autonumérico.
        /// </summary>
        [AutoNumericSearch(StringRelated.GENERIC_CORRELATIVE)]
        public override string ClientId { get; set; }
    }
}
