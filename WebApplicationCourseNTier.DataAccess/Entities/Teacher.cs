using WebApplicationCourseNTier.DataAccess.Entities.Base;

public class Teacher : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Group> Groups { get; set; } 

}
