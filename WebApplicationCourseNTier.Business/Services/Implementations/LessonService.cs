using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

public class LessonService : ILessonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILessonRepository _lessonRepository;

    public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _lessonRepository =_unitOfWork.GetRepository<ILessonRepository>();
    }

    private ILessonRepository GetLessonRepository()
    {
        return _unitOfWork.GetRepository<ILessonRepository>();
    }

    public async Task<GenericResponseModel<GetLessonDto>> GetLessonByIdAsync(int id)
    {
        var lessonRepository = GetLessonRepository();
        var lesson = await lessonRepository.GetLessonWithDetailsAsync(id);

        if (lesson == null)
        {
            return new GenericResponseModel<GetLessonDto>
            {
                StatusCode = 404,
                Data = null,
               
            };
        }

        var lessonDto = _mapper.Map<GetLessonDto>(lesson);
        return new GenericResponseModel<GetLessonDto>
        {
            StatusCode = 200,
            Data = lessonDto
        };
    }

    public async Task<GenericResponseModel<PostLessonDto>> CreateLessonAsync(PostLessonDto lessonDto)
    {
        var lessonRepository = GetLessonRepository();
        var lesson = _mapper.Map<Lesson>(lessonDto);

        var result = await lessonRepository.AddAsync(lesson);
        if (result)
        {
            await _unitOfWork.CommitAsync();  // Commit transaction after changes

            return new GenericResponseModel<PostLessonDto>
            {
                StatusCode = 201,
                Data = lessonDto,
               
            };
        }

        return new GenericResponseModel<PostLessonDto>
        {
            StatusCode = 400,
            Data = null,
          
        };
    }

    public async Task<GenericResponseModel<bool>> UpdateLessonAsync(int id, UpdateLessonDto lessonDto)
    {
        var lessonRepository = GetLessonRepository();
        var lesson = await lessonRepository.GetByIdAsync(id);

        if (lesson == null)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404,
                Data = false,
               
            };
        }

        _mapper.Map(lessonDto, lesson);
        var result = await lessonRepository.Update(lesson);
        if (result)
        {
            await _unitOfWork.CommitAsync();  // Commit transaction after changes
            return new GenericResponseModel<bool>
            {
                StatusCode = 200,
                Data = true,
               
            };
        }

        return new GenericResponseModel<bool>
        {
            StatusCode = 400,
            Data = false,
           
        };
    }

    public async Task<GenericResponseModel<bool>> DeleteLessonAsync(int id)
    {
        var lessonRepository = GetLessonRepository();
        var lesson = await lessonRepository.GetByIdAsync(id);

        if (lesson == null)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404,
                Data = false,
                
            };
        }

        var result = await lessonRepository.RemoveAsync(lesson);
        if (result)
        {
            await _unitOfWork.CommitAsync();  // Commit transaction after changes
            return new GenericResponseModel<bool>
            {
                StatusCode = 200,
                Data = true,
                
            };
        }

        return new GenericResponseModel<bool>
        {
            StatusCode = 400,
            Data = false,
        
        };
    }

    public async Task<GenericResponseModel<IEnumerable<GetLessonDto>>> GetAllLessonsAsync()
    {
        var lessonRepository = GetLessonRepository();
        var lessons = await lessonRepository.GetAllAsync(null);
        var lessonDtos = _mapper.Map<IEnumerable<GetLessonDto>>(lessons);

        return new GenericResponseModel<IEnumerable<GetLessonDto>>
        {
            StatusCode = 200,
            Data = lessonDtos,
          
        };
    }

    public async Task<GenericResponseModel<IEnumerable<GetLessonDto>>> GetLessonsByGroupIdAsync(int groupId)
    {
        var lessonRepository = GetLessonRepository();
        var lessons = await lessonRepository.GetLessonsByGroupIdAsync(groupId);
        var lessonDtos = _mapper.Map<IEnumerable<GetLessonDto>>(lessons);

        return new GenericResponseModel<IEnumerable<GetLessonDto>>
        {
            StatusCode = 200,
            Data = lessonDtos,
          
        };
    }
}
