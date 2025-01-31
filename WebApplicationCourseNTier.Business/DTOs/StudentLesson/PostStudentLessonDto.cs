using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.StudentLesson
{
    public class PostStudentLessonDto
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public decimal StudentMark { get; set; }
        public bool AbsentMark { get; set; }
    }
}
