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
    public class TopicRepository : Repository<Topic>, ITopicRepository { 
         private readonly CourseSystemArcDBContext _context;
    
        public TopicRepository(CourseSystemArcDBContext context) : base(context) {

            _context = context;
        }

        // Fetch the topic by its ID, including lessons and other relevant data
        public async Task<Topic> GetByIdWithLessonsAsync(int id)
        {
            return await _context.Topics
                .Include(t => t.LessonTopics)
                .ThenInclude(lt => lt.Lesson)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        // Fetch all topics for a given subject
       

        // Check if a topic exists based on its ID
        public async Task<bool> TopicExistsAsync(int id)
        {
            return await _context.Topics.AnyAsync(t => t.Id == id && !t.IsDeleted);
        }
    }
}
