using WebApplicationCourseNTier.Business.DTOs.StudentDTOs;

public class StudentApiResponse
{
    public int TotalCount { get; set; }
    public List<GetStudentDto> Data { get; set; }
}
