using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Data
{
    public class CourseSystemArcDBContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<LessonTopic> LessonTopics { get; set; }
        public DbSet<FileDetail> FileDetail { get; set; } // Pluralized the name here
        public DbSet<User> Users { get; set; }

        // Constructor to accept DbContext options
        public CourseSystemArcDBContext(DbContextOptions<CourseSystemArcDBContext> options)
            : base(options)
        {
        }

        // Override OnModelCreating to apply all configurations from the assembly
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        // Override SaveChangesAsync to handle soft deletes and manage audit fields (timestamps)
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>(); // ChangeTracker tracks all entity states

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    // Set the CreateDate for newly added entities
                    entry.Entity.CreateDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Set the UpdateDate for modified entities
                    entry.Entity.UpdateDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Soft delete logic: mark the entity as deleted without removing it
                    if (entry.Entity is BaseEntity baseEntity)
                    {
                        entry.State = EntityState.Modified; // Change state to Modified to prevent physical deletion
                        baseEntity.IsDeleted = true; // Set the IsDeleted flag to true
                        baseEntity.DeleteDate = DateTime.Now; // Set the DeleteDate
                    }
                }
            }

            // Call the base SaveChangesAsync method to persist changes to the database
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}



//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection;
//using WebApplicationCourseNTier.DataAccess.Entities;
//using WebApplicationCourseNTier.DataAccess.Entities.Base;

//namespace WebApplicationCourseNTier.DataAccess.Data
//{
//    public class CourseSystemArcDBContext : IdentityDbContext<User>
//    {
//        public DbSet<Group> Groups { get; set; }
//        public DbSet<Student> Students { get; set; }
//        public DbSet<GroupStudent> GroupStudents { get; set; }
//        public DbSet<Subject> Subjects { get; set; }
//        public DbSet<Teacher> Teachers { get; set; }
//        public DbSet<Lesson> Lessons { get; set; }
//        public DbSet<StudentLesson> StudentLessons { get; set; }
//        public DbSet<Topic> Topics { get; set; }
//        public DbSet<LessonTopic> LessonTopics { get; set; }
//        public DbSet<FileDetail> FileDetail { get; set; }
//        public DbSet<User> Users { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//            base.OnModelCreating(modelBuilder);
//        }
//        public CourseSystemArcDBContext(DbContextOptions<CourseSystemArcDBContext> options)
//        : base(options)
//        {
//        }
//        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//        {
//            var entries = ChangeTracker.Entries<BaseEntity>();

//            foreach (var entry in entries)
//            {
//                if (entry.State == EntityState.Added)
//                {
//                    entry.Entity.CreateDate = DateTime.Now;

//                }
//                else if (entry.State == EntityState.Modified)
//                {
//                    entry.Entity.UpdateDate = DateTime.Now;
//                }
//                else if (entry.State == EntityState.Deleted)
//                {

//                    if (entry.Entity is BaseEntity baseEntity)
//                    {
//                        entry.State = EntityState.Modified;
//                        baseEntity.IsDeleted = true;
//                        baseEntity.DeleteDate = DateTime.Now;
//                    }
//                }
//            }


//            return await base.SaveChangesAsync(cancellationToken);
//        }



//    }
//}

