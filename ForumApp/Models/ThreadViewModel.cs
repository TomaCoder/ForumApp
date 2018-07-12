using System;

namespace ForumApp.Models
{
	public class ThreadViewModel
	{
		public int ThreadID { get; set; }
		public int UserID { get; set; }
		public int TopicID { get; set; }
		public int? NumPosts { get; set; }
		public string Name { get; set; }
		public string NickName { get; set; }
		public string Description { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? PostCreatedOn { get; set; }
		public bool? Inactive { get; set; }
	}
}