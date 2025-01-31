
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Mappings
{
    public class StudentLessonProfile:Profile
    {
        public StudentLessonProfile()
        {
            CreateMap<StudentLesson, GetStudentLessonDto>()
           .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name)) // Assuming Student has a 'Name' property
           .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Lesson.Name)) // Assuming Lesson has a 'Name' property
           .ForMember(dest => dest.StudentMark, opt => opt.MapFrom(src => src.StudentMark))
           .ForMember(dest => dest.AbsentMark, opt => opt.MapFrom(src => src.AbsentMark));

            // Mapping from PostStudentLessonDto to StudentLesson (Create operation)
            CreateMap<PostStudentLessonDto, StudentLesson>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.LessonId, opt => opt.MapFrom(src => src.LessonId))
                .ForMember(dest => dest.StudentMark, opt => opt.MapFrom(src => src.StudentMark))
                .ForMember(dest => dest.AbsentMark, opt => opt.MapFrom(src => src.AbsentMark));

            // Mapping from UpdateStudentLessonDto to StudentLesson (Update operation)
            CreateMap<UpdateStudentLessonDto, StudentLesson>()
                .ForMember(dest => dest.StudentMark, opt => opt.MapFrom(src => src.StudentMark))
                .ForMember(dest => dest.AbsentMark, opt => opt.MapFrom(src => src.AbsentMark));
        }

    }
}
