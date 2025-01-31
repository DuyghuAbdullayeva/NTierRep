using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class LessonTopic:BaseEntity
    {
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
