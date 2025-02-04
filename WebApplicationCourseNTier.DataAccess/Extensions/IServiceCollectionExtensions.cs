using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationCourseNTier.DataAccess.Data;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions;
using WebApplicationCourseNTier.DataAccess.Repositories.Abstractions.Base;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;

using WebApplicationNTier.DataAccess.Repositories.Implementations.Base;

namespace WebApplicationCourseNTier.DataAccess.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDataAccessLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<CourseSystemArcDBContext>(options =>
                              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //serviceCollection.AddScoped<IGroupRepository, GroupRepository>();
            //serviceCollection.AddScoped<IStudentRepository, StudentRepository>();

            serviceCollection.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
             .AddEntityFrameworkStores<CourseSystemArcDBContext>()
             .AddDefaultTokenProviders();

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IRoleRepository, RoleRepository>();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
