using EF_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Project.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Department");

        builder.HasKey(d => d.DepartmentId);

        builder.Property(d => d.DepartmentId)
            .HasColumnName("Department ID");

        builder.Property(d => d.ManagerId)
            .HasColumnName("ManagerID");

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(d => d.Location)
            .HasMaxLength(255);

        builder.HasOne(d => d.Manager)
            .WithOne(i => i.ManagedDepartment)
            .HasForeignKey<Department>(d => d.ManagerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
