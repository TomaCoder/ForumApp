namespace ForumApp.Models
{
	public class ApplicationUser
	{
		public int UserID { get; set; }
		public string NickName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Role { get; set; }
	}
}