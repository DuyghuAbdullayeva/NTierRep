using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface ILessonService
    {
        Task<GenericResponseModel<GetLessonDto>> GetLessonByIdAsync(int id);
        Task<GenericResponseModel<bool>> CreateLessonAsync(PostLessonDto postLessonDto);
        Task<GenericResponseModel<bool>> UpdateLessonAsync(int id, UpdateLessonDto lessonDto);
        Task<GenericResponseModel<bool>> DeleteLessonAsync(int id);
        Task<GenericResponseModel<PaginationResponse<GetLessonDto>>> GetAllLessonsAsync(PaginationRequest paginationRequest);
        Task<GenericResponseModel<IEnumerable<GetLessonDto>>> GetLessonsByGroupIdAsync(int groupId);
        Task<GenericResponseModel<List<GetStudentLessonDto>>> GetStudentLessonsByLessonIdAsync(int lessonId);
    }
}
