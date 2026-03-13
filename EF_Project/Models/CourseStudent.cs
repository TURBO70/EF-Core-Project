namespace EF_Project.Models;

public class CourseStudent
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    // Navigation properties
    public virtual Course Course { get; set; } = null!;
    public virtual Student Student { get; set; } = null!;
}
