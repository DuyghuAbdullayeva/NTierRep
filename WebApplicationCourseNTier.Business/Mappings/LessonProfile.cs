using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Mappings
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            // Mapping from Lesson entity to GetLessonDto
            CreateMap<Lesson, GetLessonDto>()
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))  // Map GroupName
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentLessons.Select(sl => sl.Student)))  // Map Students
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.LessonTopics.Select(lt => lt.Topic)));  // Map Topics

            // Mapping from PostLessonDto to Lesson entity
            CreateMap<PostLessonDto, Lesson>()
                .ForMember(dest => dest.StudentLessons, opt => opt.MapFrom(src => src.StudentIds.Select(id => new StudentLesson { StudentId = id })));  // Map StudentIds

            // Mapping from UpdateLessonDto to Lesson entity
            CreateMap<UpdateLessonDto, Lesson>()
                .ForMember(dest => dest.LessonTopics, opt => opt.MapFrom(src => src.TopicIds.Select(id => new LessonTopic { TopicId = id })))  // Map TopicIds
                .ForMember(dest => dest.StudentLessons, opt => opt.MapFrom(src => src.StudentIds.Select(id => new StudentLesson { StudentId = id })));  // Map StudentIds
        }
    }
 }

