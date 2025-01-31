using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.Business.Services.Implementations
{
    public class StudentLessonService : IStudentLessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStudentLessonRepository _studentLessonRepository;

        public StudentLessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _studentLessonRepository = _unitOfWork.GetRepository<IStudentLessonRepository>();
        }

        private IStudentLessonRepository GetStudentLessonRepository()
        {
            return _unitOfWork.GetRepository<IStudentLessonRepository>();
        }

        public async Task<GenericResponseModel<GetStudentLessonDto>> GetStudentLessonByIdAsync(int id)
        {
            var studentLessonRepository = GetStudentLessonRepository();
            var studentLesson = await studentLessonRepository.GetStudentLessonWithDetailsAsync(id);

            if (studentLesson == null)
            {
                return CreateErrorResponse<GetStudentLessonDto>("Student lesson not found.", 404);
            }

            var studentLessonDto = _mapper.Map<GetStudentLessonDto>(studentLesson);
            return new GenericResponseModel<GetStudentLessonDto>
            {
                StatusCode = 200,
                Data = studentLessonDto,

            };
        }

        public async Task<GenericResponseModel<PostStudentLessonDto>> CreateStudentLessonAsync(PostStudentLessonDto studentLessonDto)
        {
            var studentLessonRepository = GetStudentLessonRepository();
            var studentLesson = _mapper.Map<StudentLesson>(studentLessonDto);

            var result = await studentLessonRepository.AddAsync(studentLesson);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<PostStudentLessonDto>
            {
                StatusCode = result ? 201 : 400,
                Data = studentLessonDto,
                
            };
        }

        public async Task<GenericResponseModel<bool>> UpdateStudentLessonAsync(int id, UpdateStudentLessonDto studentLessonDto)
        {
            var studentLessonRepository = GetStudentLessonRepository();
            var studentLesson = await studentLessonRepository.GetByIdAsync(id);

            if (studentLesson == null)
            {
                return CreateErrorResponse<bool>("Student lesson not found.", 404);
            }

            studentLesson.StudentMark = studentLessonDto.StudentMark;
            studentLesson.AbsentMark = studentLessonDto.AbsentMark;

            var result = await studentLessonRepository.Update(studentLesson);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = result ? 200 : 400,
                Data = result,
                
            };
        }

        public async Task<GenericResponseModel<bool>> DeleteStudentLessonAsync(int id)
        {
            var studentLessonRepository = GetStudentLessonRepository();
            var studentLesson = await studentLessonRepository.GetByIdAsync(id);

            if (studentLesson == null)
            {
                return CreateErrorResponse<bool>("Student lesson not found.", 404);
            }

            var result = await studentLessonRepository.RemoveAsync(studentLesson);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = result ? 200 : 400,
                Data = result,
               
            };
        }

        public async Task<GenericResponseModel<IEnumerable<GetStudentLessonDto>>> GetAllStudentLessonsAsync()
        {
            var studentLessonRepository = GetStudentLessonRepository();
            var studentLessons = await studentLessonRepository.GetAllStudentLessonsWithDetailsAsync();
            var studentLessonDtos = _mapper.Map<IEnumerable<GetStudentLessonDto>>(studentLessons);

            return new GenericResponseModel<IEnumerable<GetStudentLessonDto>>
            {
                StatusCode = 200,
                Data = studentLessonDtos,
                
            };
        }

        // Helper method for error response generation
        private GenericResponseModel<T> CreateErrorResponse<T>(string message, int statusCode)
        {
            return new GenericResponseModel<T>
            {
                StatusCode = statusCode,
                Data = default,
               
            };
        }
    }
}
