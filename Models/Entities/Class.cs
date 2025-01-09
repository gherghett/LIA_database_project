namespace Models.Entities;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public int StudyProgramId { get; set; }
    public StudyProgram StudyProgram { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = null!;
}