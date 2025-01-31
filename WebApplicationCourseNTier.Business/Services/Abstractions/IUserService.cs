using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface IUserService
    {
        Task<bool> IsUserLoggedInAsync();
        Task<IdentityResult> RegisterUserAsync(UserRegistrationModel model);
        Task<SignInResult> ValidateLoginAsync(string email, string password);
        Task SignInUserAsync(string email, bool rememberMe);
        Task<User> FindUserByEmailAsync(string email);
        Task<IdentityResult> RemoveUserPasswordAsync(User user);
        Task<IdentityResult> AddUserPasswordAsync(User user, string newPassword);
        Task LogoutAsync();
    }
}
