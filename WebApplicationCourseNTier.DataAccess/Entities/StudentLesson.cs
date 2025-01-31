using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class StudentLesson:BaseEntity
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public Lesson Lesson { get; set; }
        public Student Student { get; set; }
        public decimal StudentMark { get; set; }  
        public bool AbsentMark { get; set; }


    }
}
