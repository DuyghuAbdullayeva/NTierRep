using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;


namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<PaginationResponse<Student>> GetAllStudentAsync(PaginationRequest paginationRequest);
        //Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetStudentByIdAsync(int id);


    }
}
