namespace Models.Entities;

// Detta symboliserar om någon blir anställd som resulatt av sin lia
public class Employment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CompanyId { get; set; }
    public DateTime EmploymentDate { get; set; }
    public Student Student { get; set; } = null!;
    public Company Company { get; set; } = null!;
}