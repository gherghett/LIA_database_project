namespace Models.Entities;
public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Company> Companies { get; set; } = null!;
}