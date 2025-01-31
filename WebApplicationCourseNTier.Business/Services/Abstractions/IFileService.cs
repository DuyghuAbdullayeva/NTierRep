
using CourseSystem.Dtos.File;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.Business.Services.Abstractions
{
    public interface IFileService
    {

        Task Upload(IFormFile formFile, int studentId);
        Task<FileDetailDto> Download(int studentId);
    }
}
