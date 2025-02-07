using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.Lesson
{
    public class UpdateLessonDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string GroupName {  get; set; }
 
    }  
}
