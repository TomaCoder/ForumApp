using System;

namespace ForumApp.Models
{
	public class TopicViewModel
	{
		public int TopicID { get; set; }
		public int UserID { get; set; }
		public string Name { get; set; }
		public string NickName { get; set; }
		public string ThreadName { get; set; }
		public int? NumPosts { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? PostCreatedOn { get; set; }
	}
}