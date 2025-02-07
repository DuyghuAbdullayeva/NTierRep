using Microsoft.AspNetCore.Identity;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<SignInResult> ValidateUserCredentialsAsync(string email, string password);
        Task<User> FindUserByEmailAsync(string email);
        Task<IdentityResult> RemovePasswordAsync(User user);
        Task<IdentityResult> AddPasswordAsync(User user, string newPassword);
    }

}
