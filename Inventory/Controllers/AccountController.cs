using Inventory.DB.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Configuration;

namespace Inventory.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View("Register");
		}
		[HttpPost]
		public async Task<IActionResult> SaveRegister(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				User appuser = new User();
				appuser.UserName = registerViewModel.UserName;
				appuser.PasswordHash = registerViewModel.UserName;

				IdentityResult result = await userManager.CreateAsync(appuser, registerViewModel.Password);
				if (result.Succeeded)
				{
					//cookie will created
					await signInManager.SignInAsync(appuser, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}

			}
			return View("Register");
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View("login");
		}

		[HttpPost]
		public async Task<IActionResult> SaveLogin(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid == true)
			{
				User appuser =
					await userManager.FindByNameAsync(loginViewModel.UserName);
				if (appuser != null)
				{
					bool found =
							await userManager.CheckPasswordAsync(appuser, loginViewModel.Password);
					if (found == true)
					{
						await signInManager.SignInAsync(appuser, loginViewModel.RememberMe);
						return RedirectToAction("Index", "Home");
					}
				}
				ModelState.AddModelError("", "Username Or Password Wrong");

			}
			return View("Login", loginViewModel);
		}
		public async Task<IActionResult>SignOut()
		{
			await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
	}
}
