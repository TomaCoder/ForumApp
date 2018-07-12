using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

using ForumApp.Models;
using ForumApp.Helpers;

namespace ForumApp.Controllers
{
	public class PostsController : Controller
	{
		// GET: Posts
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
			  ConnectionStrings["ConnectionString"].ConnectionString;

		[FormatException]
		public JsonResult AddPost(PostViewModel vm)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand("InsertPost", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = vm.ThreadID;
						cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = (int)System.Web.HttpContext.Current.Session["UserID"];
						cmd.Parameters.Add("@Text", SqlDbType.NVarChar).Value = vm.Text;

						con.Open();
						SqlDataReader reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							vm = new PostViewModel
							{
								PostID = (int)reader["PostID"],
								ThreadID = (int)reader["ThreadID"],
								UserID = (int)reader["UserID"],
								Text = reader["Text"].ToString(),
								CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"])
							};
						}

						reader.Close();
						con.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new HttpException("You don't have access. Please login to continue", ex);
			}

			return this.Json(vm);
		}

		public List<PostViewModel> GetDataCollection(int threadID)
		{
			List<PostViewModel> dmCollection = new List<PostViewModel>();
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("GetPosts", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						PostViewModel vm = new PostViewModel
						{
							PostID = (int)reader["PostID"],
							ThreadID = (int)reader["ThreadID"],
							UserID = (int)reader["UserID"],
							NumPosts = (int?)reader["NumPosts"],
							Text = reader["Text"].ToString(),
							NickName = reader["NickName"].ToString(),
							City = reader["City"].ToString(),
							Country = reader["Country"].ToString(),
							CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"]),
							UserCreatedOn = (reader.IsDBNull(reader.GetOrdinal("UserCreatedOn")) ? null : (DateTime?)reader["UserCreatedOn"])
						};
						//var a= vm.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
						vm.PostCreated = String.Format("{0:MM/dd/yyyy, HH:mm}", vm.CreatedDate);
						vm.UserCreated = String.Format("{0:MM/dd/yyyy, HH:mm}", vm.UserCreatedOn);
						dmCollection.Add(vm);
					}

					reader.Close();
					con.Close();
				}
			}

			return dmCollection;
		}
	}
}