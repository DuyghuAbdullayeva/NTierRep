using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.Business.DTOs.Topic
{
    public class UpdateTopicDto
    {
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public List<int> LessonIds { get; set; }
    }
}
