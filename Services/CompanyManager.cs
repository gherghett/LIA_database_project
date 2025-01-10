using ConsoleTables;

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

}