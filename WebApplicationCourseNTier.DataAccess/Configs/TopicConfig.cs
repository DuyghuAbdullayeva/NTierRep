using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Configs.Base;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class TopicConfig : IBaseEntityConfig<Topic>
    {
        public override void Configure(EntityTypeBuilder<Topic> builder)
        {
            base.Configure(builder);

         
            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);

         
        }
    }
}
