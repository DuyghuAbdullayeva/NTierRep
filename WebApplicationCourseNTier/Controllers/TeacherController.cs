using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;


namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseModel<GetTeacherDto>>> GetTeacherByIdAsync(int id)
        {
            var response = await _teacherService.GetTeacherByIdAsync(id);
          
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseModel<IEnumerable<GetTeacherDto>>>> GetAllTeachersAsync()
        {
            var response = await _teacherService.GetAllTeachersAsync();
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponseModel<PostTeacherDto>>> CreateTeacherAsync(PostTeacherDto teacherDto)
        {
            var response = await _teacherService.CreateTeacherAsync(teacherDto);
   
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        {
            var response = await _teacherService.UpdateTeacherAsync(id, teacherDto);
      
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteTeacherAsync(int id)
        {
            var response = await _teacherService.DeleteTeacherAsync(id);
   
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
