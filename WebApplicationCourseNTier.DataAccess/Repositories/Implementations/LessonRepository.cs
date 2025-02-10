using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;

public class LessonRepository : Repository<Lesson>, ILessonRepository
{
    private readonly CourseSystemArcDBContext _context;

    public LessonRepository(CourseSystemArcDBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<StudentLesson>> GetStudentLessonsByLessonIdAsync(int lessonId)
    {
        return await _context.StudentLessons
            .Where(sl => sl.LessonId == lessonId) 
            .Include(sl => sl.Student)
            .Include(sl => sl.Lesson)
            .ToListAsync();
    }

    public async Task<PaginationResponse<Lesson>> GetAllLessonAsync(PaginationRequest paginationRequest)
    {
        IQueryable<Lesson> query = _context.Lessons
          
            .Include(l => l.Group)
            .Include(l => l.StudentLessons)
                .ThenInclude(sl => sl.Student)
            .Include(l => l.LessonTopics)
                .ThenInclude(lt => lt.Topic);

        int totalCount = await query.CountAsync();

        var lessons = await query
       .OrderBy(l => l.Name)
       .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)  
       .Take(paginationRequest.PageSize)
       .ToListAsync();


        return new PaginationResponse<Lesson>(totalCount, lessons);
    }

    public async Task<Lesson> GetLessonByIdAsync(int id)
    {
        var lesson = await _context.Lessons
          
            .Include(l => l.Group)
            .Include(l => l.StudentLessons)
                .ThenInclude(sl => sl.Student)
            .Include(l => l.LessonTopics)
                .ThenInclude(lt => lt.Topic)
            .FirstOrDefaultAsync();

        return lesson;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsByGroupIdAsync(int groupId)
    {
        return await _context.Lessons
           
            .Include(l => l.StudentLessons)
                .ThenInclude(sl => sl.Student)
            .Include(l => l.LessonTopics)
                .ThenInclude(lt => lt.Topic)
            .ToListAsync();
    }

    public async Task<Group> GetGroupByNameAsync(string groupName)
    {
        return await _context.Groups
            .Where(g => g.Name == groupName && !g.IsDeleted)  
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Topic>> GetTopicsByNamesAsync(IEnumerable<string> topicNames)
    {
        return await _context.Topics
            .Where(topic => topicNames.Contains(topic.Name) )  
            .ToListAsync();
    }
}
