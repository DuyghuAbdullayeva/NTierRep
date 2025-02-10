//using Microsoft.AspNetCore.Mvc;
//using WebApplicationCourseNTier.DataAccess.Entities;
//using WebApplicationCourseNTier.MVC.Models;
//using WebApplicationCourseNTier.Business.Services.Abstractions;
//using System.Threading.Tasks;
//using WebApplicationCourseNTier.DataAccess.Models;

//namespace UsersApp.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IUserService _userService;

      
//        public AccountController(IUserService userService)
//        {
//            _userService = userService;
//        }

  
//        public IActionResult Login()
//        {
//            return View();
//        }

      
//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _userService.ValidateLoginAsync(model.Email, model.Password);

//                if (result.Succeeded)
//                {
//                    await _userService.SignInUserAsync(model.Email, model.RememberMe);
//                    return RedirectToAction("All","Student");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Email or password is incorrect.");
//                    return View(model);
//                }
//            }
//            return View(model);
//        }

//        // Register GET
//        public IActionResult Register()
//        {
//            return View();
//        }

//        // Register POST
//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new User
//                {
//                    FullName = model.Name,
//                    Email = model.Email,
//                    UserName = model.Email,
//                };

//                var result = await _userService.RegisterUserAsync(new UserRegistrationModel
//                {
//                    FullName = model.Name,
//                    Email = model.Email,
//                    UserName = model.Email,
//                    Password = model.Password
//                });

//                if (result.Succeeded)
//                {
//                    return RedirectToAction("Login", "Account");
//                }
//                else
//                {
//                    foreach (var error in result.Errors)
//                    {
//                        ModelState.AddModelError("", error.Description);
//                    }

//                    return View(model);
//                }
//            }
//            return View(model);
//        }

//        // Verify Email GET
//        public IActionResult VerifyEmail()
//        {
//            return View();
//        }

//        // Verify Email POST
//        [HttpPost]
//        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userService.FindUserByEmailAsync(model.Email);

//                if (user == null)
//                {
//                    ModelState.AddModelError("", "Something is wrong!");
//                    return View(model);
//                }
//                else
//                {
//                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
//                }
//            }
//            return View(model);
//        }

//        // Change Password GET
//        public IActionResult ChangePassword(string username)
//        {
//            if (string.IsNullOrEmpty(username))
//            {
//                return RedirectToAction("VerifyEmail", "Account");
//            }
//            return View(new ChangePasswordViewModel { Email = username });
//        }

//        // Change Password POST
//        [HttpPost]
//        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userService.FindUserByEmailAsync(model.Email);
//                if (user != null)
//                {
//                    var result = await _userService.RemoveUserPasswordAsync(user);
//                    if (result.Succeeded)
//                    {
//                        result = await _userService.AddUserPasswordAsync(user, model.NewPassword);
//                        return RedirectToAction("Login", "Account");
//                    }
//                    else
//                    {
//                        foreach (var error in result.Errors)
//                        {
//                            ModelState.AddModelError("", error.Description);
//                        }
//                        return View(model);
//                    }
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Email not found!");
//                    return View(model);
//                }
//            }
//            else
//            {
//                ModelState.AddModelError("", "Something went wrong. try again.");
//                return View(model);
//            }
//        }

//        // Logout
//        public async Task<IActionResult> Logout()
//        {
//            await _userService.LogoutAsync();
//            return RedirectToAction("Index", "Home");
//        }
//    }
//}
