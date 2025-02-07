using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class StudentGroupRepoistory : Repository<GroupStudent>, IStudentGroupRepoistory
    {
        private readonly CourseSystemArcDBContext _context;

        public StudentGroupRepoistory(CourseSystemArcDBContext courseNtierDbContext) : base(courseNtierDbContext)
        {
            _context = courseNtierDbContext;
        }

        public async Task<bool> RemoveAsync(GroupStudent groupStudent)
        {
            if (groupStudent == null)
                return false;

            _context.GroupStudents.Remove(groupStudent); // Qrup əlaqəsini sil

            var result = await _context.SaveChangesAsync(); // Dəyişiklikləri yadda saxla
            return result > 0; // Əgər silinmə uğurlu oldusa, true qaytar
        }

        public async Task<IEnumerable<GroupStudent>> GetAlllAsync()
        {
            return await _context.GroupStudents
                .Where(gs => !gs.Group.IsDeleted)  // Ensure the group is not deleted
                .ToListAsync();
        }
    }
}