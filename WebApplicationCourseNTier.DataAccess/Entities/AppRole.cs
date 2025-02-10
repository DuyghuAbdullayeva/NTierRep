using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class AppRole : IEntity
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
