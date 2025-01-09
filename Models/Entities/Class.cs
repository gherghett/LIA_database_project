namespace Models.Entities;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public int ProgramId { get; set; }
    public StudyProgram Program { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = null!;
}