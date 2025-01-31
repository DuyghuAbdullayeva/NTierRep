using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface IStudentGroupRepoistory : IRepository<GroupStudent>
    {
        Task<bool> RemoveAsync(GroupStudent groupStudent);
        Task<IEnumerable<GroupStudent>> GetAlllAsync();
    }

}

