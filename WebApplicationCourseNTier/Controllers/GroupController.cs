using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.GroupDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET api/group/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericResponseModel<GetGroupDto>>> GetGroupByIdAsync(int id)
        {
            var response = await _groupService.GetGroupByIdAsync(id);
            if (response.Data == null)
            {
                return StatusCode(response.StatusCode, response.Data); 
            }
            return StatusCode(response.StatusCode, response.Data); 
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponseModel<PaginationResponse<GetGroupDto>>>> GetAllGroupsAsync([FromQuery] PaginationRequest paginationRequest)
        {
            // Ensure page number and page size are valid
            if (paginationRequest.PageNumber <= 0 || paginationRequest.PageSize <= 0)
            {
                return BadRequest(new GenericResponseModel<PaginationResponse<GetGroupDto>>
                {
                    StatusCode = 400,
                    Data = null
                });
            }

            // Call the service to fetch the paginated groups
            var response = await _groupService.GetAllGroupsAsync(paginationRequest);

            if (response.StatusCode == 200)
            {
                return Ok(response); // Return paginated data with a 200 OK status
            }

            return NotFound(response); // If no groups found or an error occurs
        }




        [HttpPost]
        public async Task<ActionResult<GenericResponseModel<PostGroupDto>>> CreateGroupAsync([FromBody] PostGroupDto groupDto)
        {
            var response = await _groupService.CreateGroupAsync(groupDto);
            return StatusCode(response.StatusCode, response.Data); 
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateGroupAsync(int id, [FromBody] UpdateGroupDto groupDto)
        {
            var response = await _groupService.UpdateGroupAsync(id, groupDto);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteGroupAsync(int id)
        {
            var response = await _groupService.DeleteGroupAsync(id);
            return StatusCode(response.StatusCode, response.Data); 
        }
    }
}
