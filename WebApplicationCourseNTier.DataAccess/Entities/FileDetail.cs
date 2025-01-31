using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class FileDetail : BaseEntity
    {
      
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string FilePath { get; set; }
    }
}
