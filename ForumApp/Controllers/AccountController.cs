using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;

using ForumApp.Models;

namespace ForumApp.Controllers
{
    public class AccountController : Controller
    {
		//TODO move to some static place
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
	ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Login()
		{
			return View("Login");
		}

		public ActionResult Register()
		{
			return View("Register");
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = null;
				using (SqlConnection con = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand("GetUser", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;

						cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
						cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = model.Password;

						con.Open();
						SqlDataReader reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							user = new ApplicationUser
							{
								UserID = (int)reader["UserID"],
								NickName = reader["NickName"].ToString(),
								City = reader["City"].ToString(),
								Country = reader["Country"].ToString()
							};
						}

						reader.Close();
						con.Close();
					}
				}

				if (user != null)
				{
					System.Web.HttpContext.Current.Session.Add("UserID", user.UserID);
					System.Web.HttpContext.Current.Session.Add("UserName", user.NickName);
					return RedirectToAction("Index", "Home");
				}
			}

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = null;
				using (SqlConnection con = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand("InsertUser", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;

						cmd.Parameters.Add("@NickName", SqlDbType.NVarChar).Value = model.NickName;
						cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = model.Password;
						cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
						cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = model.City;
						cmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = model.Country;

						con.Open();
						SqlDataReader reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							user = new ApplicationUser
							{
								UserID = (int)reader["UserID"],
								NickName = reader["NickName"].ToString(),
								City = reader["City"].ToString(),
								Country = reader["Country"].ToString()
							};
						}

						reader.Close();
						con.Close();
					}
				}

				if(user != null){
					System.Web.HttpContext.Current.Session.Add("UserID", user.UserID);
					System.Web.HttpContext.Current.Session.Add("UserName", user.NickName);
					return RedirectToAction("Index", "Home");
				}
			}

			return View(model);
		}
	}
}