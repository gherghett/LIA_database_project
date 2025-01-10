using InputHandler;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ConsoleTables;

Console.WriteLine("Hello, Krister!");

ContextFactory factory = new();
Services.CompanyManager companyManager = new(factory);


MenuBuilder.CreateMenu("Välkommen! Main Menu för -\\'LIA DB PROGRAMMET'/-")
    .AddScreen("See överblick", companyManager.Overview)
    .AddMenu("Företag")
        .AddScreen("Lägg till nytt företag", companyManager.AddNewCompany)
        .AddScreen("Ändra bas-info", companyManager.ChangeCompanyBaseInfo)
        .AddScreen("Hantera Intresseanmälan", companyManager.ChangeInterestApp)
        .AddScreen("Hantera LiaPitch", companyManager.ChangeLiaPitch)
        .AddMenu("Hantera kontakter")
            .AddScreen("See kontakter", companyManager.ViewAllContact)
            .AddScreen("Lägg till kontakt", companyManager.AddContactToCompany)
            .Done()
        .Done()
    .AddMenu("Skola")
        .AddScreen("Se Överblick", Overview)
        .Done()
    .AddQuit("Avsluta programmet")
    .Enter();

void Overview()
{
    using(var context = factory.GetContext())
    {
        var classes  = context.Classes
            .Include(c => c.Students)
                .ThenInclude(s => s.Employments)
                    .ThenInclude( e => e.Company);
         var table = new ConsoleTable(
            "Klass", "Studenter", "Anställningar till följd av LIA" );
        
        foreach( var c in classes )
        {
            var employments = c.Students
                .Where(s => s.Employments.Any())
                .SelectMany(s => s.Employments)
                .Select(e => e.Company)
                .Distinct()
                .Select(c => c.Name);
                
            table.AddRow(c.Name, c.Students.Count(), string.Join(", ", employments));
        }

        table.Write();
    }
}