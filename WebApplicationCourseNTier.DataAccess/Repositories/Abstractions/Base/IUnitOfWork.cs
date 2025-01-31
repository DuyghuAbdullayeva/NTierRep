using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>()
            where TRepository : class, IRepositoryBase;

        Task CommitAsync();
    }
}



