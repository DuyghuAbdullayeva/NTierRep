using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    private readonly CourseSystemArcDBContext _context;

    public LessonRepository(CourseSystemArcDBContext context) : base(context)
    {
        _context = context;
    }

    // Get a lesson with related students and topics
    public async Task<Lesson> GetLessonWithDetailsAsync(int id)
    {
        return await _context.Lessons
            .Include(l => l.StudentLessons)
            .ThenInclude(sl => sl.Student)
            .Include(l => l.LessonTopics)
            .ThenInclude(lt => lt.Topic)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    // Get all lessons for a specific group
    public async Task<IEnumerable<Lesson>> GetLessonsByGroupIdAsync(int groupId)
    {
        return await _context.Lessons
            .Where(l => l.GroupId == groupId)
            .Include(l => l.StudentLessons)
            .ThenInclude(sl => sl.Student)
            .Include(l => l.LessonTopics)
            .ThenInclude(lt => lt.Topic)
            .ToListAsync();
    }
}
