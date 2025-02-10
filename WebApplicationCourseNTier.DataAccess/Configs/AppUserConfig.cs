using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.DataAccess.Configs.Base
{
    public class AppUserConfig : IEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Set UserName as required and limit its length
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50); // You can set a reasonable length here, for example, 50 chars

            // Set Password as required and limit its length (consider security best practices)
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(256); // Ensure it's long enough to store hashed passwords

            // Set Email as required, with validation for correct format
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100) // Reasonable length for emails
                .HasAnnotation("Email", "true"); // You can customize this for more specific validation

            // Set Phone as required and limit its length (optional, based on your validation rules)
            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(15); // Assuming international phone number format

            // If necessary, index frequently used columns to improve performance
            builder.HasIndex(x => x.UserName).IsUnique();  // Ensure UserName is unique
            builder.HasIndex(x => x.Email).IsUnique();     // Ensure Email is unique

            // Call base class configuration (if it contains other shared configurations)
            base.Configure(builder);
        }
    }
}
