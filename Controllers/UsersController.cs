using Lab10.Data;
using Lab10.Models;
using Lab10.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Lab10.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId != null)
			{
				return RedirectToAction("Index", "UserLogs");
			}

			return RedirectToAction("Login", "Users");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();

			return RedirectToAction("Login", "Users");
		}

		[HttpGet]
        public IActionResult Login() {
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId != null)
			{
				return RedirectToAction("Index", "UserLogs");
			}

			return View(); 
        }

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginViewModel viewModel)
		{
            User? user = context.Users.SingleOrDefault(user => user.Email == viewModel.Email);

			if (user != null && user.Password == viewModel.Password)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);

				return RedirectToAction("Index", "UserLogs");
			}

			return RedirectToAction("Login", "Users");
		}

		[HttpGet]
		public IActionResult Register()
		{
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId != null)
			{
				return RedirectToAction("Index", "UserLogs");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
		{
			User? user = context.Users.SingleOrDefault(user => user.Email == viewModel.Email);

			if (user == null)
			{
				User newUser = new User
				{
					Email = viewModel.Email,
					Password = viewModel.Password,
					Name = viewModel.Name,
				};

				await context.Users.AddAsync(newUser);
				await context.SaveChangesAsync();

				return RedirectToAction("Login", "Users");
			}

			return RedirectToAction("Register", "Users");
		}
	}
}
