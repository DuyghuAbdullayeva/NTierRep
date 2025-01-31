using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // GET api/lesson/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseModel<GetLessonDto>>> GetLessonByIdAsync(int id)
        {
            var response = await _lessonService.GetLessonByIdAsync(id);
            return StatusCode(response.StatusCode, response.Data);  // Return lesson by ID with appropriate status code
        }

        // POST api/lesson
        [HttpPost]
        public async Task<ActionResult<GenericResponseModel<PostLessonDto>>> CreateLessonAsync([FromBody] PostLessonDto lessonDto)
        {
            var response = await _lessonService.CreateLessonAsync(lessonDto);
            return StatusCode(response.StatusCode, response.Data);  // Respond after creating the lesson
        }

        // PUT api/lesson/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateLessonAsync(int id, [FromBody] UpdateLessonDto lessonDto)
        {
            var response = await _lessonService.UpdateLessonAsync(id, lessonDto);
            return StatusCode(response.StatusCode, response.Data);  // Respond after updating the lesson
        }

        // DELETE api/lesson/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteLessonAsync(int id)
        {
            var response = await _lessonService.DeleteLessonAsync(id);
            return StatusCode(response.StatusCode, response.Data);  // Respond after deleting the lesson
        }

        // GET api/lesson
        [HttpGet]
        public async Task<ActionResult<GenericResponseModel<IEnumerable<GetLessonDto>>>> GetAllLessonsAsync()
        {
            var response = await _lessonService.GetAllLessonsAsync();
            return StatusCode(response.StatusCode, response.Data);  // Respond with all lessons
        }

        // GET api/lesson/group/{groupId}
        [HttpGet("group/{groupId}")]
        public async Task<ActionResult<GenericResponseModel<IEnumerable<GetLessonDto>>>> GetLessonsByGroupIdAsync(int groupId)
        {
            var response = await _lessonService.GetLessonsByGroupIdAsync(groupId);
            return StatusCode(response.StatusCode, response.Data);  // Respond with lessons filtered by group ID
        }
    }
}
