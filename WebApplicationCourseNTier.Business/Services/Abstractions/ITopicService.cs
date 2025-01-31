using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Topic;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface ITopicService
    {
        // Fetch a topic by its ID, including related lessons
        Task<GenericResponseModel<GetTopicDto>> GetTopicByIdAsync(int id);

        // Create a new topic
        Task<GenericResponseModel<PostTopicDto>> CreateTopicAsync(PostTopicDto topicDto);

        // Update an existing topic by its ID
        Task<GenericResponseModel<bool>> UpdateTopicAsync(int id, UpdateTopicDto topicDto);

        // Delete a topic by its ID
        Task<GenericResponseModel<bool>> DeleteTopicAsync(int id);

        // Fetch all topics
        Task<GenericResponseModel<IEnumerable<GetTopicDto>>> GetAllTopicsAsync();
    }
}
