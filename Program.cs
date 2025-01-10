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
    .AddQuit("Avsluta programmet")
    .Enter();









