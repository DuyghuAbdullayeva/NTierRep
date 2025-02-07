using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.Business.DTOs.Lesson;
using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;
using WebApplicationCourseNTier.Business.DTOs.SubjectDTOs;
using WebApplicationCourseNTier.Business.DTOs.TeacherDTOs;

namespace WebApplicationCourseNTier.Business.DTOs.GroupDTOs
{
    public class GetGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public ICollection<string> StudentNames { get; set; }
        public ICollection<string> LessonNames { get; set; }
    }
}
