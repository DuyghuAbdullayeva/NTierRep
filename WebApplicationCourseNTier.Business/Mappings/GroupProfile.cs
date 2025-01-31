using AutoMapper;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.Business.DTOs.GroupDTOs;
using System.Linq;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<Group, GetGroupDto>()
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
            .ForMember(dest => dest.StudentNames, opt => opt.MapFrom(src => src.GroupStudents.Select(gs => gs.Student.Name))) 
            .ForMember(dest => dest.LessonNames, opt => opt.MapFrom(src => src.Lessons.Select(l => l.Name))); 

      
        CreateMap<PostGroupDto, Group>()
            .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => new Teacher { Name = src.TeacherName })) 
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => new Subject { Name = src.SubjectName })); 

        CreateMap<Group, UpdateGroupDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
            .ReverseMap();
    }
}
