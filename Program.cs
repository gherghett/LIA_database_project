using InputHandler;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

Console.WriteLine("Hello, Krister!");
    
MenuBuilder.CreateMenu("Välkommen! Main Menu för -\\'LIA DB PROGRAMMET'/-")
    .AddScreen("See överblick", Overview)
    .AddQuit("Avsluta programmet")
    .Enter();

void Overview() 
{
    using (var context = new Context())
    {
        var companies = context.Companies.ToList();
        foreach( var company in companies)
        {
            Console.WriteLine(company.Name);
        }
    }
}

