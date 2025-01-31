using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.Lesson
{
    public class PostLessonDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupId { get; set; }
        public ICollection<int> StudentIds { get; set; } 
   
    }
}
