using System.Web.Mvc;

namespace Gaar.EntityTag
{
    public class EntityTagAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string etagValue = EntityTagHelper.GetEtagValueInCache();
            var responseEtag = filterContext.HttpContext.Request.Headers["if-none-match"];
            if (string.IsNullOrWhiteSpace(responseEtag) || responseEtag != etagValue)
            {
                filterContext.HttpContext.Response.Headers["etag"] = etagValue;

                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(304);
            }
        }
    }
}