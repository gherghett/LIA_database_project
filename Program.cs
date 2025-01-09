using InputHandler;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ConsoleTables;

Console.WriteLine("Hello, Krister!");
    
MenuBuilder.CreateMenu("Välkommen! Main Menu för -\\'LIA DB PROGRAMMET'/-")
    .AddScreen("See överblick", Overview)
    .AddQuit("Avsluta programmet")
    .Enter();

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

