using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
using WebApplicationCourseNTier.Business.DTOs.User;
using WebApplicationCourseNTier.Business.Services.Abstractions;

namespace WebApplicationCourseNTier.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public UserController(IAppUserService userService)
        {
            _appUserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
          await  _appUserService.AddAsync(registerUserDto);
            return Ok();

        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GenericResponseModel<GetTeacherDto>>> GetTeacherByIdAsync(int id)
        //{
        //    var response = await _teacherService.GetTeacherByIdAsync(id);

        //    return StatusCode(response.StatusCode, response.Data);
        //}

        //[HttpGet]
        //public async Task<ActionResult<GenericResponseModel<IEnumerable<GetTeacherDto>>>> GetAllTeachersAsync()
        //{
        //    var response = await _teacherService.GetAllTeachersAsync();
        //    return StatusCode(response.StatusCode, response.Data);
        //}

        //[HttpPost]
        //public async Task<ActionResult<GenericResponseModel<PostTeacherDto>>> CreateTeacherAsync(PostTeacherDto teacherDto)
        //{
        //    var response = await _teacherService.CreateTeacherAsync(teacherDto);

        //    return StatusCode(response.StatusCode, response.Data);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<GenericResponseModel<bool>>> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        //{
        //    var response = await _teacherService.UpdateTeacherAsync(id, teacherDto);

        //    return StatusCode(response.StatusCode, response.Data);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<GenericResponseModel<bool>>> DeleteTeacherAsync(int id)
        //{
        //    var response = await _teacherService.DeleteTeacherAsync(id);

        //    return StatusCode(response.StatusCode, response.Data);
        //}
    }
}
