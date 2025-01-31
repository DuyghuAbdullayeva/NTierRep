using CourseSystem.Dtos.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.GroupDTOs;
using WebApplicationCourseNTier.Business.DTOs.StudentLesson;

namespace WebApplicationCourseNTier.Business.DTOs.StudentDTOs
{
    public class GetStudentDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<string> GroupNames { get; set; }
        public List<string> LessonNames { get; set; }
        public List<FileDetailDto> FileDetails { get; set; }

    }

}
