using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class StudentLessonRepository : Repository<StudentLesson>, IStudentLessonRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public StudentLessonRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StudentLesson> GetStudentLessonWithDetailsAsync(int id)
        {
            return await _context.StudentLessons
                .Include(sl => sl.Lesson)
                .Include(sl => sl.Student)
                .FirstOrDefaultAsync(sl => sl.Id == id);
        }

        public async Task<IEnumerable<StudentLesson>> GetAllStudentLessonsWithDetailsAsync()
        {
            return await _context.StudentLessons
                .Include(sl => sl.Lesson)
                .Include(sl => sl.Student)
                .ToListAsync();
        }
    }
}
