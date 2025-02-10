using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.Business.Services.Implementations;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.MVC.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;

namespace WebApplicationCourseNTier.MVC.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public IActionResult Create()
        {
            var model = new PostLessonDto();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostLessonDto lessonDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _lessonService.CreateLessonAsync(lessonDto);

                if (response.StatusCode == 201)
                {
                    return RedirectToAction("All");
                }

                ModelState.AddModelError(string.Empty, "There was an error creating the lesson.");
            }

            return View(lessonDto);
        }

        public async Task<IActionResult> All(PaginationRequest paginationRequest)
        {
            var response = await _lessonService.GetAllLessonsAsync(paginationRequest);

            if (response == null || response.Data == null)
            {
                return View("Error");
            }

            var paginationResponse = response.Data;

            PaginationViewModel<GetLessonDto> viewModel = new()
            {
                Values = paginationResponse.Data,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalCount = paginationResponse.TotalCount
            };

            return View("All", viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _lessonService.GetLessonByIdAsync(id);

            if (response.StatusCode == 404 || response.Data == null)
            {
                return View("Error");
            }

            var lessonDto = response.Data;

            var updateLessonDto = new UpdateLessonDto
            {
                Name = lessonDto.Name,
                StartDate = lessonDto.StartDate,
                EndDate = lessonDto.EndDate,
                GroupName = lessonDto.GroupName,

            };

            return View(updateLessonDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateLessonDto updateLessonDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _lessonService.UpdateLessonAsync(id, updateLessonDto);

                if (response.StatusCode == 200)
                {
                    return RedirectToAction("All");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating the lesson");
                }
            }

            return View(updateLessonDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _lessonService.GetLessonByIdAsync(id);

            if (response.StatusCode != 200 || response.Data == null)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _lessonService.DeleteLessonAsync(id);

            if (response.StatusCode == 200)
            {
                return RedirectToAction("All");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while deleting the lesson.");
            return RedirectToAction("All");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _lessonService.GetStudentLessonsByLessonIdAsync(id);

            if (response.Data == null)
            {
                return NotFound();
            }

            return View(response.Data);
        }
    }
}