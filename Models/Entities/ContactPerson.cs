    namespace Models.Entities;

    public class ContactPerson
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Ranking { get; set; } // companies kan fler contactpersons, detta visar vilka so är primära
        public string Position { get; set; }  = null!;//tex "VD" 
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public ICollection<ContactDetail> ContactDetails { get; set; } = null!;
    }