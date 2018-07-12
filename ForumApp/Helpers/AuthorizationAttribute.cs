using System.Web;
using System.Web.Mvc;

namespace ForumApp.Helpers
{
	public class AuthorizationAttribute : ActionFilterAttribute
	{
		#region Overridden Method

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string url;
			if (System.Web.HttpContext.Current.Session["UserID"] != null)
			{
				url = new UrlHelper(filterContext.RequestContext).Action("Index", "Index");
				base.OnActionExecuting(filterContext);
			}
			else
			{
				url = new UrlHelper(filterContext.RequestContext).Action("Register", "Account");
				HttpContextBase httpContext = filterContext.RequestContext.HttpContext;
				httpContext.Response.Redirect(url, false);
			}
		}

		#endregion
	}
}