using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;

namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // Get all subjects
        [HttpGet]
        public async Task<ActionResult<GenericResponseModel<IEnumerable<GetSubjectDto>>>> GetAllSubjectsAsync()
        {
            var response = await _subjectService.GetAllSubjectsAsync();
            return Ok(response);
        }

        // Get subject by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseModel<GetSubjectDto>>> GetSubjectByIdAsync(int id)
        {
            var response = await _subjectService.GetSubjectByIdAsync(id);
            if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("create")] // Ensure this route exists in your request
        public async Task<IActionResult> CreateSubjectAsync([FromBody] PostSubjectDto subjectDto)
        {
            var response = await _subjectService.CreateSubjectAsync(subjectDto);
            return StatusCode(response.StatusCode, response.Data);
        }

        // Update an existing subject
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateSubjectAsync(int id, [FromBody] UpdateSubjectDto subjectDto)
        {
            var response = await _subjectService.UpdateSubjectAsync(id, subjectDto);
            if (response.StatusCode == 404)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // Delete a subject by ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteSubjectAsync(int id)
        {
            var response = await _subjectService.DeleteSubjectAsync(id);
            if (response.StatusCode == 400)
            {
                return BadRequest(response);
            }
            return NoContent();
        }
    }
}
