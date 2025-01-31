using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<Lesson> GetLessonWithDetailsAsync(int id);  // A method to get lesson with related students and topics
        Task<IEnumerable<Lesson>> GetLessonsByGroupIdAsync(int groupId);  // Get lessons for a specific group

    }
}
