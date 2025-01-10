using InputHandler;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using ConsoleTables;

Console.WriteLine("Hello, Krister!");

ContextFactory factory = new();
Services.CompanyManager companyManager = new(factory);
Services.SchoolManager schoolManager = new(factory, companyManager);

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
        .AddScreen("Se Överblick", schoolManager.Overview)
        .AddScreen("Lägg till student", schoolManager.AddStudent)
        .AddScreen("Lägg till anställning till fäljd av LIA", schoolManager.AddEmployment)
        .Done()
    .AddQuit("Avsluta programmet")
    .Enter();

