using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Project.Models;

[Table("Instructor")]
public class Instructor
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [Column("Department ID")]
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; } = null!;

    [MaxLength(255)]
    public string? Phone { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; } = null!;

    public virtual Department? ManagedDepartment { get; set; }
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<CourseSession> CourseSessions { get; set; } = new List<CourseSession>();
}
