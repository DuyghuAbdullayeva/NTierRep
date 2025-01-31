using WebApplicationCourseNTier.DataAccess.Entities;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IFileRepository : IRepository<FileDetail>
    {
        Task<Student> GetStudent(int studentId);
        Task AddFileDetails(FileDetail fileDetails);
    }
}
