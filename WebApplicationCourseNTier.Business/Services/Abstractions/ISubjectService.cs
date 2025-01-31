using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface ISubjectService
    {
        Task<GenericResponseModel<GetSubjectDto>> GetSubjectByIdAsync(int id);
        Task<GenericResponseModel<IEnumerable<GetSubjectDto>>> GetAllSubjectsAsync();
        Task<GenericResponseModel<PostSubjectDto>> CreateSubjectAsync(PostSubjectDto subjectDto);
        Task<GenericResponseModel<bool>> UpdateSubjectAsync(int id, UpdateSubjectDto subjectDto);
        Task<GenericResponseModel<bool>> DeleteSubjectAsync(int id);

    }
}
