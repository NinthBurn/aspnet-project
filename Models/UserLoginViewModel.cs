using System.ComponentModel.DataAnnotations;

namespace Lab10.Models
{
	public class UserLoginViewModel
	{
		[Required(ErrorMessage = "Please enter your email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter your password.")]
		public string Password { get; set; }
	}
}
