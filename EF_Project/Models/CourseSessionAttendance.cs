namespace EF_Project.Models;

public class CourseSessionAttendance
{
    public int Id { get; set; }
    public int CourseSessionId { get; set; }
    public int StudentId { get; set; }
    public int? Grade { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public virtual CourseSession CourseSession { get; set; } = null!;
    public virtual Student Student { get; set; } = null!;
}
