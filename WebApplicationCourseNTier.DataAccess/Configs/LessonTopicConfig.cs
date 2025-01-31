using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Configs.Base;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class LessonTopicConfig : IBaseEntityConfig<LessonTopic>
    {
        public override void Configure(EntityTypeBuilder<LessonTopic> builder)
        {
            base.Configure(builder);

            builder.HasKey(lt => new { lt.LessonId, lt.TopicId });

            builder.HasOne(lt => lt.Lesson)
                   .WithMany(l => l.LessonTopics)
                   .HasForeignKey(lt => lt.LessonId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(lt => lt.Topic)
                   .WithMany(t => t.LessonTopics)
                   .HasForeignKey(lt => lt.TopicId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
