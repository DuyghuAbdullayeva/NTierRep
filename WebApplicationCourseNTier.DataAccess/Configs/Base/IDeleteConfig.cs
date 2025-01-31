using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs.Base
{

    public class IDeleteConfig<T> : IUpdateConfig<T> where T : class, IDeleteEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder); 
            builder.HasQueryFilter(x=>x.IsDeleted==false);
            builder.Property(e => e.IsDeleted).IsRequired(); 
            builder.Property(e => e.DeleteDate).IsRequired(false);
        }
    }
}
