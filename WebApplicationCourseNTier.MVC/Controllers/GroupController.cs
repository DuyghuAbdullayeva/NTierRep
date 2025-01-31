using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.GroupDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.Services.Implementations;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.MVC.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        private readonly IUserService _userService;
        public GroupController(IGroupService groupService,IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        public IActionResult Create()
        {
            var model = new PostGroupDto();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostGroupDto groupDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _groupService.CreateGroupAsync(groupDto);

                if (response.StatusCode == 201)
                {
                    return RedirectToAction("All");
                }

                ModelState.AddModelError(string.Empty, "There was an error creating the group.");
            }

            return View(groupDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _groupService.GetGroupByIdAsync(id);

            if (response.Data == null)
            {
                return NotFound();
            }

            var editGroupDto = new UpdateGroupDto
            {
                Name = response.Data.Name,
                TeacherName = response.Data.TeacherName,
                SubjectName = response.Data.SubjectName
            };

            return View(editGroupDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateGroupDto groupDto)
        {
            if (!ModelState.IsValid)
            {
                return View(groupDto);
            }

            var response = await _groupService.UpdateGroupAsync(id, groupDto);

            if (response.StatusCode == 200)
            {
                return RedirectToAction("All");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the group.");
            return View(groupDto);
        }

        [HttpGet]
       
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _groupService.DeleteGroupAsync(id);
            return RedirectToAction("All");
        }
        [HttpGet]
        public async Task<IActionResult> All(int pageNumber = 1, int pageSize = 2)
        {
            if (!await _userService.IsUserLoggedInAsync())
            {
                return RedirectToAction("Login", "Account");
            }
            var paginationRequest = new PaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

      
            var paginationResponse = await _groupService.GetAllGroupsAsync(paginationRequest);

            if (paginationResponse.StatusCode == 200)
            {
                
                ViewData["PaginationResponse"] = paginationResponse.Data;
                ViewData["PaginationRequest"] = paginationRequest;

                return View(paginationResponse.Data); 
            }

         
            return View("Error");
        }






    }
}
