using AutoMapper;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepo;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teacherRepo = _unitOfWork.GetRepository<ITeacherRepository>();
        }

        public async Task<GenericResponseModel<PostTeacherDto>> CreateTeacherAsync(PostTeacherDto teacherDto)
        {
            
            if (teacherDto == null)
            {
                return new GenericResponseModel<PostTeacherDto>
                {
                    StatusCode = 400, // Bad Request
                    Data = null
                };
            }

            // 2. Yeni müəllimi əlavə etmək
            var teacher = _mapper.Map<Teacher>(teacherDto); // DTO-nu Teacher entity-sinə çeviririk
            await _teacherRepo.AddAsync(teacher); // Repository-yə əlavə edirik
            await _unitOfWork.CommitAsync(); // Dəyişiklikləri saxlamaq

            // 3. Yaradılmış müəllimi DTO-ya çeviririk
            var createdTeacherDto = _mapper.Map<PostTeacherDto>(teacher);

            return new GenericResponseModel<PostTeacherDto>
            {
                StatusCode = 201, // Created
                Data = createdTeacherDto
            };
        }


        // Method to delete a teacher by ID
        public async Task<GenericResponseModel<bool>> DeleteTeacherAsync(int id)
        {
            var teacher = await _teacherRepo.GetByIdAsync(id);

            if (teacher == null)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404, // Not Found
                    Data = false
                };
            }

            await _teacherRepo.RemoveAsync(teacher);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = 200, // OK
                Data = true
            };
        }

   
        public async Task<GenericResponseModel<IEnumerable<GetTeacherDto>>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepo.GetAllAsync(null); // Fetch all teachers

            if (teachers == null || !teachers.Any())
            {
                return new GenericResponseModel<IEnumerable<GetTeacherDto>>
                {
                    StatusCode = 404, // Not Found
                    Data = Enumerable.Empty<GetTeacherDto>()
                };
            }

            var teacherDtos = _mapper.Map<IEnumerable<GetTeacherDto>>(teachers);
            return new GenericResponseModel<IEnumerable<GetTeacherDto>>
            {
                StatusCode = 200, 
                Data = teacherDtos
            };
        }

        // Method to get a specific teacher by ID
        public async Task<GenericResponseModel<GetTeacherDto>> GetTeacherByIdAsync(int id)
        {
            // Fetch the teacher by ID, ensuring it's not marked as deleted
            var teacher = await _teacherRepo.GetAllAsync(t => t.Id == id && !t.IsDeleted);

            // If no teacher is found or if the teacher is deleted, return a 404 response
            if (teacher == null || !teacher.Any())
            {
                return new GenericResponseModel<GetTeacherDto>
                {
                    StatusCode = 404, // Not Found
                    Data = null
                };
            }

            // Since GetAllAsync returns a collection, we take the first (and only) element
            var teacherDto = _mapper.Map<GetTeacherDto>(teacher.FirstOrDefault());

            return new GenericResponseModel<GetTeacherDto>
            {
                StatusCode = 200, // OK
                Data = teacherDto
            };
        }


        public async Task<GenericResponseModel<bool>> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        {
            // Fetch the teacher by ID, ensuring it is not marked as deleted
            var teacher = await _teacherRepo.GetByIdAsync(id);

            // Check if the teacher exists and is not deleted
            if (teacher == null || teacher.IsDeleted)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404, // Not Found or Deleted
                    Data = false
                };
            }

            // Map the changes from the DTO to the teacher entity
            _mapper.Map(teacherDto, teacher);

            // Update the teacher in the repository
            await _teacherRepo.Update(teacher);

            // Commit the changes to the database
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = 200, // OK
                Data = true
            };
        }


    }
}









//using AutoMapper;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
//using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
//using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
//using WebApplicationCourseNTier.DataAccess.Entities;
//using WebApplicationCourseNTier.Business.Services.Abstractions;
//using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

//namespace WebApplicationCourseNTier.Business.Services.Implementations
//{
//    public class TeacherService : ITeacherService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;


//        private IRepository<Teacher> TeacherRepository => _unitOfWork.GetRepository<ITeacherRepository>();

//        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public async Task<GenericResponseModel<GetTeacherDto>> GetTeacherByIdAsync(int id)
//        {
//            var teacher = await TeacherRepository.GetByIdAsync(id);
//            if (teacher == null)
//            {
//                return new GenericResponseModel<GetTeacherDto> { StatusCode = 404, Data = null };
//            }
//            var teacherDto = _mapper.Map<GetTeacherDto>(teacher);
//            return new GenericResponseModel<GetTeacherDto> { StatusCode = 200, Data = teacherDto };
//        }

//        public async Task<GenericResponseModel<IEnumerable<GetTeacherDto>>> GetAllTeachersAsync()
//        {
//            var teachers = await TeacherRepository.GetAllAsync(t => true); 
//            var teacherDtos = _mapper.Map<IEnumerable<GetTeacherDto>>(teachers);
//            return new GenericResponseModel<IEnumerable<GetTeacherDto>> { StatusCode = 200, Data = teacherDtos };
//        }

//        public async Task<GenericResponseModel<GetTeacherDto>> CreateTeacherAsync(PostTeacherDto postTeacherDto)
//        {
//            var teacher = _mapper.Map<Teacher>(postTeacherDto);
//            await TeacherRepository.AddAsync(teacher);
//            await _unitOfWork.CommitAsync();

//            var teacherDto = _mapper.Map<GetTeacherDto>(teacher);
//            return new GenericResponseModel<GetTeacherDto> { StatusCode = 201, Data = teacherDto };
//        }

//        public async Task<GenericResponseModel<GetTeacherDto>> UpdateTeacherAsync(UpdateTeacherDto updateTeacherDto)
//        {
//            var teacher = await TeacherRepository.GetByIdAsync(updateTeacherDto.Id);
//            if (teacher == null)
//            {
//                return new GenericResponseModel<GetTeacherDto> { StatusCode = 404, Data = null };
//            }

//            _mapper.Map(updateTeacherDto, teacher);
//            teacher.UpdateDate = DateTime.UtcNow;
//            teacher.UpdateUserId = 0;

//            await TeacherRepository.UpdateAsync(teacher);
//            await _unitOfWork.CommitAsync();

//            var teacherDto = _mapper.Map<GetTeacherDto>(teacher);
//            return new GenericResponseModel<GetTeacherDto> { StatusCode = 200, Data = teacherDto };
//        }

//        public async Task<GenericResponseModel<bool>> DeleteTeacherAsync(int id)
//        {
//            var teacher = await TeacherRepository.GetByIdAsync(id);
//            if (teacher == null)
//            {
//                return new GenericResponseModel<bool> { StatusCode = 404, Data = false };
//            }

//            teacher.IsDeleted = true;
//            teacher.DeleteDate = DateTime.UtcNow;
//            teacher.DeleteUserId = 0;
//            await TeacherRepository.UpdateAsync(teacher); 
//            await _unitOfWork.CommitAsync();

//            return new GenericResponseModel<bool> { StatusCode = 200, Data = true };
//        }
//    }
//}
