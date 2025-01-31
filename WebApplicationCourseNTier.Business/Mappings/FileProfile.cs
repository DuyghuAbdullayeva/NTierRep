using AutoMapper;
using CourseSystem.Dtos.File;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Mappings
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            // Mapping from FileDetail to FileDetailDto
            CreateMap<FileDetail, FileDetailDto>()
     .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension))
     .ForMember(dest => dest.Data, opt => opt.MapFrom(src => File.ReadAllBytes(src.FilePath)));
        }
    }
}
