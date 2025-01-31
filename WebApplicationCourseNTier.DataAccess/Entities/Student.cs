using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupStudent> GroupStudents { get; set; }
        public ICollection<FileDetail> FileDetail { get; set; }
        public ICollection<StudentLesson> Lessons { get; set; }
        
       
    }
}
