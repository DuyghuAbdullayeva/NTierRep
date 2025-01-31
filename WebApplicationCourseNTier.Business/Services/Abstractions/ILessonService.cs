using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface ILessonService
    {
        Task<GenericResponseModel<GetLessonDto>> GetLessonByIdAsync(int id);
        Task<GenericResponseModel<PostLessonDto>> CreateLessonAsync(PostLessonDto lessonDto);
        Task<GenericResponseModel<bool>> UpdateLessonAsync(int id, UpdateLessonDto lessonDto);
        Task<GenericResponseModel<bool>> DeleteLessonAsync(int id);
        Task<GenericResponseModel<IEnumerable<GetLessonDto>>> GetAllLessonsAsync();
        Task<GenericResponseModel<IEnumerable<GetLessonDto>>> GetLessonsByGroupIdAsync(int groupId);
    }
}
