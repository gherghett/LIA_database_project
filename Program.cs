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
        .AddScreen("Lägg till student", AddStudent)
        .AddScreen("Lägg till anställning till fäljd av LIA", AddEmployment)
        .Done()
    .AddQuit("Avsluta programmet")
    .Enter();

void AddStudent()
{
    using(var context = factory.GetContext())
    {
        var programs = context.StudyPrograms;
        var options = programs.ToList()
            .Select(p => (p.Name, p))
            .ToArray();
        var chosenProgram = Chooser.ChooseAlternative<StudyProgram>("Välj program", options);
        var classes = context.Classes.Where(c => c.StudyProgramId == chosenProgram.Id);
        var classOptions = classes.ToList()
            .Select(c => (c.Name, c))
            .ToArray();
        var chosenClass = Chooser.ChooseAlternative<Class>("Välj Klass",classOptions);
        var namn = UserGet.GetString("Namn på student");
        var newStudent = new Student {
            Name = namn,
            StudyProgramId = chosenProgram.Id,
            ClassId = chosenClass.Id
        };
        context.Add(newStudent);
        context.SaveChanges();
    }
}

void AddEmployment()
{
    throw new NotImplementedException();
}

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