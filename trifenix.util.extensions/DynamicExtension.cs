﻿using System;
using System.Collections.Generic;
using System.Dynamic;


namespace trifenix.util.extensions
{
    public static class DynamicExtension
    {
        public static bool HasProperty(dynamic obj, string name)
        {
            Type objType = obj.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)obj).ContainsKey(name);
            }

            return objType.GetProperty(name) != null;
        }
    }
}
