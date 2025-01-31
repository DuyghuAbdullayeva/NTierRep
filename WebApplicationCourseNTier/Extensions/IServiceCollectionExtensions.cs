//using Microsoft.EntityFrameworkCore;
//using WebApplicationCourseNTier.Business.Services.Abstractions;
//using WebApplicationCourseNTier.DataAccess.Data;
//using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
//using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
//using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;
//using AutoMapper;
//using WebApplicationCourseNTier.DataAccess.Repositories.Implementations.Base;
//using WebApplicationNTier.DataAccess.Repositories.Implementations.Base;
//using WebApplicationCourseNTier.Business.Services.Implementations;
//using WebApplicationCourseNTier.Business.Services;

//namespace WebApplicationCourseNTier.API.Extensions
//{
//    public static class IServiceCollectionExtensions
//    {
//        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
//        { 
//            services.AddScoped<IStudentService, StudentService>();
//            services.AddScoped<IGroupService, GroupService>();
//            services.AddScoped<ISubjectService, SubjectService>();
//            services.AddScoped<ITeacherService, TeacherService>();
//            services.AddScoped<ILessonService, LessonService>();
//            services.AddScoped<ITopicService, TopicService>();
//            services.AddScoped<IStudentLessonService, StudentLessonService>();
//            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//            return services;

//        }
//        public static IServiceCollection AddDataAccessLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
//        {
//            serviceCollection.AddDbContext<CourseSystemArcDBContext>(options =>
//            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

          

//            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
//            return serviceCollection;
//        }
//    }
//}

////serviceCollection.AddScoped<IGroupRepository, GroupRepository>();
////serviceCollection.AddScoped<ISubjectRepository, SubjectRepository>();
////serviceCollection.AddScoped<ITeacherRepository, TeacherRepository>();
////serviceCollection.AddScoped<IStudentRepository, StudentRepository>();