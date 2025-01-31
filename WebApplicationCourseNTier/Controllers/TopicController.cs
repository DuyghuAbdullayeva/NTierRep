using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.Topic;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;

namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        // GET api/topic/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseModel<GetTopicDto>>> GetTopicByIdAsync(int id)
        {
            var response = await _topicService.GetTopicByIdAsync(id);
            return StatusCode(response.StatusCode, response.Data);  // Return topic details by ID with appropriate status code
        }

        // POST api/topic
        [HttpPost]
        public async Task<ActionResult<GenericResponseModel<PostTopicDto>>> CreateTopicAsync([FromBody] PostTopicDto topicDto)
        {
            var response = await _topicService.CreateTopicAsync(topicDto);
            return StatusCode(response.StatusCode, response.Data);  // Respond after creating the topic
        }

        // PUT api/topic/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateTopicAsync(int id, [FromBody] UpdateTopicDto topicDto)
        {
            var response = await _topicService.UpdateTopicAsync(id, topicDto);
            return StatusCode(response.StatusCode, response.Data);  // Respond after updating the topic
        }

        // DELETE api/topic/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteTopicAsync(int id)
        {
            var response = await _topicService.DeleteTopicAsync(id);
            return StatusCode(response.StatusCode, response.Data);  // Respond after deleting the topic
        }

        // GET api/topic
        [HttpGet]
        public async Task<ActionResult<GenericResponseModel<IEnumerable<GetTopicDto>>>> GetAllTopicsAsync()
        {
            var response = await _topicService.GetAllTopicsAsync();
            return StatusCode(response.StatusCode, response.Data);  // Respond with all topics
        }
    }
}
