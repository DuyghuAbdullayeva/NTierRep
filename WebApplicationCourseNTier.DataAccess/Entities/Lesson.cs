using WebApplicationCourseNTier.DataAccess.Entities.Base;
using WebApplicationCourseNTier.DataAccess.Entities;

public class Lesson : BaseEntity
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }  
    public DateTime EndDate { get; set; }
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public ICollection<StudentLesson> StudentLessons { get; set; }
    public ICollection<LessonTopic> LessonTopics { get; set; }
}
