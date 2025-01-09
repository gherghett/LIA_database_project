using InputHandler;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ConsoleTables;

Console.WriteLine("Hello, Krister!");
    
MenuBuilder.CreateMenu("Välkommen! Main Menu för -\\'LIA DB PROGRAMMET'/-")
    .AddScreen("See överblick", Overview)
    .AddMenu("Pilla med företagen")
        .AddScreen("Lägg till nytt företag", AddNewCompany)
        .AddMenu("Gör ändringar med ett företag")
            .AddScreen("Lägg till kontakt för företag", AddContactToCompany)
            .Done()
        .Done()
    .AddQuit("Avsluta programmet")
    .Enter();

void AddContactToCompany()
{
    var company = ChooseCompany();
    if (company is null) return;
    var newContact = CreateContactPerson();
    newContact.CompanyId = company.Id;
    using (var context = new Context())
    {
        newContact.Ranking = context.ContactPersons
            .Where(cp => cp.CompanyId == company.Id)
            .Select(cp => cp.Ranking)
            .OrderByDescending(r => r)
            .First()
             + 1;

        context.Add(newContact);
        context.SaveChanges();
    }

}

Company? ChooseCompany() 
{
    using (var context = new Context())
    {
        var companies = context.Companies.ToList();
        var options = companies.Concat([new Company{Name = "Tillbaka", Id = -1}])
            .Select(c => (c.Name, c))
            .ToArray();
        var chosenCompany = Chooser.ChooseAlternative<Company>("Välj ett av företagen", options);
        return chosenCompany.Id == -1 ? null : chosenCompany;
    }
}

Location CreateNewLocation()
{
    string name = UserGet.GetString("Namn på orten");
    var newLocation = new Location {Name = name};
    return newLocation;
}

ContactPerson CreateContactPerson()
{
    string newContactName = UserGet.GetString("Namn på kontakt");
    string position = UserGet.GetString("Dennes position");
    string contactInfo = UserGet.GetString("Kontakt Information");

    var contact = new ContactPerson {
        Name = newContactName,
        Position = position,
        Ranking = 1,
        ContactDetails = new List<ContactDetail>(){new ContactDetail { ContactInfo = contactInfo }}
    };

    return contact;
}

void AddNewCompany()
{
    string name = InputHandler.UserGet.GetString("Namn på det nya företaget");
    string url = InputHandler.UserGet.GetString("företagets hemsida");

    using (var context = new Context())
    {

        // Ort
        var locations = context.Locations.ToList();
        // Vi lägger till en Location som representerar att den vi söker inte finns
        var options = locations.Concat([new Location {Id = -1, Name = "Skapa ny"}])
            .Select(l => (l.Name, l))
            .ToArray();
        var chosenLocation = Chooser.ChooseAlternative<Location>("Välj en ort för företaget, eller skapa en ny", options);
        if (chosenLocation.Id == -1)
        {
            chosenLocation = CreateNewLocation();
        }

        // Handledare
        var contact = CreateContactPerson();
        
        //Create the company
        var company = new Company {
            Name = name,
            Url = url,
            Location = chosenLocation,
            ContactPersons = [contact]
        };

        // Save
        context.Add(company);
        context.SaveChanges();
    }

}

void Overview() 
{
    using (var context = new Context())
    {
        var companies = context.CompaniesWithIncludes.ToList();

        var table = new ConsoleTable(
            "Företag", "Ort", "Pitch", "Uppdaterat",
            "Senaste Intresse", "Handledare", "Position", "Telefon", "Webbplats");

        foreach (var company in companies)
        {
            var location = company.Location?.Name ?? "N/A";
            var lastUpdated = company.LastUpdated.ToShortDateString();
            var application = company.InterestApps?.OrderByDescending(y => y.Year).FirstOrDefault()?.Year ?? "N/A";
            var liaPitch = company.LIAPitches?.OrderByDescending(y => y.Year).FirstOrDefault()?.Year ?? "N/A";

            var primaryContact = company.ContactPersons?.OrderBy(cp => cp.Ranking).FirstOrDefault();
            var contactName = primaryContact?.Name ?? "N/A";
            var contactPosition = primaryContact?.Position ?? "N/A";
            var contactInfo = primaryContact?.ContactDetails?.FirstOrDefault()?.ContactInfo ?? "N/A";

            table.AddRow(
                company.Name, location, liaPitch, lastUpdated,
                application, contactName, contactPosition, contactInfo, company.Url);
        }

        table.Write();
    }
}

