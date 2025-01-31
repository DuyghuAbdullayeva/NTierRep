using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using Microsoft.AspNetCore.Http;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IFileService _fileService;

        public StudentController(IStudentService studentService, IFileService fileService)
        {
            _studentService = studentService;
            _fileService = fileService;
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllStudents([FromQuery] PaginationRequest paginationRequest)
        {
            var response = await _studentService.GetAllStudentsApiAsync(paginationRequest);

            if (response == null || response.Data == null )
            {
                return NotFound("No students found.");
            }

            return Ok(response);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateStudent([FromForm] PostStudentDto postStudentDto)
        {
            var response = await _studentService.CreateStudentApiAsync(postStudentDto);

            if (response.Data)
            {
                
                return Ok(true); 
            }
            else
            {
                
                return BadRequest(false); 
            }
        }


        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> UpdateStudentAsync(int id, [FromBody] UpdateStudentDto studentDto)
        {
            var response = await _studentService.UpdateStudentAsync(id, studentDto);

            if (!response)
            {
                return NotFound(new GenericResponseModel<bool>
                {
                    StatusCode = 404,
                    Data = false,
                });
            }

            return Ok(new GenericResponseModel<bool>
            {
                StatusCode = 200,
                Data = true,
            });
        }
        //[HttpGet("downloadProfilePicture/{studentId}")]
        //public async Task<IActionResult> DownloadProfilePicture(int studentId)
        //{
        //    // Fayl detalını əldə edirik
        //    var fileResponse = await _fileService.Download(studentId);

        //    if (fileResponse == null)
        //    {
        //        return NotFound(); // Fayl tapılmadıqda
        //    }

        //    // Faylın adını və uzantısını əldə edirik
        //    var fileExtension = Path.GetExtension(fileResponse.FileName);
        //    var mimeType = fileExtension switch
        //    {
        //        ".jpg" => "image/jpeg",
        //        ".jpeg" => "image/jpeg",
        //        ".png" => "image/png",
        //        _ => "application/octet-stream" // Default olaraq
        //    };

        //    // Faylı yükləyirik və doğru adla təqdim edirik
        //    return File(fileResponse.Data, mimeType, fileResponse.FileName); // Faylın orijinal adı ilə
        //}




        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericResponseModel<bool>>> DeleteStudentAsync(int id)
        {
            var response = await _studentService.DeleteStudentAsync(id);
            return StatusCode(response.StatusCode, response.Data);
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GenericResponseModel<GetStudentDto>>> GetStudentByIdAsync( )
        //{
        //    var response = await _studentService.GetByIdAsync(id);
          
        //}


        //// GET: api/students
        //[HttpGet("All")]
        //public async Task<IActionResult> GetAllStudents([FromQuery] PaginationRequest paginationRequest)
        //{
        //    if (paginationRequest.PageNumber <= 0 || paginationRequest.PageSize <= 0)
        //    {
        //        return BadRequest(new GenericResponseModel<PaginationResponse<GetStudentDto>>
        //        {
        //            StatusCode = 400,
        //            Data = null
        //        });
        //    }

        //    var response = await _studentService.GetAllStudentsAsync(paginationRequest);
        //    if (response.StatusCode == 200)
        //    {
        //        return Ok(response);
        //    }

        //    return NotFound(response);
        //}
        //[HttpGet("All")]
        //public async Task<IActionResult> GetAllStudents([FromQuery] PaginationRequest paginationRequest)
        //{
        //    if (paginationRequest.PageNumber <= 0 || paginationRequest.PageSize <= 0)
        //    {
        //        return BadRequest(new GenericResponseModel<PaginationResponse<GetStudentDto>>
        //        {
        //            StatusCode = 400,
        //            Data = null
        //        });
        //    }

        //    // Call the StudentService to get data from the external API
        //    var response = await _studentService.GetAllStudentsAsync(paginationRequest);

        //    if (response.StatusCode == 200)
        //    {
        //        return Ok(response);  // If data retrieval was successful, return the data
        //    }

        //    return StatusCode(response.StatusCode, response);  // If there was an error, return error response
        //}

        //// POST: api/students/upload
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadFile(IFormFile file, int studentId)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest(new { message = "Fayl düzgün deyil." });
        //    }

        //    var folderPath = Path.Combine("wwwroot", "uploads", studentId.ToString());
        //    if (!Directory.Exists(folderPath))
        //    {
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    var filePath = Path.Combine(folderPath, file.FileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return Ok(new { FilePath = $"/uploads/{studentId}/{file.FileName}" });
        //}

        //// GET: api/students/download/{studentId}
        //[HttpGet("download/{studentId}")]
        //public async Task<IActionResult> DownloadFile(int studentId)
        //{
        //    var fileDto = await _fileService.DownloadAsync(studentId);
        //    if (fileDto == null)
        //    {
        //        return NotFound(new { message = "File not found." });
        //    }

        //    return File(fileDto.Data, "application/octet-stream", fileDto.FileName);
        //}
    }
}
