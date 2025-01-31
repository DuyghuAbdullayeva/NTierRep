using WebApplicationCourseNTier.DataAccess.Entities.Base;
using WebApplicationCourseNTier.DataAccess.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public ICollection<GroupStudent> GroupStudents { get; set; } 
    public ICollection<Lesson> Lessons { get; set; }
}
