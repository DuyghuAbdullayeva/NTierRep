using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public TeacherRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }

     
    }
}
