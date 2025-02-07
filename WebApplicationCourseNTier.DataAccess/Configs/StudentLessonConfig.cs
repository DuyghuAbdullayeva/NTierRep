using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class StudentLessonConfig : IBaseEntityConfig<StudentLesson>
    {
        public override void Configure(EntityTypeBuilder<StudentLesson> builder)
        {
            builder.HasKey(lt => new { lt.LessonId, lt.StudentId });
            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.HasOne(sl => sl.Student)
                .WithMany(s => s.Lessons)
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sl => sl.Lesson)
                .WithMany(l => l.StudentLessons)
                .HasForeignKey(sl => sl.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
            base.Configure(builder);
        }
    }
}
