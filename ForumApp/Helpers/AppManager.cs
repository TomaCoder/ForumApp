using System.Web;

namespace ForumApp.Helpers
{
	public static class AppManager
	{
		//public bool? IsAdmin{ get; set; }
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
		public static string GetCurrentUserRole()
		{
			if (HttpContext.Current.Session.Count == 0)
			{
				return null;
			}
			if (HttpContext.Current.Session["Role"] == null)
			{
				return null;
			}

			return HttpContext.Current.Session["Role"].ToString();
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