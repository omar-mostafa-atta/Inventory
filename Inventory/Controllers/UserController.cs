using Inventory.DB.ViewModels;
using Inventory.Service.Sevices.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Inventory.Controllers
{
	[Authorize]
	[Authorize(Roles = "admin")]

	public class UserController : Controller
    {
        private readonly IUserService _UserService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;

        public UserController(IUserService UserService, IPasswordHasher<User> passwordHasher, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signinManager)
        {
            _UserService = UserService;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _userManager = userManager;
            _signinManager = signinManager;
        }


        public List<IdentityRole> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }

        [Authorize]
        public IActionResult GetAll(string searchString)
        {
            var Users = _UserService.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                Users = Users.Where(x => x.UserName.Contains(searchString)).ToList();
            }
            return View(Users);
        }

		public async Task<IActionResult> GetByIdAsync(string id)
		{
			var User = _UserService.GetById(id);
            var roles = await _userManager.GetRolesAsync(User);
            ViewBag.RoleName = roles.FirstOrDefault(); 
            return View(User);
		}


		[HttpGet]
        public IActionResult Insert()
        {

            return View();
        }
        [HttpPost]

        public IActionResult Insert(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var user = new User
                {
                    UserName = viewModel.Name,
                    Email = viewModel.Email,
                    PasswordHash = viewModel.Password,
                    PhoneNumber = viewModel.Phone,
                    imageurl = viewModel.imageurl

                };

                _UserService.Insert(user);

                return RedirectToAction(nameof(GetAll));
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = _UserService.GetById(id);
            if (user == null)
            {
                return NotFound("This user doesn't exist");
            }

            var userRoles = await _userManager.GetRolesAsync(user); 

            var roles = GetAllRoles();
            var selectRole = roles.Select(role => new SelectListItem
            {
                Value = role.Name,
                Text = role.Name,
                Selected = userRoles.Contains(role.Name) 
            }).ToList();

            var viewModel = new UserViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Password = string.Empty,
                Roles = selectRole,
                imageurl = user.imageurl

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserViewModel viewModel)
        {

            var existingUser = _UserService.GetById(id);
            if (existingUser == null)
            {
                return NotFound("This User doesn't exist.");
            }

            existingUser.UserName = viewModel.Name;
            existingUser.Email = viewModel.Email;
            existingUser.PhoneNumber = viewModel.Phone;
            existingUser.imageurl = viewModel.imageurl;

            var currentRoles = await _userManager.GetRolesAsync(existingUser);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);

            }

            if (!string.IsNullOrEmpty(viewModel.role))
            {
                await _userManager.AddToRoleAsync(existingUser, viewModel.role);
            }
                //await _signinManager.RefreshSignInAsync(existingUser);

            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, viewModel.Password);
            }
            _UserService.Update(existingUser);

            return RedirectToAction(nameof(GetAll));
        }

		public IActionResult Delete(string id)
		{
			_UserService.Delete(id);
			return RedirectToAction(nameof(GetAll));
		}
	}
}
