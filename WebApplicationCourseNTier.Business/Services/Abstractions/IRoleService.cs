using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface IRoleService
    {
        Task SeedRolesAsync();
        Task AddRoleToUserAsync(User user, string role);
        Task RemoveRoleFromUserAsync(User user, string role);
        Task<bool> IsUserInRoleAsync(User user, string role);
    }
}