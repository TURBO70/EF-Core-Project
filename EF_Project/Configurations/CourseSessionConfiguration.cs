using EF_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Project.Configurations;

public class CourseSessionConfiguration : IEntityTypeConfiguration<CourseSession>
{
    public void Configure(EntityTypeBuilder<CourseSession> builder)
    {
        builder.ToTable("CourseSession");

        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.Id)
            .HasColumnName("Id");

        builder.Property(cs => cs.CourseId)
            .HasColumnName("CourseID");

        builder.Property(cs => cs.InstructorId)
            .HasColumnName("InstructorID");

        builder.Property(cs => cs.Date)
            .IsRequired();

        builder.Property(cs => cs.Title)
            .HasMaxLength(255);

        builder.HasOne(cs => cs.Course)
            .WithMany(c => c.CourseSessions)
            .HasForeignKey(cs => cs.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.Instructor)
            .WithMany(i => i.CourseSessions)
            .HasForeignKey(cs => cs.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
