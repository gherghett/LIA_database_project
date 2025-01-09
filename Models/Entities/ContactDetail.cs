namespace Models.Entities;

public class ContactDetail
{
    public int Id { get; set; }
    public int ContactPersonId { get; set; }

    //Detta kan vara email telefon vadsomhelst
    public string ContactInfo { get; set; } = null!;
    public ContactPerson ContactPerson { get; set; } = null!;
}