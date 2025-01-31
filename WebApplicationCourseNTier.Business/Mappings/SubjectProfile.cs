using AutoMapper;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
using WebApplicationCourseNTier.DataAccess.Entities;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, GetSubjectDto>()
                .ReverseMap(); 

        CreateMap<PostSubjectDto, Subject>().ReverseMap();

        CreateMap<UpdateSubjectDto, Subject>().ReverseMap();

     
   
    }
}
