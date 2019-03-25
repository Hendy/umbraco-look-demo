using System.Net;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web;

namespace UmbracoLookWineDemo.Logic.Controllers
{
    public class ContentSurfaceController : SurfaceController
    {
        /// <summary>
        /// This allows the site to fetch page content by ajax without the header and footer content.
        /// </summary>
        /// <param name="nodeId">The Page/Node Id to render</param>
        /// <returns></returns>
        public ActionResult GetAsyncContent(int nodeId)
        {
            // Here we are switching the default layout for the page to a blank view.
            // This will renders the page without the header and footer.

            ViewData["Layout"] = "/Views/_Ajax.cshtml";
            var typedContent = Umbraco.TypedContent(nodeId);

            if (typedContent == null) return null;

            // Get template alias for the page
            var t = typedContent.GetTemplateAlias();

            // We can't render pages without a template so return a Bad Request
            if (string.IsNullOrWhiteSpace(t)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Return the Rendered View as a Partial and send it the current page.
            return PartialView("~/Views/" + t + ".cshtml", typedContent);
        }
    }
}
