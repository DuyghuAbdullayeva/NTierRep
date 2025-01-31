using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class LessonConfig : IBaseEntityConfig<Lesson>
    {
        public override void Configure(EntityTypeBuilder<Lesson> builder)
        {

          
            builder.Property(l => l.Name).IsRequired().HasMaxLength(50);
            builder.Property(l => l.StartDate).IsRequired();
            builder.Property(l => l.EndDate).IsRequired();



        }
    }
}
