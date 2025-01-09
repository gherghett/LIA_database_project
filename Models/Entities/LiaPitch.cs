namespace Models.Entities;

public class LiaPitch
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Year { get; set; }  = null!;
    public bool HasOccurred { get; set; }
    public Company Company { get; set; } = null!;
}