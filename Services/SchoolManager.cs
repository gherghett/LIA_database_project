using Microsoft.EntityFrameworkCore;
using ConsoleTables;
using InputHandler;
using Models.Entities;

namespace Services;

public class SchoolManager
{
    private readonly ContextFactory factory;
    private readonly CompanyManager companyManager;
    public SchoolManager(ContextFactory factory, CompanyManager companyManager)
    {
        this.factory = factory;
        this.companyManager = companyManager;
    }

    public void AddStudent()
    {
        using (var context = factory.GetContext())
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
            var chosenClass = Chooser.ChooseAlternative<Class>("Välj Klass", classOptions);
            var namn = UserGet.GetString("Namn på student");
            var newStudent = new Student
            {
                Name = namn,
                StudyProgramId = chosenProgram.Id,
                ClassId = chosenClass.Id
            };
            context.Add(newStudent);
            context.SaveChanges();
        }
    }

    public void AddEmployment()
    {
        using (var context = factory.GetContext())
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
            var chosenClass = Chooser.ChooseAlternative<Class>("Välj Klass", classOptions);
            var students = context.Students.Where(s => s.ClassId == chosenClass.Id);
            var studentOptions = students.ToList()
                .Select(s => (s.Name, s))
                .ToArray();
            var chosenStudent = Chooser.ChooseAlternative<Student>("Välj student", studentOptions);
            var company = companyManager.ChooseCompany();
            if (company is null) return;
            var newEmployment = new Employment
            {
                StudentId = chosenStudent.Id,
                CompanyId = company.Id,
                EmploymentDate = UserGet.GetDateTime("När skedde eller sker anställningen?")
            };
            context.Add(newEmployment);
            context.SaveChanges();
        }
    }

    public void Overview()
    {
        using (var context = factory.GetContext())
        {
            var classes = context.Classes
                .Include(c => c.Students)
                    .ThenInclude(s => s.Employments)
                        .ThenInclude(e => e.Company);
            var table = new ConsoleTable(
               "Klass", "Studenter", "Anställningar till följd av LIA");

            foreach (var c in classes)
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
}