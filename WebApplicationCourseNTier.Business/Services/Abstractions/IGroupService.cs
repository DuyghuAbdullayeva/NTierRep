using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.GroupDTOs;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;

using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface IGroupService
    {
        Task<GenericResponseModel<GetGroupDto>> GetGroupByIdAsync(int id);
        Task<IEnumerable<string>> GetAllGroupNamesAsync();
    
        Task<GenericResponseModel<PostGroupDto>> CreateGroupAsync(PostGroupDto groupDto);
        Task<GenericResponseModel<bool>> UpdateGroupAsync(int id, UpdateGroupDto groupDto);
        Task<GenericResponseModel<bool>> DeleteGroupAsync(int id);
        Task<GenericResponseModel<PaginationResponse<GetGroupDto>>> GetAllGroupsAsync(PaginationRequest paginationRequest);
    }
}
