using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using CourseSystem.Dtos.File;

namespace CourseSystem.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _fileRepository = _unitOfWork.GetRepository<IFileRepository>(); 
        }

        public async Task Upload(IFormFile formFile, int studentId)
        {
            if (formFile == null || formFile.Length == 0)
            {
                throw new ArgumentException("No file provided or the file is empty.");
            }

            var fileDetails = new FileDetail()
            {
                FileName = $"{Path.GetFileNameWithoutExtension(formFile.FileName)}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}",
                Extension = Path.GetExtension(formFile.FileName),
                StudentId = studentId
            };

            
            string filePath = GetFilePath(fileDetails);

            try
            {
                
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while uploading the file: {ex.Message}");
            }

            fileDetails.FilePath = filePath;
            await _fileRepository.AddFileDetails(fileDetails);

           
            await _unitOfWork.CommitAsync();
        }

        private string GetFilePath(FileDetail fileDetails)
        {
            string folderPath = _configuration["configs:filePath"];
            string fullPath = Path.Combine(folderPath, "UploadFiles");

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            string fileName = $"{fileDetails.FileName}{fileDetails.Extension}";
            return Path.Combine(fullPath, fileName);
        }

    
        public async Task<FileDetailDto> Download(int studentId)
        {
            
            var fileExists = await CheckFileExists(studentId);
            if (!fileExists)
            {
                return null; 
            }

            
            var student = await _fileRepository.GetStudent(studentId);
            var fileDetail = student?.FileDetail?.FirstOrDefault(); 

            if (fileDetail != null)
            {
                return new FileDetailDto
                {
                    Data = await File.ReadAllBytesAsync(fileDetail.FilePath), 
                    Extension = fileDetail.Extension 
                };
            }

            return null;
        }

        public async Task<bool> CheckFileExists(int studentId)
        {
            var student = await _fileRepository.GetStudent(studentId);
            return student?.FileDetail?.Any() ?? false; 
        }
    }
}
