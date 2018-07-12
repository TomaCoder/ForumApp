using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Collections.Generic;

using ForumApp.Models;
using System.Web;
using ForumApp.Helpers;

namespace ForumApp.Controllers
{
	public class ThreadsController : Controller
	{
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
			ConnectionStrings["ConnectionString"].ConnectionString;

		public ViewResult Details(int id){
			PostsController controller = new PostsController();

			ViewBag.threadDetails = GetDataRecord(id);
			return View("Details", controller.GetDataCollection(id));
		}

		[FormatException]
		public JsonResult AddThread(ThreadViewModel vm)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand("InsertThread", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@TopicID", SqlDbType.Int).Value = vm.TopicID;
						cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = (int)System.Web.HttpContext.Current.Session["UserID"];
						cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = vm.Name;
						cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = vm.Description;

						con.Open();
						SqlDataReader reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							vm = new ThreadViewModel
							{
								TopicID = (int)reader["TopicID"],
								ThreadID = (int)reader["ThreadID"],
								UserID = (int)reader["UserID"],
								Name = reader["Name"].ToString(),
								CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"])
							};
						}

						reader.Close();
						con.Close();
					}
				}
			}
			catch(Exception ex){
				throw new HttpException("You don't have access. Please login to continue", ex);
			}

			return this.Json(vm);
		}

		public ThreadViewModel GetDataRecord(int threadID)
		{
			ThreadViewModel vm = null;
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("GetThreadDetails", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						vm = new ThreadViewModel
						{
							ThreadID = (int)reader["ThreadID"],
							TopicID = (int)reader["TopicID"],
							UserID = (int)reader["UserID"],
							Name = reader["Name"].ToString(),
							Description = reader["Description"].ToString(),
							CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"])
						};
					}

					reader.Close();
					con.Close();
				}
			}

			return vm;
		}

		public JsonResult StartThread(int threadID)
		{
			ThreadViewModel vm = null;
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("StartThread", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						vm = new ThreadViewModel
						{
							ThreadID = (int)reader["ThreadID"],
							TopicID = (int)reader["TopicID"],
							UserID = (int)reader["UserID"],
							Inactive = (reader.IsDBNull(reader.GetOrdinal("Inactive")) ? null : (bool?)reader["Inactive"]),
							Name = reader["Name"].ToString(),
							Description = reader["Description"].ToString(),
							CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"])
						};
					}

					reader.Close();
					con.Close();
				}
			}

			return Json(vm);
		}

		public JsonResult CloseThread(int threadID)
		{
			ThreadViewModel vm = null;
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("CloseThread", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						vm = new ThreadViewModel
						{
							ThreadID = (int)reader["ThreadID"],
							TopicID = (int)reader["TopicID"],
							UserID = (int)reader["UserID"],
							Inactive = (reader.IsDBNull(reader.GetOrdinal("Inactive")) ? null : (bool?)reader["Inactive"]),
							Name = reader["Name"].ToString(),
							Description = reader["Description"].ToString(),
							CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"])
						};
					}

					reader.Close();
					con.Close();
				}
			}

			return Json(vm);
		}

		public List<ThreadViewModel> GetDataCollection(int topicID)
		{
			List<ThreadViewModel> dmCollection = new List<ThreadViewModel>();
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("GetThreads", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@TopicID", SqlDbType.Int).Value = topicID;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						ThreadViewModel vm = new ThreadViewModel
						{
							ThreadID = (int)reader["ThreadID"],
							TopicID = (int)reader["TopicID"],
							UserID = (int)reader["UserID"],
							NumPosts = (int?)reader["NumPosts"],
							Name = reader["Name"].ToString(),
							NickName = reader["NickName"].ToString(),
							Description = reader["Description"].ToString(),
							Inactive = (reader.IsDBNull(reader.GetOrdinal("Inactive")) ? null: (bool?)reader["Inactive"]),
							CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"]),
							PostCreatedOn = (reader.IsDBNull(reader.GetOrdinal("PostCreatedOn")) ? null : (DateTime?)reader["PostCreatedOn"])
						};

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