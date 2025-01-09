namespace Models.Entities;
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public DateTime LastUpdated { get; set; }
    public int? LocationId { get; set; }
    // public Location Location { get; set; }
    // public ICollection<ContactPerson> ContactPersons { get; set; }
    // public ICollection<InterestApplication> InterestApplications { get; set; }
    // public ICollection<LIAPitch> LIAPitches { get; set; }
    // public ICollection<Employment> Employments { get; set; }
}