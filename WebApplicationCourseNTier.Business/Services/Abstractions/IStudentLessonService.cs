using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface IStudentLessonService
    {
        Task<GenericResponseModel<GetStudentLessonDto>> GetStudentLessonByIdAsync(int id);
        Task<GenericResponseModel<PostStudentLessonDto>> CreateStudentLessonAsync(PostStudentLessonDto studentLessonDto);
        Task<GenericResponseModel<bool>> UpdateStudentLessonAsync(int id, UpdateStudentLessonDto studentlessonDto);
        Task<GenericResponseModel<bool>> DeleteStudentLessonAsync(int id);
        Task<GenericResponseModel<IEnumerable<GetStudentLessonDto>>> GetAllStudentLessonsAsync();
    }
}
