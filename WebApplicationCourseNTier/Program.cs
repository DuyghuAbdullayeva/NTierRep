using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebApplicationCourseNTier.API.Configs;

using WebApplicationCourseNTier.Business.Extensions;
using WebApplicationCourseNTier.DataAccess.Extensions;
using Microsoft.AspNetCore.Http.Features;

namespace WebApplicationCourseNTier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Register controllers
            builder.Services.AddControllers();
            builder.Services.AddControllers(opts => opts.Conventions
                                             .Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer())));

            builder.Services.AddBusinessLayer();
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddHttpClient();
           

            // Register Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
