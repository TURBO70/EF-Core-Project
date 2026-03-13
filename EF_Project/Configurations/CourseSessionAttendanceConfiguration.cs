using EF_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Project.Configurations;

public class CourseSessionAttendanceConfiguration : IEntityTypeConfiguration<CourseSessionAttendance>
{
    public void Configure(EntityTypeBuilder<CourseSessionAttendance> builder)
    {
        builder.ToTable("CourseSessionAttendance");

        builder.HasKey(csa => csa.Id);

        builder.Property(csa => csa.Id)
            .HasColumnName("Id");

        builder.Property(csa => csa.CourseSessionId)
            .HasColumnName("CourseSessionId");

        builder.Property(csa => csa.StudentId)
            .HasColumnName("StudentID");

        builder.Property(csa => csa.Grade);

        builder.Property(csa => csa.Notes)
            .HasColumnType("varchar(max)");

        builder.HasOne(csa => csa.CourseSession)
            .WithMany(cs => cs.Attendances)
            .HasForeignKey(csa => csa.CourseSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(csa => csa.Student)
            .WithMany(s => s.CourseSessionAttendances)
            .HasForeignKey(csa => csa.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
