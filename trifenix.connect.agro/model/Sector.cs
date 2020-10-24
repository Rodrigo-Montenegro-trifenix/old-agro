﻿using Cosmonaut;
using Cosmonaut.Attributes;
using trifenix.connect.agro.index_model.enums;
using trifenix.connect.agro.index_model.props;
using trifenix.connect.agro.mdm_attributes;
using trifenix.connect.entities.cosmos;
using trifenix.connect.mdm.enums;
using trifenix.connect.mdm_attributes;

namespace trifenix.connect.agro_model
{
    /// <summary>
    /// Sector de una parcela.
    /// </summary>
    [SharedCosmosCollection("agro", "Sector")]
    [ReferenceSearchHeader(EntityRelated.SECTOR, PathName = "sectors", Kind = EntityKind.ENTITY)]
    [GroupMenu(MenuEntityRelated.MANTENEDORES, PhisicalDevice.ALL, SubMenuEntityRelated.TERRENO)]
    public class Sector : DocumentBaseName, ISharedCosmosEntity
    {
        /// <summary>
        /// identificador
        /// </summary>
        public override string Id { get; set; }

        /// <summary>
        /// nombre
        /// </summary>
        [StringSearch(StringRelated.GENERIC_NAME)]
        [Unique]
        public override string Name { get; set; }

        /// <summary>
        /// Identificador visual 
        /// </summary>
        [AutoNumericSearch(StringRelated.GENERIC_CORRELATIVE)]
        public override string ClientId { get; set; }



    }


}
