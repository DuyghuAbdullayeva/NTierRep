namespace WebApplicationCourseNTier.MVC.Models
{
    public class PaginationViewModel<T>
    {
        public IEnumerable<T> Values { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; } 
        
    }
}
