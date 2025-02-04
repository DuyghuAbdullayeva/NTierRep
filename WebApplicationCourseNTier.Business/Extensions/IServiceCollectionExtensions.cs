using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.Business.Services.Implementations;
using WebApplicationCourseNTier.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using CourseSystem.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IStudentLessonService, StudentLessonService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}