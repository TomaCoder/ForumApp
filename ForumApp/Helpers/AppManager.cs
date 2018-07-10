using System.Web;

namespace ForumApp.Helpers
{
	public static class AppManager
	{
		public static int? GetCurrentUserID()
		{
			if(HttpContext.Current.Session.Count == 0){
				return null;
			}
			if (HttpContext.Current.Session["UserID"] == null){
				return 0;
			}

			return (int)System.Web.HttpContext.Current.Session["UserID"];
		}
		public static string GetCurrentUserName()
		{
			if (HttpContext.Current.Session["UserID"] == null)
			{
				return null;
			}

			return (string)HttpContext.Current.Session["UserName"];
		}
	}
}