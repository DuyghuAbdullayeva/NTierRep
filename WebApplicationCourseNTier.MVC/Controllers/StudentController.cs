using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Models;

using WebApplicationCourseNTier.MVC.Models;
using Newtonsoft.Json;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.Business.DTOs;
using WebApplicationCourseNTier.Business.Services.Implementations;

namespace WebApplicationCourseNTier.Controllers
{
    public class StudentController : Controller
    {


        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService,IUserService userService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _userService = userService;
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {

            var groupNames = await _groupService.GetAllGroupNamesAsync();

           
            var postStudentDto = new PostStudentDto
            {
                GroupNames = groupNames.ToList()
            };

            return View(postStudentDto);
        }

        // POST: /Student/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create( PostStudentDto postStudentDto)
        {
            
            var response = await _studentService.AddAsync(postStudentDto);

            if (response.StatusCode==201 || response.Data ==true)
            {
                return RedirectToAction("All");
            }



            return View("Error"); 
        }
        [HttpGet]
        public async Task<IActionResult> All(PaginationRequest paginationRequest)
        {
            if (!await _userService.IsUserLoggedInAsync())
            {
                return RedirectToAction("Login", "Account");
            }

            var response = await _studentService.GetAll(paginationRequest);

            if (response == null || response.Data == null)
            {
                return View("Error");
            }

            var paginationResponse = response.Data;

            PaginationViewModel<GetStudentDto> viewModel = new()
            {
                Values = paginationResponse.Data,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalCount = paginationResponse.TotalCount
            };

            return View("All", viewModel);
        }

    //public async Task<IActionResult> Create()
    //{
    //    // Pagination parameters (adjust as needed)
    //    var paginationRequest = new PaginationRequest
    //    {
    //        PageNumber = 1,
    //        PageSize = 10
    //    };

    //    // Fetch the groups asynchronously
    //    var response = await _groupService.GetAllGroupsAsync(paginationRequest);

    //    if (response.StatusCode == 200)
    //    {
    //        // Pass the group names to the view
    //        ViewData["AvailableGroups"] = response.Data.Data.Select(g => g.Name).ToList();
    //    }
    //    else
    //    {
    //        // Handle error if fetching groups fails
    //        ModelState.AddModelError(string.Empty, "An error occurred while fetching groups.");
    //    }

    //    // Return the view with an empty student DTO for the form
    //    return View(new PostStudentDto());
    //}

    //// POST: /Student/Create
    //[HttpPost]
    //public async Task<IActionResult> Create(PostStudentDto studentDto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var response = await _studentService.AddAsync(studentDto);

    //        if (response.StatusCode == 201)
    //        {
    //            return RedirectToAction("All");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "An error occurred while creating the student.");
    //        }
    //    }

    //    return View(studentDto);
    //}


    //public async Task<IActionResult> Edit(int id)
    //{
    //    var response = await _studentService.GetByIdAsync(id);

    //    if (response.StatusCode != 200 || response.Data == null)
    //    {
    //        return View("Error");
    //    }

    //    var updateStudentDto = new UpdateStudentDto
    //    {
    //        Id = response.Data.Id,
    //        Name = response.Data.Name,
    //        GroupNames = response.Data.GroupNames
    //    };

    //    return View(updateStudentDto);
    //}

    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateStudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                var updateResponse = await _studentService.UpdateStudentAsync(id, studentDto);

                if (updateResponse)
                {
                    return RedirectToAction("All");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the student.");
                }
            }

            return View(studentDto);
        }

        //[HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _studentService.DeleteStudentAsync(id);
            if (response.StatusCode == 200 && response.Data)
            {
                return RedirectToAction("All");
            }
            return View("Error");
        }
    }
}
