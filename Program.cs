using Microsoft.EntityFrameworkCore;
using Models.Entities;

Console.WriteLine("Hello, Krister!");


using (var context = new Context())
{
    var c = context.Companies.Where(c => c.Id == 1).Include(c => c.ContactPersons).Single();
    Console.WriteLine(c.Name);
}

