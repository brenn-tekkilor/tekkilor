﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace scraper {
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
    internal class ScraperResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ScraperResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("scraper.ScraperResources", typeof(ScraperResources).Assembly);
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
        ///   Looks up a localized string similar to Resuming Last Category Scrape..
        /// </summary>
        internal static string _msg_resume {
            get {
                return ResourceManager.GetString("_msg_resume", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Starting A New Scraping Session..
        /// </summary>
        internal static string _msg_start_new {
            get {
                return ResourceManager.GetString("_msg_start_new", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to section.CollapsibleFacetContainer-Categories &gt; div.CollapsibleFacetContainer-content &gt; ul.ProductsNavList.
        /// </summary>
        internal static string _query_categories_root_node {
            get {
                return ResourceManager.GetString("_query_categories_root_node", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to section.CollapsibleFacetContainer--Categories &gt; a.CollapsibleFacetContainer-collapseLink.
        /// </summary>
        internal static string _query_expand_categories_btn {
            get {
                return ResourceManager.GetString("_query_expand_categories_btn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to div.AutoGrid-cell &gt; div.ProductCard.
        /// </summary>
        internal static string _query_product_card_node {
            get {
                return ResourceManager.GetString("_query_product_card_node", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to button.kds-SolitarySearch-button.
        /// </summary>
        internal static string _query_product_search_btn {
            get {
                return ResourceManager.GetString("_query_product_search_btn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to li.ProductsNavList-item.is-selected.is-breadcrumb &gt; div.ProductsNavList-itemText.
        /// </summary>
        internal static string _query_selected_category {
            get {
                return ResourceManager.GetString("_query_selected_category", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://www.frysfood.com.
        /// </summary>
        internal static string _uri_home {
            get {
                return ResourceManager.GetString("_uri_home", resourceCulture);
            }
        }
    }
}