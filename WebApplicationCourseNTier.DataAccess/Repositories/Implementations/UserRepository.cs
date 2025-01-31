using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseSystemArcDBContext _context;
        private readonly UserManager<User> _userManager;

      
        public UserRepository(CourseSystemArcDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

     
        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

      
        public async Task<SignInResult> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result ? SignInResult.Success : SignInResult.Failed;
            }
            return SignInResult.Failed;
        }

      
        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

       
        public async Task<IdentityResult> RemovePasswordAsync(User user)
        {
            return await _userManager.RemovePasswordAsync(user);
        }

   
        public async Task<IdentityResult> AddPasswordAsync(User user, string newPassword)
        {
            return await _userManager.AddPasswordAsync(user, newPassword);
        }
    }
}
