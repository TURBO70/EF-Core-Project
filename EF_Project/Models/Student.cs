using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Project.Models;

[Table("Student")]
public class Student
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; } = null!;

    [MaxLength(255)]
    public string? Phone { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    public virtual ICollection<CourseSessionAttendance> CourseSessionAttendances { get; set; } = new List<CourseSessionAttendance>();
}
