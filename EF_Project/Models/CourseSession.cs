namespace EF_Project.Models;

public class CourseSession
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int InstructorId { get; set; }
    public DateTime Date { get; set; }
    public string? Title { get; set; }

    // Navigation properties
    public virtual Course Course { get; set; } = null!;
    public virtual Instructor Instructor { get; set; } = null!;
    public virtual ICollection<CourseSessionAttendance> Attendances { get; set; } = new List<CourseSessionAttendance>();
}
