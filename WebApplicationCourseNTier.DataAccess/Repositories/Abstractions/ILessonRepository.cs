using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Models;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Abstractions
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<PaginationResponse<Lesson>> GetAllLessonAsync(PaginationRequest paginationRequest);
        Task<Lesson> GetLessonByIdAsync(int id);
        Task<Group> GetGroupByNameAsync(string groupName);
        Task<IEnumerable<Topic>> GetTopicsByNamesAsync(IEnumerable<string> topicNames);
        Task<IEnumerable<Lesson>> GetLessonsByGroupIdAsync(int groupId);
        Task<List<StudentLesson>> GetStudentLessonsByLessonIdAsync(int lessonId);
    }
}
