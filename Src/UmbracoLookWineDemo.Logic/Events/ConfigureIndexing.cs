using System;
using System.Collections.Generic;
using Our.Umbraco.Look;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoLookWineDemo.Logic.Events
{
    public class ConfigureIndexing : ApplicationEventHandler
    {
        /// <summary>
        /// Umbraco has started event
        /// </summary>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            LookConfiguration.TextIndexer = indexingContext => { 

                // eg. if content, render page and scrape markup
                if (!indexingContext.IsDetached && indexingContext.Item.ItemType == PublishedItemType.Content) 
                {				
                    // (could pass in httpContext to render page without http web request)
                    // return string
                }

                return null; // don't index
            };
            
            LookConfiguration.TagIndexer = indexingContext =>
            {
                var tags = new List<LookTag>();

                if (indexingContext.Item.DocumentTypeAlias == "wineReview")
                {
                    // add tags 
                    tags.Add(new LookTag("designation:" + indexingContext.Item.GetPropertyValue<string>("designation")));
                }
                return tags.ToArray();
            };
            
        }
    }
}