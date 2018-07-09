using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Mvc;

using ForumApp.Models;

namespace ForumApp.Controllers
{
	public class TopicsController : Controller
	{
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
			ConnectionStrings["ConnectionString"].ConnectionString;

		public ViewResult Details(int id) // topicid
		{
			ThreadsController controller = new ThreadsController();

			return View("Details", controller.GetDataCollection(id));
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