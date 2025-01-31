using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class Topic:BaseEntity
    {
        public string Name { get; set; } 
        public ICollection<LessonTopic> LessonTopics { get; set; }
    }
}
