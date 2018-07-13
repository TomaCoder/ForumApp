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
	public class TopicsController : Controller
	{
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
			ConnectionStrings["ConnectionString"].ConnectionString;

		public ViewResult Details(int id) // topicid
		{
			ThreadsController controller = new ThreadsController();
			ViewBag.topicDetails = GetDataRecord(id);

			return View("Details", controller.GetDataCollection(id));
		}

		[FormatException]
		[Authorization("Admin")]
		public JsonResult AddTopic(TopicViewModel vm)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand("InsertTopic", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = (int)System.Web.HttpContext.Current.Session["UserID"];
						cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = vm.Name;

						con.Open();
						SqlDataReader reader = cmd.ExecuteReader();
						while (reader.Read())
						{
							vm = new TopicViewModel
							{
								TopicID = (int)reader["TopicID"],
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
			catch (Exception ex)
			{
				throw new HttpException("You don't have access. Please login to continue", ex);
			}

			return this.Json(vm);
		}

		public TopicViewModel GetDataRecord(int topicID)
		{
			TopicViewModel vm = null;
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("GetTopicDetails", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@TopicID", SqlDbType.Int).Value = topicID;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						vm = new TopicViewModel
						{
							TopicID = (int)reader["TopicID"],
							UserID = (int)reader["UserID"],
							Name = reader["Name"].ToString(),
							CreatedDate = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["CreatedDate"])
						};
					}

					reader.Close();
					con.Close();
				}
			}

			return vm;
		}

		public List<TopicViewModel> GetDataCollection(int? id = null)
		{
			List<TopicViewModel> dmCollection = new List<TopicViewModel>();
			using (SqlConnection con = new SqlConnection(ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand("GetTopics", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					con.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						TopicViewModel vm = new TopicViewModel
						{
							TopicID = (int)reader["TopicID"],
							UserID = (int)reader["UserID"],
							NumPosts = (int?)reader["NumPosts"],
							Name = reader["Name"].ToString(),
							NickName = reader["NickName"].ToString(),
							ThreadName = reader["ThreadName"].ToString(),
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