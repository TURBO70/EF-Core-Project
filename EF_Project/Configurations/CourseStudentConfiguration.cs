using EF_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Project.Configurations;

public class CourseStudentConfiguration : IEntityTypeConfiguration<CourseStudent>
{
    public void Configure(EntityTypeBuilder<CourseStudent> builder)
    {
        builder.ToTable("Course_Student");

        builder.HasKey(cs => new { cs.CourseId, cs.StudentId });

        builder.Property(cs => cs.CourseId)
            .HasColumnName("CourseID");

        builder.Property(cs => cs.StudentId)
            .HasColumnName("StudentID");

        builder.HasOne(cs => cs.Course)
            .WithMany(c => c.CourseStudents)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.Student)
            .WithMany(s => s.CourseStudents)
            .HasForeignKey(cs => cs.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
