using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class TeacherConfig : IBaseEntityConfig<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {

 
            builder.Property(t => t.Name).IsRequired().HasMaxLength(128);

        
            base.Configure(builder);
        }
    }
}
