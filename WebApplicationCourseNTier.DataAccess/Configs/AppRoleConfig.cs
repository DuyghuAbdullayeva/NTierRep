using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Configs.Base;
using WebApplicationCourseNTier.DataAccess.Enums;

namespace WebApplicationCourseNTier.DataAccess.Configs
{
    public class AppRoleConfig : IEntityConfig<AppRole>
    {
        public override void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Define properties for the AppRole entity
            builder.Property(x => x.Name).IsRequired();

            // Seed initial data for roles
            builder.HasData(new AppRole[]
            {
                new AppRole
                {
                    Id = (int)RoleEnum.Admin,
                    Name = RoleEnum.Admin.ToString()
                },
                new AppRole
                {
                    Id = (int)RoleEnum.Teacher,
                    Name = RoleEnum.Teacher.ToString()
                },
                new AppRole
                {
                    Id = (int)RoleEnum.Student,
                    Name = RoleEnum.Student.ToString()
                }
            });

            // Call base class configuration (if necessary)
            base.Configure(builder);
        }
    }
}
