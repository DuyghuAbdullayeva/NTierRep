using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.Topic;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.Services.Abstractions;

namespace WebApplicationCourseNTier.Business.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITopicRepository _topicRepository;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _topicRepository =_unitOfWork.GetRepository<ITopicRepository>();
        }

        // Fetch topic by ID with lessons
        public async Task<GenericResponseModel<GetTopicDto>> GetTopicByIdAsync(int id)
        {
            var topic = await _topicRepository.GetByIdWithLessonsAsync(id);

            if (topic == null)
            {
                return new GenericResponseModel<GetTopicDto>
                {
                    StatusCode = 404,
                    Data = null
                };
            }

            var topicDto = _mapper.Map<GetTopicDto>(topic);
            topicDto.LessonIds = topic.LessonTopics.Select(lt => lt.LessonId).ToList();

            return new GenericResponseModel<GetTopicDto>
            {
                StatusCode = 200,
                Data = topicDto
            };
        }

        // Create a new topic
        public async Task<GenericResponseModel<PostTopicDto>> CreateTopicAsync(PostTopicDto topicDto)
        {
            var topic = _mapper.Map<Topic>(topicDto);

            // Check if topic already exists by its Name (assuming a name uniqueness constraint)
            var topicExists = await _topicRepository.GetAllAsync(t => t.Name == topic.Name && !t.IsDeleted);
            if (topicExists.Any())
            {
                return new GenericResponseModel<PostTopicDto>
                {
                    StatusCode = 400,
                    Data = topicDto
                };
            }

            // Add new topic
            await _topicRepository.AddAsync(topic);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<PostTopicDto>
            {
                StatusCode = 201,
                Data = topicDto
            };
        }

        // Update an existing topic
        public async Task<GenericResponseModel<bool>> UpdateTopicAsync(int id, UpdateTopicDto topicDto)
        {
            var existingTopic = await _topicRepository.GetByIdAsync(id);
            if (existingTopic == null)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404,
                    Data = false
                };
            }

            // Update topic properties
            existingTopic.Name = topicDto.Name;
          

            // Ensure that the lesson IDs are updated (example of handling related entities)
            existingTopic.LessonTopics = topicDto.LessonIds.Select(lessonId => new LessonTopic
            {
                TopicId = id,
                LessonId = lessonId
            }).ToList();

            var updateResult = await _topicRepository.Update(existingTopic);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = updateResult ? 200 : 400,
                Data = updateResult
            };
        }

        // Delete a topic by ID
        public async Task<GenericResponseModel<bool>> DeleteTopicAsync(int id)
        {
            var existingTopic = await _topicRepository.GetByIdAsync(id);
            if (existingTopic == null)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404,
                    Data = false
                };
            }

            var deleteResult = await _topicRepository.RemoveByIdAsync(id);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = deleteResult ? 200 : 400,
                Data = deleteResult
            };
        }

        // Fetch all topics
        public async Task<GenericResponseModel<IEnumerable<GetTopicDto>>> GetAllTopicsAsync()
        {
            var topics = await _topicRepository.GetAllAsync(t => !t.IsDeleted);
            var topicDtos = _mapper.Map<IEnumerable<GetTopicDto>>(topics);

            return new GenericResponseModel<IEnumerable<GetTopicDto>>
            {
                StatusCode = 200,
                Data = topicDtos
            };
        }
    }
}
