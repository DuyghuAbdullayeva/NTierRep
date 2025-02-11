using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebApplicationCourseNTier.API.Configs;
using WebApplicationCourseNTier.Business.Validators;
using WebApplicationCourseNTier.Business.Extensions;
using WebApplicationCourseNTier.DataAccess.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;

namespace WebApplicationCourseNTier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
               .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<RegisterUserDtoValidator>());
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllers(opts => opts.Conventions
                                             .Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer())));

            builder.Services.AddBusinessLayer();
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddHttpClient();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(opt =>
             {
                 opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateIssuerSigningKey = true,
                     ValidateLifetime = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     // Fixed typo here
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                 };


             
             });

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
