using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public AppUserRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
