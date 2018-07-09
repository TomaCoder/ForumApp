using System.Web.Mvc;

namespace ForumApp.Controllers
{
    public class BaseDataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}