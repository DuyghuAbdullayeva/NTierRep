using Microsoft.AspNetCore.Identity;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IRoleRepository
    {
        Task SeedRolesAsync();
        Task<IdentityResult> AddRoleToUserAsync(User user, string role);
        Task<IdentityResult> RemoveRoleFromUserAsync(User user, string role);
        Task<bool> IsUserInRoleAsync(User user, string role);
    }
}