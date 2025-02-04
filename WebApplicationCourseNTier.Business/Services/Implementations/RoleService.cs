using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task SeedRolesAsync()
        {
            await _roleRepository.SeedRolesAsync();
        }

        public async Task AddRoleToUserAsync(User user, string role)
        {
            await _roleRepository.AddRoleToUserAsync(user, role);
        }

        public async Task RemoveRoleFromUserAsync(User user, string role)
        {
            await _roleRepository.RemoveRoleFromUserAsync(user, role);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string role)
        {
            return await _roleRepository.IsUserInRoleAsync(user, role);
        }
    }
}