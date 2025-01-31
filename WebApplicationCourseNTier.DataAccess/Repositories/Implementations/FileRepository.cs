using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Data;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class FileRepository : Repository<FileDetail>, IFileRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public FileRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }


        public async Task AddFileDetails(FileDetail fileDetails)
        {
            await _context.FileDetail.AddAsync(fileDetails);
           

        }

        public async Task<Student> GetStudent(int studentId)
        {
            return await _context.Students
                .Where(x => x.Id == studentId)
                .Include(x => x.FileDetail)
                .FirstOrDefaultAsync();
        }
    }
}
