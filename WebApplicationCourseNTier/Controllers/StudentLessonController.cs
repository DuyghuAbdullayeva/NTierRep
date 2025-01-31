using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonStudentController : ControllerBase
    {
        private readonly IStudentLessonService _studentLessonService;

        public LessonStudentController(IStudentLessonService studentLessonService)
        {
            _studentLessonService = studentLessonService;
        }

        // GET api/lessonstudent/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseModel<GetStudentLessonDto>>> GetStudentLessonByIdAsync(int id)
        {
            var response = await _studentLessonService.GetStudentLessonByIdAsync(id);
            return StatusCode(response.StatusCode, response.Data);  // Return student lesson by ID with appropriate status code
        }

        // POST api/lessonstudent
        [HttpPost]
        public async Task<ActionResult<GenericResponseModel<PostStudentLessonDto>>> CreateStudentLessonAsync([FromBody] PostStudentLessonDto studentLessonDto)
        {
            var response = await _studentLessonService.CreateStudentLessonAsync(studentLessonDto);
            return StatusCode(response.StatusCode, response.Data);  // Respond after creating the student lesson
        }

        // PUT api/lessonstudent/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateStudentLessonAsync(int id, [FromBody] UpdateStudentLessonDto studentLessonDto)
        {
            var response = await _studentLessonService.UpdateStudentLessonAsync(id, studentLessonDto);
            return StatusCode(response.StatusCode, response.Data);  // Respond after updating the student lesson
        }

        // DELETE api/lessonstudent/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteStudentLessonAsync(int id)
        {
            var response = await _studentLessonService.DeleteStudentLessonAsync(id);
            return StatusCode(response.StatusCode, response.Data);  // Respond after deleting the student lesson
        }

        // GET api/lessonstudent
        [HttpGet]
        public async Task<ActionResult<GenericResponseModel<IEnumerable<GetStudentLessonDto>>>> GetAllStudentLessonsAsync()
        {
            var response = await _studentLessonService.GetAllStudentLessonsAsync();
            return StatusCode(response.StatusCode, response.Data);  // Respond with all student lessons
        }
    }
}
