namespace WebApplicationCourseNTier.Business.DTOs.StudentDTOs
{
    public class UpdateStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> GroupNames { get; set; } 
    }
}
