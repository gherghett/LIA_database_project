namespace Models.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ProgramId { get; set; }
    public int ClassId { get; set; }
    public StudyProgram Program { get; set; } = null!;
    public Class Class { get; set; } = null!;
    // public ICollection<Employment> Employments { get; set; }
}