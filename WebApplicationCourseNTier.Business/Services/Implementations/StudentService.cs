using AutoMapper;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApplicationCourseNTier.Business.Services.Implementations;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using WebApplicationCourseNTier.Business.DTOs.Student;


namespace WebApplicationCourseNTier.Business.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CourseSystemArcDBContext _courseSystemArcDBContext;
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        
        private readonly IFileRepository _fileRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;


        public StudentService(IUnitOfWork unitOfWork, IMapper mapper,
            CourseSystemArcDBContext courseSystemArcDBContext,
            IConfiguration configuration, IFileService fileService, IHttpClientFactory httpClient)
        {
            _unitOfWork = unitOfWork;
            _courseSystemArcDBContext = courseSystemArcDBContext;
            _fileService = fileService;
            _studentRepository = _unitOfWork.GetRepository<IStudentRepository>(); ;
            _groupRepository = _unitOfWork.GetRepository<IGroupRepository>();
           
            _fileRepository = _unitOfWork.GetRepository<IFileRepository>();
            _httpClient = httpClient.CreateClient();

            _mapper = mapper;
            _httpClient.BaseAddress = new Uri(configuration["StudentApi"]);


        }

        public async Task<GenericResponseModel<bool>> CreateStudentApiAsync(PostStudentDto postStudentDto)
        {
            
            var student = _mapper.Map<Student>(postStudentDto);
            student.GroupStudents = new List<GroupStudent>();

            
            foreach (var groupName in postStudentDto.GroupNames)
            {
                var group = await _groupRepository.GetByNameAsync(groupName);
                if (group == null)
                {
                    return new GenericResponseModel<bool>
                    {
                        StatusCode = 404,
                        Data = false 
                    };
                }
                if (group.IsDeleted)
                {
                    return new GenericResponseModel<bool>
                    {
                        StatusCode = 400,
                        Data = false 
                    };
                }

                student.GroupStudents.Add(new GroupStudent
                {
                    GroupId = group.Id,
                    Student = student
                });
            }

        
            await _studentRepository.AddAsync(student);
            await _unitOfWork.CommitAsync();

            if (postStudentDto.ProfilePicture != null)
            {
                await _fileService.Upload(postStudentDto.ProfilePicture, student.Id);
            }

            return new GenericResponseModel<bool>
            {
                StatusCode = 201,
                Data = true 
            };
        }

        public async Task<GenericResponseModel<bool>> AddAsync(PostStudentDto postStudentDto)
        {
            
            var formContent = new MultipartFormDataContent();

            
            formContent.Add(new StringContent(postStudentDto.Name), "Name");
            formContent.Add(new StringContent(postStudentDto.CreatedDate.ToString("yyyy-MM-dd")), "CreatedDate");

           
            foreach (var groupName in postStudentDto.GroupNames)
            {
                formContent.Add(new StringContent(groupName), "GroupNames[]");
            }

            if (postStudentDto.ProfilePicture != null)
            {
                var fileContent = new StreamContent(postStudentDto.ProfilePicture.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(postStudentDto.ProfilePicture.ContentType);
                formContent.Add(fileContent, "ProfilePicture", postStudentDto.ProfilePicture.FileName);
            }

            
            var response = await _httpClient.PostAsync("/api/student/Create", formContent);

          
            if (response.IsSuccessStatusCode)
            {
                
                return new GenericResponseModel<bool>
                {
                    StatusCode = 201,
                    Data = true
                };
            }
            else
            {
                
                var errorMessage = await response.Content.ReadAsStringAsync();

                return new GenericResponseModel<bool>
                {
                    StatusCode = (int)response.StatusCode,
                    Data = false
                };
            }
        }







        public async Task<GenericResponseModel<PaginationResponse<GetStudentDto>>> GetAllStudentsApiAsync(PaginationRequest paginationRequest)
        {
            
            var paginationResult = await _studentRepository.GetAllStudentAsync(paginationRequest);

            
            var studentDtos = _mapper.Map<List<GetStudentDto>>(paginationResult.Data);

            
            foreach (var studentDto in studentDtos)
            {
                if (studentDto.FileDetails != null)
                {
                    foreach (var fileDetail in studentDto.FileDetails)
                    {
                        
                        var fileData = await _fileService.Download(studentDto.Id);
                        if (fileData != null)
                        {
                            fileDetail.Data = fileData.Data;
                            fileDetail.Extension = fileData.Extension;
                        }
                    }
                }
            }

            
            return new GenericResponseModel<PaginationResponse<GetStudentDto>>
            {
                Data = new PaginationResponse<GetStudentDto>(paginationResult.TotalCount, studentDtos),
                StatusCode = 200
            };
        }

        public async Task<GenericResponseModel<PaginationResponse<GetStudentDto>>> GetAll(PaginationRequest paginationRequest)
        {
            
            var apiUrl = $"/api/student/All?pageNumber={paginationRequest.PageNumber}&pageSize={paginationRequest.PageSize}";

            
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                
                var responseStr = await response.Content.ReadAsStringAsync();

                
                var paginationResponse = JsonConvert.DeserializeObject<GenericResponseModel<PaginationResponse<GetStudentDto>>>(responseStr);

                return new GenericResponseModel<PaginationResponse<GetStudentDto>>
                {
                    Data = paginationResponse.Data,
                    StatusCode = 200
                };
            }
            else
            {
                
                return new GenericResponseModel<PaginationResponse<GetStudentDto>>
                {
                    Data = null,
                    StatusCode = (int)response.StatusCode
                };
            }

        }


        public async Task<bool> UpdateStudentAsync(int studentId, UpdateStudentDto updateStudentDto)
            {
                var student = await _studentRepository.GetStudentByIdAsync(studentId);
                if (student == null)
                {
                    return false;
                }

                student.Name = updateStudentDto.Name;

                student.GroupStudents.Clear();

                foreach (var groupName in updateStudentDto.GroupNames)
                {
                    var group = await _groupRepository.GetByNameAsync(groupName);
                    if (group != null)
                    {
                        student.GroupStudents.Add(new GroupStudent
                        {
                            StudentId = student.Id,
                            GroupId = group.Id
                        });
                    }
                }

                await _unitOfWork.CommitAsync();

                return true;
            }
        public async Task<Student> GetByIdAsync(int id)
        {
            
            var student = await _studentRepository.GetByIdAsync(id);

            
            if (student == null)
            {
                return null;
            }

            
            return student;
        }


        public async Task<GenericResponseModel<bool>> DeleteStudentAsync(int id)
            {

                var student = await _studentRepository.GetStudentByIdAsync(id);

                if (student == null)
                {
                    return new GenericResponseModel<bool>
                    {
                        StatusCode = 404,
                        Data = false,

                    };
                }

                student.IsDeleted = true;

                await _studentRepository.Update(student);
                await _unitOfWork.CommitAsync();

                return new GenericResponseModel<bool>
                {
                    Data = true,
                    StatusCode = 200,

                };
            }

        //public async Task<List<GetStudentDto>> GetAllStudentsAsync()
        //{

        //    var students = await _studentRepository.GetAllStudentAsync();

        //    var studentDtos = _mapper.Map<IEnumerable<GetStudentDto>>(students);


        //    foreach (var studentDto in studentDtos)
        //    {

        //        var fileResponse = await _fileService.Download(studentDto.Id);


        //        if (fileResponse != null)
        //        {
        //            studentDto.ProfilePictureUrl = $"https://localhost:7273/api/student/downloadProfilePicture/{studentDto.Id}";
        //        }  
        //        else
        //        {
        //            studentDto.ProfilePictureUrl = null; 
        //        }
        //    }


        //    return studentDtos.ToList();
        //}
        //public async Task<GenericResponseModel<PostStudentDto>> AddAsync(PostStudentDto postStudentDto)
        //{
        //    string requsetObj = JsonConvert.SerializeObject(postStudentDto);
        //    HttpContent httpContent = new StringContent(requsetObj, System.Text.Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("/api/student/Create", httpContent);

        //    if (response.IsSuccessStatusCode)
        //    {


        //    }



        ////}




        //public async Task<List<GetStudentDto>> GetAll(GetStudentDto getStudentDto)
        //{
        //    var response = await _httpClient.GetAsync("/api/student/All");

        //    if (response.IsSuccessStatusCode)
        //    {

        //        string responseStr = await response.Content.ReadAsStringAsync();


        //        var result = JsonConvert.DeserializeObject<List<GetStudentDto>>(responseStr);


        //        return result;
        //    }


        //    return new List<GetStudentDto>();
        //}

        //public async Task<List<GetStudentDto>> GetAllStudentsAsync()
        //{

        //    var students = await _studentRepository.GetAllStudentAsync();

        //    var studentDtos = _mapper.Map<IEnumerable<GetStudentDto>>(students);


        //    foreach (var studentDto in studentDtos)
        //    {

        //        var fileResponse = await _fileService.Download(studentDto.Id);


        //        if (fileResponse != null)
        //        {
        //            studentDto.ProfilePictureUrl = $"https://localhost:7273/api/student/downloadProfilePicture/{studentDto.Id}";
        //        }
        //        else
        //        {
        //            studentDto.ProfilePictureUrl = null; 
        //        }
        //    }


        //    return studentDtos.ToList();
        //}


    }
    
}




