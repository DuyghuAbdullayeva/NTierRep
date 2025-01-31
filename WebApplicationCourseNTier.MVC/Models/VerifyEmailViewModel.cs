using System.ComponentModel.DataAnnotations;

namespace WebApplicationCourseNTier.MVC.Models
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
