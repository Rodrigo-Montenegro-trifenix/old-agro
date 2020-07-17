﻿using Microsoft.Spatial;
using System;
using trifenix.connect.mdm.az_search;
using trifenix.connect.search_mdl;

namespace trifenix.agro.search.operations
{
    public class Implements : Implements<GeographyPoint>
    {
        public Type num32 => typeof(Num32Property);

        public Type dbl => typeof(DblProperty);

        public Type bl => typeof(BoolProperty);

        public Type num64 => typeof(Num64Property);

        public Type dt => typeof(DtProperty);

        public Type enm => typeof(EnumProperty);

        public Type rel => typeof(RelatedId);

        public Type str => typeof(StrProperty);

        public Type sug => typeof(StrProperty);

        public Type geo => typeof(GeoProperty);


        // refactorizar.
        public Func<object, GeographyPoint> GeoObjetoToGeoSearch => (ob)=>GeographyPoint.Create(0,0);
    }

}