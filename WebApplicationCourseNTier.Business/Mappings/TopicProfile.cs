using AutoMapper;
using WebApplicationCourseNTier.Business.DTOs.Topic;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Mapping
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic, GetTopicDto>()
               .ForMember(dest => dest.LessonIds, opt => opt.MapFrom(src => src.LessonTopics.Select(lt => lt.LessonId).ToList())).ReverseMap();


            // DTO to Entity
            CreateMap<PostTopicDto, Topic>().ReverseMap();
            CreateMap<UpdateTopicDto, Topic>().ReverseMap();
        }
    }
}
