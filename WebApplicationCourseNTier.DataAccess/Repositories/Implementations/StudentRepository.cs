using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;
using WebApplicationCourseNTier.DataAccess.Models;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly CourseSystemArcDBContext _context;

        public StudentRepository(CourseSystemArcDBContext context) : base(context)
        {
            _context = context;
        }
        //public async Task<List<Student>> GetAllStudentAsync()
        //{
        //    // Fetch all students, including related data (Groups, Lessons, and FileDetail)
        //    return await _context.Students
        //        .Where(s => !s.IsDeleted) // Exclude deleted students
        //        .Include(s => s.GroupStudents) // Include related GroupStudents
        //        .ThenInclude(gs => gs.Group) // Include related Group for each GroupStudent
        //        .Include(s => s.Lessons) // Include related Lessons
        //        .ThenInclude(sl => sl.Lesson) // Include related Lesson for each Lesson
        //        .Include(s => s.FileDetail) // Include related FileDetail
        //        .ToListAsync(); // Execute the query and fetch the results as a list
        //}

        public async Task<PaginationResponse<Student>> GetAllStudentAsync(PaginationRequest paginationRequest)
        {
            IQueryable<Student> query = _context.Students
                .Where(s => !s.IsDeleted) // Silinmiş tələbələri çıxarırıq
                .Include(s => s.GroupStudents)
                    .ThenInclude(gs => gs.Group)
                .Include(s => s.Lessons)
                    .ThenInclude(sl => sl.Lesson)
                .Include(s => s.FileDetail); // Fayl məlumatları ilə birlikdə gətiririk

            // Ümumi tələbə sayını alırıq
            int totalCount = await query.CountAsync();

            // İstədiyimiz səhifə məlumatlarını alırıq
            var students = await query
                .OrderBy(s => s.Id) // Tələbələri sıralayırıq (id ilə)
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize) // Səhifələmə üçün uyğun tələbələri atırıq
                .Take(paginationRequest.PageSize) // Hər səhifədə göstəriləcək tələbə sayını təyin edirik
                .ToListAsync();

            return new PaginationResponse<Student>(totalCount, students); // Pagination cavabını qaytarırıq
        }


        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Where(s => s.Id == id && !s.IsDeleted)
                .Include(s => s.GroupStudents)
                .ThenInclude(gs => gs.Group)
                .Include(s => s.Lessons)
                .ThenInclude(sl => sl.Lesson)
                .Include(s => s.FileDetail)
                .FirstOrDefaultAsync();
        }
        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
          
            return student;
        }
        //public async Task<List<Student>> GetAllStudentAsync()
        //{
        //    // Fetch all students, including related data (Groups, Lessons, and FileDetail)
        //    return await _context.Students
        //        .Where(s => !s.IsDeleted) // Exclude deleted students
        //        .Include(s => s.GroupStudents) // Include related GroupStudents
        //        .ThenInclude(gs => gs.Group) // Include related Group for each GroupStudent
        //        .Include(s => s.Lessons) // Include related Lessons
        //        .ThenInclude(sl => sl.Lesson) // Include related Lesson for each Lesson
        //        .Include(s => s.FileDetail) // Include related FileDetail
        //        .ToListAsync(); // Execute the query and fetch the results as a list
        //}



        //}
    }
}

/*.ToListAsync();*///bunu etmirik cunki elesek rama gelib dusecek
