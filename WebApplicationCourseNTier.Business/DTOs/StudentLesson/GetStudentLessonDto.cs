using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.StudentLesson
{
    public class GetStudentLessonDto
    {
        public int LessonId { get; set; }  // Dərsin ID-si
        public int StudentId { get; set; }  // Tələbənin ID-si
        public string StudentName { get; set; } // Tələbənin adı
        public string LessonName { get; set; }  // Dərsin adı
        public decimal StudentMark { get; set; }
        public bool AbsentMark { get; set; }
    }
}
