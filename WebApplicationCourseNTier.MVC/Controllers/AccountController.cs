using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.MVC.Models;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UsersApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(IUserService userService, RoleManager<Role> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ValidateLoginAsync(model.Email, model.Password);

                if (result.Succeeded)
                {
                    await _userService.SignInUserAsync(model.Email, model.RememberMe);
                    return RedirectToAction("All", "Student");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }

        // Register GET
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");

            return View();
        }

        // Register POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userService.CreateUserAsync(user, model.Password);

                if (!string.IsNullOrEmpty(model.Role) && (model.Role == "Admin" || model.Role == "Manager"))
                {
                    await _userService.AddUserToRoleAsync(user, model.Role);
                }

                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        // Verify Email GET
        public IActionResult VerifyEmail()
        {
            return View();
        }

        // Verify Email POST
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindUserByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
                }
            }
            return View(model);
        }

        // Change Password GET
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        // Change Password POST
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindUserByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userService.RemoveUserPasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userService.AddUserPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}