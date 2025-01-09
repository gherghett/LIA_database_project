namespace Models.Entities;
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Url { get; set; } = null;
    public DateTime LastUpdated { get; set; }
    public Location Location { get; set; } = null!;
    public ICollection<ContactPerson> ContactPersons { get; set; } = null!;
    public ICollection<InterestApp> InterestApplications { get; set; } = null!;
    public ICollection<LiaPitch> LIAPitches { get; set; } = null!;
    public ICollection<Employment> Employments { get; set; } = null!;
}