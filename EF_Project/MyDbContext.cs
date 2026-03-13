using EF_Project.Configurations;
using EF_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Project;

public class MyDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSession> CourseSessions { get; set; }
    public DbSet<CourseSessionAttendance> CourseSessionAttendances { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-J7ET2UU\\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new CourseSessionConfiguration());
        modelBuilder.ApplyConfiguration(new CourseSessionAttendanceConfiguration());
        modelBuilder.ApplyConfiguration(new CourseStudentConfiguration());

        modelBuilder.Entity<Instructor>()
            .HasOne(i => i.Department)
            .WithMany(d => d.Instructors)
            .HasForeignKey(i => i.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, Name = "Computer Science", Location = "Building A" },
            new Department { DepartmentId = 2, Name = "Mathematics", Location = "Building B" },
            new Department { DepartmentId = 3, Name = "Physics", Location = "Building C" }
        );

        modelBuilder.Entity<Instructor>().HasData(
            new Instructor { Id = 1, DepartmentId = 1, FirstName = "John", LastName = "Smith", Phone = "555-0101" },
            new Instructor { Id = 2, DepartmentId = 1, FirstName = "Jane", LastName = "Doe", Phone = "555-0102" },
            new Instructor { Id = 3, DepartmentId = 2, FirstName = "Robert", LastName = "Johnson", Phone = "555-0103" },
            new Instructor { Id = 4, DepartmentId = 3, FirstName = "Emily", LastName = "Brown", Phone = "555-0104" }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FirstName = "Alice", LastName = "Williams", Phone = "555-1001" },
            new Student { Id = 2, FirstName = "Bob", LastName = "Davis", Phone = "555-1002" },
            new Student { Id = 3, FirstName = "Charlie", LastName = "Miller", Phone = "555-1003" },
            new Student { Id = 4, FirstName = "Diana", LastName = "Wilson", Phone = "555-1004" },
            new Student { Id = 5, FirstName = "Edward", LastName = "Moore", Phone = "555-1005" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, DepartmentId = 1, InstructorId = 1, Duration = 60, Name = "Introduction to Programming" },
            new Course { Id = 2, DepartmentId = 1, InstructorId = 2, Duration = 90, Name = "Data Structures" },
            new Course { Id = 3, DepartmentId = 2, InstructorId = 3, Duration = 45, Name = "Calculus I" },
            new Course { Id = 4, DepartmentId = 3, InstructorId = 4, Duration = 60, Name = "Physics 101" }
        );

        modelBuilder.Entity<CourseSession>().HasData(
            new CourseSession { Id = 1, CourseId = 1, InstructorId = 1, Date = new DateTime(2024, 1, 15), Title = "Variables and Types" },
            new CourseSession { Id = 2, CourseId = 1, InstructorId = 1, Date = new DateTime(2024, 1, 22), Title = "Control Flow" },
            new CourseSession { Id = 3, CourseId = 2, InstructorId = 2, Date = new DateTime(2024, 1, 16), Title = "Arrays and Lists" },
            new CourseSession { Id = 4, CourseId = 3, InstructorId = 3, Date = new DateTime(2024, 1, 17), Title = "Limits" },
            new CourseSession { Id = 5, CourseId = 4, InstructorId = 4, Date = new DateTime(2024, 1, 18), Title = "Newton's Laws" }
        );

        modelBuilder.Entity<CourseStudent>().HasData(
            new CourseStudent { CourseId = 1, StudentId = 1 },
            new CourseStudent { CourseId = 1, StudentId = 2 },
            new CourseStudent { CourseId = 1, StudentId = 3 },
            new CourseStudent { CourseId = 2, StudentId = 1 },
            new CourseStudent { CourseId = 2, StudentId = 4 },
            new CourseStudent { CourseId = 3, StudentId = 2 },
            new CourseStudent { CourseId = 3, StudentId = 5 },
            new CourseStudent { CourseId = 4, StudentId = 3 },
            new CourseStudent { CourseId = 4, StudentId = 4 },
            new CourseStudent { CourseId = 4, StudentId = 5 }
        );

        modelBuilder.Entity<CourseSessionAttendance>().HasData(
            new CourseSessionAttendance { Id = 1, CourseSessionId = 1, StudentId = 1, Grade = 95, Notes = "Excellent participation" },
            new CourseSessionAttendance { Id = 2, CourseSessionId = 1, StudentId = 2, Grade = 88, Notes = "Good work" },
            new CourseSessionAttendance { Id = 3, CourseSessionId = 2, StudentId = 1, Grade = 92, Notes = null },
            new CourseSessionAttendance { Id = 4, CourseSessionId = 3, StudentId = 1, Grade = 85, Notes = "Needs improvement" },
            new CourseSessionAttendance { Id = 5, CourseSessionId = 4, StudentId = 2, Grade = 90, Notes = null },
            new CourseSessionAttendance { Id = 6, CourseSessionId = 5, StudentId = 3, Grade = 78, Notes = "Late submission" }
        );
    }
}
