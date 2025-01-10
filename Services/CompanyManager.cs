using Microsoft.EntityFrameworkCore;
using ConsoleTables;
using InputHandler;
using Models.Entities;

namespace Services;

public class CompanyManager
{
    private readonly ContextFactory factory;
    public CompanyManager(ContextFactory factory)
    {
        this.factory = factory;
    }

    public void Overview()
    {
        using (var context = factory.GetContext())
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

    public void AddNewCompany()
    {
        string name = InputHandler.UserGet.GetString("Namn på det nya företaget");
        string url = InputHandler.UserGet.GetString("företagets hemsida");

        using (var context = factory.GetContext())
        {

            // Ort
            var locations = context.Locations.ToList();
            // Vi lägger till en Location som representerar att den vi söker inte finns
            var options = locations.Concat([new Location { Id = -1, Name = "Skapa ny" }])
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
            var company = new Company
            {
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

    public void ChangeLiaPitch()
    {
        var company = ChooseCompany();
        if (company is null) return;

        using (var context = new Context())
        {
            var pitches = context.LiaPitches.Where(i => i.CompanyId == company.Id).ToList();
            Console.WriteLine($"{company.Name} har lia pitcher: {String.Join(", ", pitches.Select(a => a.Year + $"{(a.HasOccurred ? "x" : "o")}"))}");
            MenuBuilder.CreateMenu("")
                .AddScreen("Lägg till planerad pitch", () =>
                {
                    pitches = context.LiaPitches.Where(i => i.CompanyId == company.Id).ToList();

                    int current_year = DateTime.Now.Year;
                    int year;
                    do
                    {
                        year = UserGet.GetYear("Ange detta år eller framtida år");
                    } while (year < current_year);
                    if (pitches.Any(i => i.Year == year.ToString()))
                    {
                        Console.WriteLine("Detta årets finns redan tillagt.");
                    }
                    else
                    {
                        context.Add(new LiaPitch { Year = year.ToString(), CompanyId = company.Id });
                        context.SaveChanges();
                        Console.WriteLine($"{year} lades till till {company.Name}");
                    }
                })
                .AddScreen("Markera pitch som har ägt rum",  () => {
                    pitches = context.LiaPitches.Where(i => i.CompanyId == company.Id).ToList();

                    if(!pitches.Any(lp => !lp.HasOccurred)) 
                    {
                        Console.WriteLine("Finns ingen pitch som inte är markerad som ägt rum");
                    }
                    else
                    {
                        var options = pitches
                            .Select(p => (p.Year, p))
                            .ToArray();
                        var toMark = Chooser.ChooseAlternative<LiaPitch>("Välj pitch att markera", options);
                        toMark.HasOccurred = true;
                        context.SaveChanges();
                    }
                })
                .AddQuit("Tillbaka")
            .Enter();
        }
    }

    public void ChangeInterestApp()
    {
        var company = ChooseCompany();
        if (company is null) return;

        using (var context = new Context())
        {
            var apps = context.InterestApps.Where(i => i.CompanyId == company.Id);
            Console.WriteLine($"{company.Name} has interest for year(s): {String.Join(", ", apps.Select(a => a.Year))}");
            MenuBuilder.CreateMenu("")
                .AddScreen("Lägg till anmälan", () =>
                {
                    apps = context.InterestApps.Where(i => i.CompanyId == company.Id);  
                    int current_year = DateTime.Now.Year;
                    int year;
                    do
                    {
                        year = UserGet.GetYear("Ange detta år eller framtida år");
                    } while (year < current_year);
                    if (apps.Any(i => i.Year == year.ToString()))
                    {
                        Console.WriteLine("Detta årets finns redan tillagt.");
                    }
                    else
                    {
                        context.Add(new InterestApp { Year = year.ToString(), CompanyId = company.Id });
                        context.SaveChanges();
                        Console.WriteLine($"{year} lades till till {company.Name}");
                    }
                })
                .AddQuit("Tillbaka")
            .Enter();
        }
    }

    public void ChangeCompanyBaseInfo()
    {
        var company = ChooseCompany();
        if (company is null) return;


        using (var context = new Context())
        {
            company = context.Companies.Where(c => c.Id == company.Id).FirstOrDefault();
            if (company is null) throw new Exception("Kunde inte hitta företaget i databasen");

            MenuBuilder.CreateMenu("Vad vill du ändra?")
                .AddScreen($"Ändra namn", () => company.Name = UserGet.GetString("Nytt namn"))
                .AddScreen($"Ändra url", () => company.Url = UserGet.GetString("Ny url"))
                .AddQuit("Färdig")
            .Enter();

            company.LastUpdated = DateTime.Now;
            context.SaveChanges();
        }
    }

    public void ViewAllContact()
    {
        var company = ChooseCompany();
        if (company is null) return;

        Console.WriteLine(company.Name);

        using (var context = new Context())
        {
            var contacts = context.ContactPersons
                .Where(c => c.CompanyId == company.Id)
                .Include(c => c.ContactDetails)
                .OrderBy(c => c.Ranking)
                .ToList();

            var table = new ConsoleTable(
                "Namn", "Position", "Ranking", "Kontakt");

            foreach (var contact in contacts)
            {
                table.AddRow(contact.Name, contact.Position, contact.Ranking, string.Join(", ", contact.ContactDetails.Select(cd => cd.ContactInfo)));
            }

            table.Write();
        }

    }

    public void AddContactToCompany()
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


    public Company? ChooseCompany()
    {
        using (var context = factory.GetContext())
        {
            var companies = context.Companies.ToList();
            var options = companies.Concat([new Company { Name = "Tillbaka", Id = -1 }])
                .Select(c => (c.Name, c))
                .ToArray();
            var chosenCompany = Chooser.ChooseAlternative<Company>("Välj ett av företagen", options);
            return chosenCompany.Id == -1 ? null : chosenCompany;
        }
    }
    Location CreateNewLocation()
    {
        string name = UserGet.GetString("Namn på orten");
        var newLocation = new Location { Name = name };
        return newLocation;
    }

    ContactPerson CreateContactPerson()
    {
        string newContactName = UserGet.GetString("Namn på kontakt");
        string position = UserGet.GetString("Dennes position");
        string contactInfo = UserGet.GetString("Kontakt Information");

        var contact = new ContactPerson
        {
            Name = newContactName,
            Position = position,
            Ranking = 1,
            ContactDetails = new List<ContactDetail>() { new ContactDetail { ContactInfo = contactInfo } }
        };

        return contact;
    }

}