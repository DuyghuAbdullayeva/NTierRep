
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;

namespace WebApplicationCourseNTier.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private IUserRepository _userRepository;

        public UserService(SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository userRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _userRepository.FindUserByEmailAsync(email);
        }

        public async Task<IdentityResult> RemoveUserPasswordAsync(User user)
        {
            return await _userRepository.RemovePasswordAsync(user);
        }


        public async Task<IdentityResult> AddUserPasswordAsync(User user, string newPassword)
        {
            return await _userRepository.AddPasswordAsync(user, newPassword);
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);
            return user != null;
        }


        public async Task<IdentityResult> RegisterUserAsync(UserRegistrationModel model)  
        {
            var user = new User
            {
                FullName = model.FullName,   
                Email = model.Email,
                UserName = model.UserName
            };

            
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
        public async Task<SignInResult> ValidateLoginAsync(string email, string password)
        {
            return await _userRepository.ValidateUserCredentialsAsync(email, password);
        }
        public async Task SignInUserAsync(string email, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _signInManager.SignInAsync(user, rememberMe);
            }
        }
     
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
