using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.DataAccess.Entities.Base
{
    public interface ICreateEntity : IEntity
    {
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
