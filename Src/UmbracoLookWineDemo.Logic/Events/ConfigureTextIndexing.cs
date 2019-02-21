using Our.Umbraco.Look;
using Umbraco.Core;
using Umbraco.Core.Models;
using System.Configuration;
using System.Net;
using Umbraco.Core.Logging;

namespace UmbracoLookWineDemo.Logic.Events
{
    public class ConfigureTextIndexing : ApplicationEventHandler
    {
        /// <summary>
        /// Umbraco has started event
        /// </summary>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            LookConfiguration.TextIndexer = indexingContext => { 

                if (!indexingContext.IsDetached && indexingContext.Item.ItemType == PublishedItemType.Content)
                {
                    
                    // build up URL for the page, you would most likely want to set a domain url in your web.config
                    var domain = "http://localhost:12000";
                    var url = $"{domain}/umbraco/Surface/ContentSurface/GetAsyncContent?nodeId={indexingContext.Item.Id}";

                    using (var client = new WebClient())
                    {
                        try
                        {
                            // Download the page
                            string htmlCode = client.DownloadString(url);
                            // Strip the HTML from the page
                            var cleanContent = htmlCode.StripHtml();
                            // Add the cleaned up content to the index.
                            return cleanContent;
                        }
                        catch
                        {
                            LogHelper.Info(typeof(ConfigureTagIndexing), "Cannot read HTML from" + url + " so cannot not add to index.");
                        }
                    }
                }

                return null; // don't index
            };
        }
        
       
    }
}