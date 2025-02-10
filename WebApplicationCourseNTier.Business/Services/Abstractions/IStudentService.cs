using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;

using WebApplicationCourseNTier.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using WebApplicationCourseNTier.DataAccess.Entities;
using System.Diagnostics.SymbolStore;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface IStudentService
    {
        Task<Student> GetByIdAsync(int id);
        Task<GenericResponseModel<bool>> CreateStudentApiAsync(PostStudentDto postStudentDto);
        // Method to create a new student
        //Task<GenericResponseModel<PostStudentDto>> CreateStudentAsync(PostStudentDto studentDto, IFormFile studentFile);
        Task<GenericResponseModel<bool>> AddAsync(PostStudentDto postStudentDto);
        Task<bool> UpdateStudentAsync(int studentId,UpdateStudentDto updateStudentDto);

        Task<GenericResponseModel<PaginationResponse<GetStudentDto>>> GetAllStudentsApiAsync(PaginationRequest paginationRequest);
        //Task<List<GetStudentDto>> GetAllStudentsAsync();
        //Task<List<GetStudentDto>> GetAll(GetStudentDto getStudentDto);
        Task<GenericResponseModel<PaginationResponse<GetStudentDto>>> GetAll(PaginationRequest paginationRequest);
        Task<GenericResponseModel<bool>> DeleteStudentAsync(int id);
        Task<GenericResponseModel<bool>> DeleteMvcAsync(int id);
    }
}
