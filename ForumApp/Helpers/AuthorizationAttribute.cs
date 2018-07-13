using System.Web;
using System.Web.Mvc;

namespace ForumApp.Helpers
{
	public class AuthorizationAttribute : ActionFilterAttribute
	{
		public string Role { get; set; }

		public AuthorizationAttribute()
		{
		}

		public AuthorizationAttribute(string role) {
			Role = role;
		}

		#region Overridden Method

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string url;
			if(Role == "Admin" && AppManager.GetCurrentUserRole() != "Admin")
			{
				throw new HttpException("You don't have access. Only Admin users can do that action.");
			}

			if (HttpContext.Current.Session["UserID"] != null)
			{
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