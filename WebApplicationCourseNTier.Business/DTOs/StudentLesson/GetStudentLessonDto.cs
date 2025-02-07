using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.StudentLesson
{
    public class GetStudentLessonDto
    {
        public int LessonId { get; set; }  
        public int StudentId { get; set; }  
        public string StudentName { get; set; } 
        public string LessonName { get; set; }  
        public decimal? StudentMark { get; set; }
        public bool AbsentMark { get; set; }
    }
}
