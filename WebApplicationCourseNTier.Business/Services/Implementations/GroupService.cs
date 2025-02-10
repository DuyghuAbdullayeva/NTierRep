using AutoMapper;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.GroupDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Data;

using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class GroupService : IGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CourseSystemArcDBContext _context;
    private readonly IMapper _mapper;
    private readonly IGroupRepository _groupRepository;
    private readonly HttpClient _httpClient;

    public GroupService(IUnitOfWork unitOfWork, IMapper mapper, CourseSystemArcDBContext context
         , IHttpClientFactory httpClientFactory
         , IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
        _groupRepository = _unitOfWork.GetRepository<IGroupRepository>();
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(configuration["StudentApi"]);
    }



    public async Task<GenericResponseModel<GetGroupDto>> GetGroupByIdAsync(int id)
    {
        var group = await _groupRepository.GetGroupWithAllDetailsAsync(id);

        if (group == null)
        {
            return new GenericResponseModel<GetGroupDto>
            {
                Data = null,
                StatusCode = 404,

            };
        }

        var groupDto = _mapper.Map<GetGroupDto>(group);
        return new GenericResponseModel<GetGroupDto>
        {
            Data = groupDto,
            StatusCode = 200
        };
    }

    public async Task<GenericResponseModel<PostGroupDto>> CreateGroupAsync(PostGroupDto groupDto)
    {
        var existingGroup = await _groupRepository.GetByNameAsync(groupDto.Name);
        if (existingGroup != null)
        {
            return new GenericResponseModel<PostGroupDto>
            {
                Data = null,
                StatusCode = 400,

            };
        }

        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Name == groupDto.TeacherName);

        if (teacher == null)
        {
            return new GenericResponseModel<PostGroupDto>
            {
                Data = null,
                StatusCode = 404,

            };
        }

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Name == groupDto.SubjectName);

        if (subject == null)
        {
            return new GenericResponseModel<PostGroupDto>
            {
                Data = null,
                StatusCode = 404,

            };
        }


        var group = new Group
        {
            Name = groupDto.Name,
            TeacherId = teacher.Id,
            SubjectId = subject.Id
        };

        var isAdded = await _groupRepository.AddAsync(group);
        if (!isAdded)
        {
            return new GenericResponseModel<PostGroupDto>
            {
                Data = null,
                StatusCode = 400,

            };
        }

        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<PostGroupDto>
        {
            Data = groupDto,
            StatusCode = 201,

        };
    }


    public async Task<GenericResponseModel<bool>> UpdateGroupAsync(int id, UpdateGroupDto groupDto)
    {
        var group = await _groupRepository.GetByIdAsync(id);
        if (group == null)
        {
            return new GenericResponseModel<bool>
            {
                Data = false,
                StatusCode = 404,

            };
        }


        _mapper.Map(groupDto, group);

        var isUpdated = await _groupRepository.Update(group);
        if (!isUpdated)
        {
            return new GenericResponseModel<bool>
            {
                Data = false,
                StatusCode = 400,

            };
        }

        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<bool>
        {
            Data = true,
            StatusCode = 200,

        };
    }

    public async Task<GenericResponseModel<bool>> DeleteMvcAsync(int id)
    {
        HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"/api/group/{id}");
        GenericResponseModel<bool> model = new()
        {
            Data = false,
            StatusCode = 400
        };

        if (httpResponse.IsSuccessStatusCode)
        {
            string responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<bool>(responseStr);

            if (response is true)
            {
                model.Data = true;
                model.StatusCode = 200;
                return model;
            }
        }

        return model;
    }



    public async Task<GenericResponseModel<bool>> DeleteGroupAsync(int id)
    {
        var group = await _groupRepository.GetByIdAsync(id);
        if (group == null)
        {
            return new GenericResponseModel<bool>
            {
                Data = false,
                StatusCode = 404,

            };
        }


        group.IsDeleted = true;

        var isDeleted = await _groupRepository.Update(group);
        if (!isDeleted)
        {
            return new GenericResponseModel<bool>
            {
                Data = false,
                StatusCode = 400,

            };
        }

        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<bool>
        {
            Data = true,
            StatusCode = 200,

        };
    }
    public async Task<GenericResponseModel<PaginationResponse<GetGroupDto>>> GetAllGroupsAsync(PaginationRequest paginationRequest)
    {

        var paginationResult = await _groupRepository.GetAllGroupAsync(paginationRequest);
        var groupDtos = _mapper.Map<IEnumerable<GetGroupDto>>(paginationResult.Data);
        return new GenericResponseModel<PaginationResponse<GetGroupDto>>
        {
            Data = new PaginationResponse<GetGroupDto>(paginationResult.TotalCount, groupDtos),
            StatusCode = 200
        };
    }
    public async Task<IEnumerable<string>> GetAllGroupNamesAsync()
    {
        // Retrieve all groups without pagination
        var groups = await _groupRepository.GetAllGroupsWithoutPaginationAsync();

        // Return the group names
        return groups.Select(g => g.Name).ToList();
    }

}