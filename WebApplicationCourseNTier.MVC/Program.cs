using Microsoft.AspNetCore.Http.Features;
using WebApplicationCourseNTier.Business.Extensions;
using WebApplicationCourseNTier.Business.Services.Abstractions;
using WebApplicationCourseNTier.DataAccess.Extensions;

namespace WebApplicationCourseNTier.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddBusinessLayer();
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddHttpClient();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();
                Task.Run(async () => await roleService.SeedRolesAsync()).Wait();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.B
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
          
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}