using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Collections.Generic;

using ForumApp.Models;

namespace ForumApp.Controllers
{
	public class ThreadsController : Controller
	{
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
			ConnectionStrings["ConnectionString"].ConnectionString;

		public ViewResult Details(int id){
			PostsController controller = new PostsController();

			return View("Details", controller.GetDataCollection(id));
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