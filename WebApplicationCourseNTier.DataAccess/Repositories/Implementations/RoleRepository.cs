using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleRepository(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new[] { "Admin", "Manager" };

            foreach (var roleName in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new Role { Name = roleName, NormalizedName = roleName.ToUpper() });
                }
            }
        }

        public async Task<IdentityResult> AddRoleToUserAsync(User user, string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                return await _userManager.AddToRoleAsync(user, role);
            }

            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });
        }

        public async Task<IdentityResult> RemoveRoleFromUserAsync(User user, string role)
        {
            if (await _roleManager.RoleExistsAsync(role))
            {
                return await _userManager.RemoveFromRoleAsync(user, role);
            }

            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });
        }

        public async Task<bool> IsUserInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}