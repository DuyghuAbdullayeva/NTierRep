using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<Subject> GetSubjectByNameAsync(string name);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        //Task<Subject> GetSubjectByIdWithLessonsAsync(int id); 
        //Task<IEnumerable<Subject>> GetAllSubjectsWithLessonsAsync();
    }

}
