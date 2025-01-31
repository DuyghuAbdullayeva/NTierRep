
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Mappings
{
   
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, GetTeacherDto>().ReverseMap();
            CreateMap<Teacher, PostTeacherDto>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();
          
        }

    }
}
