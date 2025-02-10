using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Models;
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
        _lessonRepository = _unitOfWork.GetRepository<ILessonRepository>();
    }

    public async Task<GenericResponseModel<PaginationResponse<GetLessonDto>>> GetAllLessonsAsync(PaginationRequest paginationRequest)
    {
        var paginationResult = await _lessonRepository.GetAllLessonAsync(paginationRequest);
        var lessonDtos = _mapper.Map<IEnumerable<GetLessonDto>>(paginationResult.Data);
        return new GenericResponseModel<PaginationResponse<GetLessonDto>>
        {
            Data = new PaginationResponse<GetLessonDto>(paginationResult.TotalCount, lessonDtos),
            StatusCode = 200
        };
    }

    public async Task<GenericResponseModel<IEnumerable<GetLessonDto>>> GetLessonsByGroupIdAsync(int groupId)
    {
        var lessons = await _lessonRepository.GetLessonsByGroupIdAsync(groupId);
        var lessonDtos = _mapper.Map<IEnumerable<GetLessonDto>>(lessons);

        return new GenericResponseModel<IEnumerable<GetLessonDto>>
        {
            StatusCode = 200,
            Data = lessonDtos,
        };
    }

    public async Task<GenericResponseModel<GetLessonDto>> GetLessonByIdAsync(int id)
    {
        var lesson = await _lessonRepository.GetLessonByIdAsync(id);

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
            Data = lessonDto,
        };
    }

    public async Task<GenericResponseModel<bool>> CreateLessonAsync(PostLessonDto postLessonDto)
    {
        var group = await _lessonRepository.GetGroupByNameAsync(postLessonDto.GroupName);

        if (group == null)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404,
                Data = false,
            };
        }

        if (group.IsDeleted)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 400,
                Data = false,
            };
        }

        var lesson = _mapper.Map<Lesson>(postLessonDto);

        lesson.GroupId = group.Id;

        await _lessonRepository.AddAsync(lesson);
        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<bool>
        {
            StatusCode = 201,
            Data = true,
        };
    }

    public async Task<GenericResponseModel<List<GetStudentLessonDto>>> GetStudentLessonsByLessonIdAsync(int lessonId)
    {
        var studentLessons = await _lessonRepository.GetStudentLessonsByLessonIdAsync(lessonId);

        var result = studentLessons.Select(sl => new GetStudentLessonDto
        {
            LessonId = sl.LessonId,
            StudentId = sl.StudentId,
            StudentName = sl.Student.Name,
            LessonName = sl.Lesson.Name,
            StudentMark = sl.StudentMark,
            AbsentMark = sl.AbsentMark
        }).ToList();

        return new GenericResponseModel<List<GetStudentLessonDto>>
        {
            StatusCode = 200,
            Data = result
        };
    }

    public async Task<GenericResponseModel<bool>> UpdateLessonAsync(int id, UpdateLessonDto lessonDto)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);

        if (lesson == null)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404,
                Data = false,
            };
        }

        if (!string.IsNullOrEmpty(lessonDto.GroupName))
        {
            var group = await _lessonRepository.GetGroupByNameAsync(lessonDto.GroupName);
            if (group == null)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404,
                    Data = false,
                };
            }
            if (group.IsDeleted)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 400,
                    Data = false,
                };
            }
            lesson.GroupId = group.Id;
        }

        _mapper.Map(lessonDto, lesson);



        var result = await _lessonRepository.Update(lesson);
        if (result)
        {
            await _unitOfWork.CommitAsync();
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
        var lesson = await _lessonRepository.GetByIdAsync(id);

        if (lesson == null)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404,
                Data = false
            };
        }

        await _lessonRepository.RemoveAsync(lesson);
        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<bool>
        {
            StatusCode = 200,
            Data = true
        };
    }
}
