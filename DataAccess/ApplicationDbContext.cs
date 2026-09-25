using EFproject.models;
using Microsoft.EntityFrameworkCore;

namespace EFproject.DataAccess
{
    internal class ApplicationDbContext: DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Student Configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);

                entity.Property(s => s.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .IsRequired();

                entity.Property(s => s.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .IsUnicode(false)
                    .IsRequired(false);

                entity.Property(s => s.RegisteredOn)
                    .IsRequired();

                entity.Property(s => s.Birthday)
                    .IsRequired(false);
            });

            // 2. Course Configuration
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);

                entity.Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true)
                    .IsRequired();

                entity.Property(c => c.Description)
                    .IsUnicode(true)
                    .IsRequired(false);

                entity.Property(c => c.StartDate)
                    .IsRequired();

                entity.Property(c => c.EndDate)
                    .IsRequired();

                entity.Property(c => c.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            // 3. Resource Configuration
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(r => r.ResourceId);

                entity.Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired();

                entity.Property(r => r.Url)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(r => r.ResourceType)
                    .IsRequired();

                // One Course -> Many Resources
                entity.HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(r => r.CourseId);
            });

            // 4. Homework Configuration
            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(h => h.HomeworkId);

                entity.Property(h => h.Content)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(h => h.ContentType)
                    .IsRequired();

                entity.Property(h => h.SubmissionTime)
                    .IsRequired();

                // One Student -> Many HomeworkSubmissions
                entity.HasOne(h => h.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(h => h.StudentId);

                // One Course -> Many HomeworkSubmissions
                entity.HasOne(h => h.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(h => h.CourseId);
            });

            // 5. StudentCourse Configuration (Many-to-Many Join Table)
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                entity.HasOne(sc => sc.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(sc => sc.StudentId);
                entity.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(sc => sc.CourseId);
            });
        }
    }

}
