using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace WebApplicationCourseNTier.Business.DTOs.StudentDTOs
{
    public class PostStudentDto
    {
        
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<string> GroupNames { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
