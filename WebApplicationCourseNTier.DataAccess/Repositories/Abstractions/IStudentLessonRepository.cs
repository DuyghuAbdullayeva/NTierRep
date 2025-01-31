using WebApplicationCourseNTier.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IStudentLessonRepository : IRepository<StudentLesson>
    {
        Task<StudentLesson> GetStudentLessonWithDetailsAsync(int id);
        Task<IEnumerable<StudentLesson>> GetAllStudentLessonsWithDetailsAsync();
    }
}
