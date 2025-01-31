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
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public SubjectRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects
                                 .Where(s => !s.IsDeleted)  // Exclude deleted subjects
                                 .ToListAsync();
        }

        // Custom method to find a subject by its name, filtering out deleted ones
        public async Task<Subject> GetSubjectByNameAsync(string name)
        {
            return await _context.Subjects
                                 .FirstOrDefaultAsync(s => s.Name == name && !s.IsDeleted);  // Ensure subject is not deleted
        }

        //public async Task<Subject> GetSubjectByIdWithLessonsAsync(int id)
        //{
        //    return await _context.Set<Subject>()
        //                         .Where(s => s.Id == id)
        //                         .Include(s => s.Lessons) 
        //                         .FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<Subject>> GetAllSubjectsWithLessonsAsync()
        //{
        //    return await _context.Set<Subject>()
        //                         .Include(s => s.Lessons)  
        //                         .ToListAsync();
        //}
    }
}
