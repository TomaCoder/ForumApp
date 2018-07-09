using System;

namespace ForumApp.Models
{
	public class PostViewModel
	{
		public int PostID { get; set; }
		public int UserID { get; set; }
		public int ThreadID { get; set; }
		public int? NumPosts { get; set; }
		public string Text { get; set; }
		public string NickName { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UserCreatedOn { get; set; }
	}
}