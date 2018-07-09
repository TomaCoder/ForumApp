using System.ComponentModel.DataAnnotations;

namespace ForumApp.Models
{
	public class LoginViewModel
	{
		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
	}

	public class RegisterViewModel
	{
		[Required]
		[StringLength(256, ErrorMessage = "Fill in the {0} field.", MinimumLength = 1)]
		[Display(Name = "Nick Name")]
		public string NickName { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Country")]
		public string Country { get; set; }

		[Display(Name = "City")]
		public string City { get; set; }
	}
}