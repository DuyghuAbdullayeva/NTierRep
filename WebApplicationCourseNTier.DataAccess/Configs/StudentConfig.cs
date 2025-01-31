using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class StudentConfig : IBaseEntityConfig<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
          
            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
