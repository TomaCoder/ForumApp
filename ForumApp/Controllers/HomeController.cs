using System.Web.Mvc;

using ForumApp.Helpers;

namespace ForumApp.Controllers
{
	public class HomeController : Controller
	{
		[Authorization]
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Forum()
		{
			TopicsController controller = new TopicsController();
			
			return View("Forum", controller.GetDataCollection());
		}
	}
}