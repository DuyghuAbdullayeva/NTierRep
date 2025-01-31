using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;


namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<PaginationResponse<Group>> GetAllGroupAsync(PaginationRequest paginationRequest);


        Task<Group> GetGroupWithAllDetailsAsync(int groupId);
        Task<Group> GetByNameAsync(string name);
        Task<List<Group>> GetAllGroupsWithoutPaginationAsync();
    }
}
