﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace trifenix.agro.search.operations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SearchQueryRes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SearchQueryRes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("trifenix.agro.search.operations.SearchQueryRes", typeof(SearchQueryRes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EntityIndex eq {0} and RelatedIds/any(elementId: elementId/EntityIndex eq {1} and elementId/EntityId eq &apos;{2}&apos;).
        /// </summary>
        internal static string ENTITIES_WITH_ENTITYID {
            get {
                return ResourceManager.GetString("ENTITIES_WITH_ENTITYID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EntityIndex eq {0} and  Id ne &apos;{3}&apos; and RelatedIds/any(elementId: elementId/EntityIndex eq {1} and elementId/EntityId eq &apos;{2}&apos;).
        /// </summary>
        internal static string ENTITIES_WITH_ENTITYID_EXCEPTID {
            get {
                return ResourceManager.GetString("ENTITIES_WITH_ENTITYID_EXCEPTID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EntityIndex eq {0} and Id eq &apos;{1}&apos;.
        /// </summary>
        internal static string GET_ELEMENT {
            get {
                return ResourceManager.GetString("GET_ELEMENT", resourceCulture);
            }
        }
    }
}
