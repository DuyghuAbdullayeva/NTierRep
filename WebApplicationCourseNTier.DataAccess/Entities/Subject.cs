using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;


namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Group> Groups { get; set; }
      
       
    }
}
