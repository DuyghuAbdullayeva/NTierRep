using AutoMapper;
using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISubjectRepository _subjectRepo;

    public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _subjectRepo = _unitOfWork.GetRepository<ISubjectRepository>();
    }

    public async Task<GenericResponseModel<PostSubjectDto>> CreateSubjectAsync(PostSubjectDto subjectDto)
    {
        // Step 1: Map the PostSubjectDto to the Subject entity
        var subject = _mapper.Map<Subject>(subjectDto);

        // Step 2: Add the Subject entity to the repository
        await _subjectRepo.AddAsync(subject);

        // Step 3: Commit the transaction to save the Subject to the database
        await _unitOfWork.CommitAsync();

        // Step 4: Map the Subject entity back to PostSubjectDto for the response
        var createdSubjectDto = _mapper.Map<PostSubjectDto>(subject);

        // Step 5: Return a successful response with the created Subject DTO
        return new GenericResponseModel<PostSubjectDto>
        {
            StatusCode = 201, // HTTP Status Code for Created
            Data = createdSubjectDto
        };
    }


    public async Task<GenericResponseModel<bool>> DeleteSubjectAsync(int id)
    {
        var subject = await _subjectRepo.GetByIdAsync(id);

        if (subject == null)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404,
                Data = false
            };
        }

        await _subjectRepo.RemoveAsync(subject);
        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<bool>
        {
            StatusCode = 200,
            Data = true
        };
    }

    public async Task<GenericResponseModel<IEnumerable<GetSubjectDto>>> GetAllSubjectsAsync()
    {
        // Subyektləri əldə et
        var subjects = await _subjectRepo.GetAllSubjectsAsync();

        // Əgər heç bir subyekt tapılmadısa, boş cavab qaytar
        if (subjects == null || !subjects.Any())
        {
            return new GenericResponseModel<IEnumerable<GetSubjectDto>>
            {
                StatusCode = 404, // Not Found
                Data = Enumerable.Empty<GetSubjectDto>()
            };
        }

        // Subyektləri DTO formatına çevir
        var subjectDtos = _mapper.Map<IEnumerable<GetSubjectDto>>(subjects);

        // Düzgün nəticəni qaytar
        return new GenericResponseModel<IEnumerable<GetSubjectDto>>
        {
            StatusCode = 200, // OK
            Data = subjectDtos
        };
    }
    public async Task<GenericResponseModel<GetSubjectDto>> GetSubjectByIdAsync(int id)
    {
        // Subyekt məlumatlarını id-ə əsaslanaraq əldə et
        var subject = await _subjectRepo.GetByIdAsync(id);

        // Subyekt tapılmadıqda, 404 - Not Found statusu qaytar
        if (subject == null || subject.IsDeleted)
        {
            return new GenericResponseModel<GetSubjectDto>
            {
                StatusCode = 404,  // Not Found
                Data = null
            };
        }

        // Tapılmış subyekti GetSubjectDto formatına çevir
        var subjectDto = _mapper.Map<GetSubjectDto>(subject);

        // Düzgün nəticəni 200 OK ilə qaytar
        return new GenericResponseModel<GetSubjectDto>
        {
            StatusCode = 200,  // OK
            Data = subjectDto
        };
    }





    public async Task<GenericResponseModel<bool>> UpdateSubjectAsync(int id, UpdateSubjectDto subjectDto)
    {
        // Fetch the teacher by ID, ensuring it is not marked as deleted
        var subject = await _subjectRepo.GetByIdAsync(id);

        // Check if the teacher exists and is not deleted
        if (subject == null || subject.IsDeleted)
        {
            return new GenericResponseModel<bool>
            {
                StatusCode = 404, // Not Found or Deleted
                Data = false
            };
        }

        // Map the changes from the DTO to the teacher entity
        _mapper.Map(subjectDto, subject);

        // Update the teacher in the repository
        await _subjectRepo.Update(subject);

        // Commit the changes to the database
        await _unitOfWork.CommitAsync();

        return new GenericResponseModel<bool>
        {
            StatusCode = 200, // OK
            Data = true
        };
    }
}











//using AutoMapper;
//using WebApplicationCourseNTier.Business.DTOs.BaseResponseModel;
//using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
//using WebApplicationCourseNTier.Business.Services.Abstractions;
//using WebApplicationCourseNTier.DataAccess.Entities;
//using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
//using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//public class SubjectService : ISubjectService
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IMapper _mapper;

//    public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
//    {
//        _unitOfWork = unitOfWork;
//        _mapper = mapper;
//    }


//    private IRepository<Subject> SubjectRepository => _unitOfWork.GetRepository<ISubjectRepository>();


//    public async Task<GenericResponseModel<GetSubjectDto>> GetSubjectByIdAsync(int id)
//    {
//        var subject = await SubjectRepository.GetByIdAsync(id);
//        if (subject == null)
//        {
//            return new GenericResponseModel<GetSubjectDto> { StatusCode = 404, Data = null };
//        }

//        var subjectDto = _mapper.Map<GetSubjectDto>(subject);
//        return new GenericResponseModel<GetSubjectDto> { StatusCode = 200, Data = subjectDto };
//    }

//    public async Task<GenericResponseModel<IEnumerable<GetSubjectDto>>> GetAllSubjectsAsync()
//    {
//        var subjects = await SubjectRepository.GetAllAsync(s => !s.IsDeleted); // Get only non-deleted subjects
//        var subjectDtos = _mapper.Map<IEnumerable<GetSubjectDto>>(subjects);
//        return new GenericResponseModel<IEnumerable<GetSubjectDto>> { StatusCode = 200, Data = subjectDtos };
//    }

//    public async Task<GenericResponseModel<GetSubjectDto>> CreateSubjectAsync(PostSubjectDto postSubjectDto)
//    {
//        var subject = _mapper.Map<Subject>(postSubjectDto);
//        await SubjectRepository.AddAsync(subject);
//        await _unitOfWork.CommitAsync();

//        var subjectDto = _mapper.Map<GetSubjectDto>(subject);
//        return new GenericResponseModel<GetSubjectDto> { StatusCode = 201, Data = subjectDto };
//    }


//    public async Task<GenericResponseModel<GetSubjectDto>> UpdateSubjectAsync(UpdateSubjectDto updateSubjectDto)
//    {
//        var subject = await SubjectRepository.GetByIdAsync(updateSubjectDto.Id);
//        if (subject == null)
//        {
//            return new GenericResponseModel<GetSubjectDto> { StatusCode = 404, Data = null };
//        }

//        _mapper.Map(updateSubjectDto, subject);
//        subject.UpdateUserId = 0;
//        subject.UpdateDate = DateTime.UtcNow;

//        await SubjectRepository.UpdateAsync(subject);
//        await _unitOfWork.CommitAsync();

//        var subjectDto = _mapper.Map<GetSubjectDto>(subject);
//        return new GenericResponseModel<GetSubjectDto> { StatusCode = 200, Data = subjectDto };
//    }


//    public async Task<GenericResponseModel<bool>> DeleteSubjectAsync(int id)
//    {
//        var subject = await SubjectRepository.GetByIdAsync(id);
//        if (subject == null)
//        {
//            return new GenericResponseModel<bool> { StatusCode = 404, Data = false };
//        }

//        subject.IsDeleted = true;
//        subject.DeleteUserId = 0;
//        subject.DeleteDate = DateTime.UtcNow;

//        await SubjectRepository.UpdateAsync(subject); 
//        await _unitOfWork.CommitAsync();

//        return new GenericResponseModel<bool> { StatusCode = 200, Data = true };
//    }
//}
