using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface ITopicRepository : IRepository<Topic>
    {
        Task<Topic> GetByIdWithLessonsAsync(int id);

        // Fetch all topics by a given subject ID
       

        // Check if a topic exists based on its ID
        Task<bool> TopicExistsAsync(int id);
    }
}
