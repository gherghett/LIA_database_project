namespace Models.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProgramId { get; set; }
    public int ClassId { get; set; }
    // public Program Program { get; set; }
    // public Class Class { get; set; }
    // public ICollection<Employment> Employments { get; set; }
}