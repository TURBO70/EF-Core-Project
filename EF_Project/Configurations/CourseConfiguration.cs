using EF_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Project.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("ID");

        builder.Property(c => c.DepartmentId)
            .HasColumnName("Department ID");

        builder.Property(c => c.InstructorId)
            .HasColumnName("InstructorID");

        builder.Property(c => c.Duration);

        builder.Property(c => c.Name)
            .HasMaxLength(255);

        builder.HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Instructor)
            .WithMany(i => i.Courses)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
