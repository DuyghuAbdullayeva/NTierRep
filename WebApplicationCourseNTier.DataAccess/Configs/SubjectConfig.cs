using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class SubjectConfig : IBaseEntityConfig<Subject>
    {
        public override void Configure(EntityTypeBuilder<Subject> builder)
        {

            builder.Property(s => s.Name).IsRequired().HasMaxLength(128);

        
            base.Configure(builder);
        }
    }
}
