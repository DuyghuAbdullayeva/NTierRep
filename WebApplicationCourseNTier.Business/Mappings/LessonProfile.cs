using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.DTOs.Student;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Mappings
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            // Mapping from Lesson entity to GetLessonDto
            CreateMap<Lesson, GetLessonDto>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))  // Map GroupName from Group entity
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentLessons.Select(sl => new StudentDTO
                {
                    Id = sl.Student.Id,
                    Name = sl.Student.Name
                }))); // Map Students from StudentLessons


            // Mapping from PostLessonDto to Lesson entity
            CreateMap<PostLessonDto, Lesson>()
                .ForMember(dest => dest.GroupId, opt => opt.Ignore())  // Ignore GroupId as it needs to be set manually
                .ForMember(dest => dest.Group, opt => opt.Ignore())   // Ignore the Group, as it will be manually set via GroupName
                .ForMember(dest => dest.StudentLessons, opt => opt.Ignore())  // Ignore StudentLessons, it will be set manually
                .ForMember(dest => dest.LessonTopics, opt => opt.Ignore());  // Ignore LessonTopics, it will be set manually

            // Mapping from UpdateLessonDto to Lesson entity
            CreateMap<UpdateLessonDto, Lesson>()
                .ForMember(dest => dest.GroupId, opt => opt.Ignore())  // Ignore GroupId as it will be manually set
                .ForMember(dest => dest.Group, opt => opt.Ignore())  // Ignore Group, as it will be manually set via GroupName

                .ForMember(dest => dest.StudentLessons, opt => opt.Ignore());  // Ignore StudentLessons for now, as this would need to be handled in the service layer.ap StudentIds
        }
    }
 }

