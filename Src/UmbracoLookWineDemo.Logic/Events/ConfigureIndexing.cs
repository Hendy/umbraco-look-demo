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
            LookConfiguration.TagIndexer = indexingContext =>
            {
                var tags = new List<LookTag>();

                switch (indexingContext.Item.DocumentTypeAlias)
                {
                    case "wineReview":
                        tags.AddRange(PropertiesToLookTags(new List<string>{ "designation", "variety", "region", "province", "country", "winery" }, indexingContext));
                        break;
                }
                
                return tags.ToArray();
            };
        }

        private List<LookTag> PropertiesToLookTags(List<string> properties, IndexingContext indexingContext)
        {
            var tags = new List<LookTag>();

            foreach (var key in properties)
            {
                var value = indexingContext.Item.GetPropertyValue<string>(key);
                if (!string.IsNullOrWhiteSpace(value))
                    tags.Add(new LookTag(key + ":" + value));
            }

            return tags;
        }
    }
}