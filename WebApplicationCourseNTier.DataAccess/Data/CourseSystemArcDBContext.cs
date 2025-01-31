
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplicationCourseNTier.DataAccess.Entities;
using WebApplicationCourseNTier.DataAccess.Entities.Base;

namespace WebApplicationCourseNTier.DataAccess.Data
{
    public class CourseSystemArcDBContext : IdentityDbContext<User>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<StudentLesson>  StudentLessons { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<LessonTopic> LessonTopics {  get; set; }
        public DbSet<FileDetail> FileDetail { get; set; }
        public DbSet<User> Users {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public CourseSystemArcDBContext(DbContextOptions<CourseSystemArcDBContext> options)
        : base(options) 
        {
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.Now; 
                    
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateDate = DateTime.Now; 
                }
                else if (entry.State == EntityState.Deleted)
                {
                   
                    if (entry.Entity is BaseEntity baseEntity)
                    {
                        entry.State = EntityState.Modified;
                        baseEntity.IsDeleted = true; 
                        baseEntity.DeleteDate = DateTime.Now; 
                    }
                }
            }

          
            return await base.SaveChangesAsync(cancellationToken);
        }



    }
}

