using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class GroupConfig : IBaseEntityConfig<Group>
    {
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
          
            builder.Property(g => g.Name).IsRequired().HasMaxLength(50);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(g => g.Teacher)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(g => g.Subject)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Lessons)
                .WithOne(l => l.Group)
                .HasForeignKey(l => l.GroupId);
            base.Configure(builder); 

            
        }
    }
}
