namespace Models.Entities;

//Intressanmälan
public class InterestApp
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Year { get; set; } = null!;
    public Company Company { get; set; } = null!;
}