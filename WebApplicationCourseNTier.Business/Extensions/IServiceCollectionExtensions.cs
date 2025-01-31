using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;
using AutoMapper;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;
using WebApplicationNTier.DataAccess.Repositories.Implementations.Base;
using WebApplicationCourseNTier.Business.Services.Implementations;
using WebApplicationCourseNTier.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using CourseSystem.Services.Implementations;

namespace WebApplicationCourseNTier.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
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
