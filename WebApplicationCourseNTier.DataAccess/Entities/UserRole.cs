using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class UserRole
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }
    }
}
