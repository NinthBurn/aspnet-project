using System.ComponentModel.DataAnnotations;

namespace Lab10.Models
{
	public class UserRegisterViewModel
	{

		[Required(ErrorMessage = "Please enter your email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter your user name.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter your password.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Please enter your password.")]
		public string ConfirmPassword { get; set; }
	}
}
