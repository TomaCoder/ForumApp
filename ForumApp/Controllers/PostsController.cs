using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Mvc;

using ForumApp.Models;

namespace ForumApp.Controllers
{
	public class PostsController : Controller
	{
		// GET: Posts
		private readonly string ConnectionString = System.Configuration.ConfigurationManager.
			  ConnectionStrings["ConnectionString"].ConnectionString;

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
							UserCreatedOn = (reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? null : (DateTime?)reader["UserCreatedOn"])
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