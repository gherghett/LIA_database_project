using InputHandler;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ConsoleTables;

Console.WriteLine("Hello, Krister!");
    
MenuBuilder.CreateMenu("Välkommen! Main Menu för -\\'LIA DB PROGRAMMET'/-")
    .AddScreen("See överblick", Overview)
    .AddMenu("Pilla med företagen")
        .AddScreen("Lägg till nytt företag", AddNewCompany)
        .Done()
    .AddQuit("Avsluta programmet")
    .Enter();

Location CreateNewLocation()
{
    string name = UserGet.GetString("Namn på orten");
    using (var context = new Context())
    {   
        var newLocation = new Location {Name = name};
        context.Add( newLocation);
        context.SaveChanges();
        return newLocation;
    }
}

void AddNewCompany()
{
    string name = InputHandler.UserGet.GetString("Namn på det nya företaget");
    using (var context = new Context())
    {
        var locations = context.Locations.ToList();
        var options = locations.Concat([new Location {Id = -1, Name = "Skapa ny"}])
            .Select(l => (l.Name, l))
            .ToArray();
        var chosenLocation = Chooser.ChooseAlternative<Location>("Välj en ort för företaget, ellr skapa en ny", options);
        if (chosenLocation.Id == -1)
        {
            chosenLocation = CreateNewLocation();
        }
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

