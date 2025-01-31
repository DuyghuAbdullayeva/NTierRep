using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
