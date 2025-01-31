using AutoMapper;
using CourseSystem.Dtos.File;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.DataAccess.Entities;
using System.Linq;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // Mapping Student to GetStudentDto
        CreateMap<Student, GetStudentDto>()
         .ForMember(dest => dest.GroupNames, opt => opt.MapFrom(src => src.GroupStudents.Select(gs => gs.Group.Name).ToList()))
         .ForMember(dest => dest.LessonNames, opt => opt.MapFrom(src => src.Lessons.Select(sl => sl.Lesson.Name).ToList()))
         .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.CreateDate)) // Assuming CreateDate is the registration date
         .ForMember(dest => dest.FileDetails, opt => opt.MapFrom(src => src.FileDetail.Select(fd => new FileDetailDto
         {
             Extension = fd.Extension
             // Data is omitted, as FileService will handle the 'Data' mapping in a separate service
         }).ToList()))
         .ReverseMap(); // Reverse mapping for updating Student from GetStudentDto

        // Mapping from FileDetail to FileDetailDto (without Data)
        CreateMap<FileDetail, FileDetailDto>()
            .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension));
    
    // Mapping from PostStudentDto to Student
    CreateMap<PostStudentDto, Student>()
            .ForMember(dest => dest.GroupStudents, opt => opt.Ignore()) // GroupStudents will be handled manually
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now)) // Automatically set the current DateTime as CreateDate
            .ForMember(dest => dest.FileDetail, opt => opt.Ignore()) // FileDetail will be handled manually
            .ReverseMap();

        // Mapping from UpdateStudentDto to Student
        CreateMap<UpdateStudentDto, Student>()
            .ForMember(dest => dest.GroupStudents, opt => opt.Ignore()) // GroupStudents will be handled manually
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)); // Map Name during update
    }
}
