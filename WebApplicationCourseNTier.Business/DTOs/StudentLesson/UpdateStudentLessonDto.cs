using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.StudentLesson
{
    public class UpdateStudentLessonDto
    {
        public decimal StudentMark { get; set; }
        public bool AbsentMark { get; set; }
    }
}
