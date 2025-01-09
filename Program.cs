using Models.Entities;

Console.WriteLine("Hello, Krister!");


using (var context = new Context())
{
    var count = context.Companies.Count();
    Console.WriteLine($"Number of companies: {count}");
}

