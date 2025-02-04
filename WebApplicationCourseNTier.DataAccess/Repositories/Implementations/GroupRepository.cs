using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public GroupRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginationResponse<Group>> GetAllGroupAsync(PaginationRequest paginationRequest)
        {
            IQueryable<Group> query = _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Subject)
                .Include(g => g.GroupStudents)
                    .ThenInclude(gs => gs.Student)
                .Include(g => g.Lessons);
                   
                

            int totalCount = await query.CountAsync();

            var groups = await query
                .OrderBy(g => g.Name)  
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)  
                .Take(paginationRequest.PageSize)  
                .ToListAsync();

            return new PaginationResponse<Group>(totalCount, groups);
        }



        public async Task<Group> GetGroupWithAllDetailsAsync(int groupId)
        {
            return await _context.Groups
                .Include(g => g.Teacher)
                .Include(g => g.Subject)
                .Include(g => g.GroupStudents)
                    .ThenInclude(gs => gs.Student)
                .Include(g => g.Lessons)
                .Where(g => g.Id == groupId && !g.IsDeleted)
                .Select(g => new Group
                {
                    Id = g.Id,
                    Name = g.Name,
                    Teacher = g.Teacher,
                    TeacherId = g.TeacherId,
                    Subject = g.Subject,
                    SubjectId = g.SubjectId,
                    GroupStudents = g.GroupStudents.Where(gs => !gs.Student.IsDeleted).ToList(),
                    Lessons = g.Lessons
                })
                .FirstOrDefaultAsync();
        }


        // Get a group by its name (and ensure it is not deleted).
        public async Task<Group> GetByNameAsync(string name)
        {
            return await _context.Groups
                .Include(g => g.Teacher) // Include related Teacher entity
                .Include(g => g.Subject) // Include related Subject entity
                .Include(g => g.GroupStudents) // Include related GroupStudents
                    .ThenInclude(gs => gs.Student) // Include related Student entity
                .Include(g => g.Lessons) // Include related Lessons entity
                .Where(g => g.Name == name && !g.IsDeleted) // Ensure group is not deleted
                .FirstOrDefaultAsync(); // Return the first match (or null if no match found)
        }

        public async Task<List<Group>> GetAllGroupsWithoutPaginationAsync()
        {
            return await _context.Groups
                .Where(g => !g.IsDeleted)  // Optionally filter out deleted groups
                .Select(g => new Group
                {
                    Name = g.Name
                })
                .ToListAsync();
        }
    }
}
