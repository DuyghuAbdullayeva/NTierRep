using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface ITeacherService
    {
        Task<GenericResponseModel<GetTeacherDto>> GetTeacherByIdAsync(int id);
        Task<GenericResponseModel<PostTeacherDto>> CreateTeacherAsync(PostTeacherDto teacherDto);
        Task<GenericResponseModel<bool>> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto);
        Task<GenericResponseModel<bool>> DeleteTeacherAsync(int id);
        Task<GenericResponseModel<IEnumerable<GetTeacherDto>>> GetAllTeachersAsync();
    }
}
