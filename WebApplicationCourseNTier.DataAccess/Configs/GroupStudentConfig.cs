using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class GroupStudentConfig : IBaseEntityConfig<GroupStudent>
    {
        public override void Configure(EntityTypeBuilder<GroupStudent> builder)
        {
            builder.HasKey(gs => new { gs.GroupId, gs.StudentId });

            builder.HasOne(gs => gs.Group)
                .WithMany(g => g.GroupStudents)
                .HasForeignKey(gs => gs.GroupId);


            builder.HasOne(gs => gs.Student)
                .WithMany(s => s.GroupStudents)
                .HasForeignKey(gs => gs.StudentId);
             
            base.Configure(builder);  

           
        }
    }
}
