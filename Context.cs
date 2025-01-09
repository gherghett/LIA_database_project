using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using Models.Entities;

public class Context : DbContext
{
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<ContactPerson> ContactPersons { get; set; } = null!;
    public DbSet<ContactDetail> ContactDetails { get; set; } = null!;
    public DbSet<InterestApp> InterestApps { get; set; } = null!;
    public DbSet<LiaPitch> LiaPitches { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudyProgram> StudyPrograms { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Employment> Employments { get; set; } = null!;

    public IQueryable<Company> CompaniesWithIncludes
    {
        get
        {
            return Companies
                .Include(c => c.LIAPitches)
                .Include(c => c.InterestApps)
                .Include(c => c.Location)
                .Include(c => c.ContactPersons)
                    .ThenInclude(cp => cp.ContactDetails);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LIA.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("CURRENT_DATE");

            entity.HasOne(e => e.Location)
                  .WithMany(l => l.Companies);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<ContactPerson>(entity =>
        {
            entity.HasOne(e => e.Company)
                    .WithMany(c => c.ContactPersons);
        });

        modelBuilder.Entity<ContactDetail>(entity =>
        {
            entity.HasOne(e => e.ContactPerson)
                    .WithMany(c => c.ContactDetails);
        });

        modelBuilder.Entity<InterestApp>(entity =>
        {
            entity.HasOne(e => e.Company)
                    .WithMany(c => c.InterestApps);
        });

        modelBuilder.Entity<LiaPitch>(entity =>
        {
            entity.HasOne(e => e.Company)
                    .WithMany(c => c.LIAPitches);
        });

        modelBuilder.Entity<Student>(entity =>
        {

            entity.HasOne(e => e.StudyProgram)
                    .WithMany(p => p.Students);

            entity.HasOne(e => e.Class)
                    .WithMany(c => c.Students);
        });

        modelBuilder.Entity<StudyProgram>(entity =>
        {
            // Students defined already in Students
            entity.HasMany(e => e.Classes)
                .WithOne(c => c.StudyProgram);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            //Students defined
            //StudyProgram defined
        });

        modelBuilder.Entity<Employment>(entity =>
        {
            entity.HasOne(e => e.Student)
                    .WithMany(s => s.Employments);

            entity.HasOne(e => e.Company)
                    .WithMany(c => c.Employments);
        });

        // ****************** Seed data *************

        // Seed data for Locations
        modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, Name = "Borås" },
            new Location { Id = 2, Name = "Göteborg" },
            new Location { Id = 3, Name = "Ulricehamn" }
        );

        // Seed data for Companies
        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "NetOnNet", Url = "https://netonnet.com", LocationId = 1 },
            new Company { Id = 2, Name = "BitAddict", Url = "https://bitaddict.se", LocationId = 2 },
            new Company { Id = 3, Name = "Advacy/Imerge", Url = "https://www.advacy.se", LocationId = 3 }
        );

        // Seed data for Contact Persons
        modelBuilder.Entity<ContactPerson>().HasData(
            new ContactPerson { Id = 1, Name = "Daniel Theliander", Position = "VD", Ranking = 1, CompanyId = 1 },
            new ContactPerson { Id = 2, Name = "Helena Bragée", Position = "CEO", Ranking = 1, CompanyId = 2 },
            new ContactPerson { Id = 3, Name = "Roland Svensson", Position = "VD", Ranking = 1, CompanyId = 3 }
        );

        // Seed data for Contact Details
        modelBuilder.Entity<ContactDetail>().HasData(
            new ContactDetail { Id = 1, ContactPersonId = 1, ContactInfo = "07070707"},
            new ContactDetail { Id = 2, ContactPersonId = 2, ContactInfo = "07099999"},
            new ContactDetail { Id = 3, ContactPersonId = 3, ContactInfo = "Svenson@Advacy.com"},
            new ContactDetail { Id = 4, ContactPersonId = 3, ContactInfo = "033 - 011111"}
        );


        // Seed data for Interest Applications
        modelBuilder.Entity<InterestApp>().HasData(
            new InterestApp { Id = 1, CompanyId = 1, Year = "2024" },
            new InterestApp { Id = 2, CompanyId = 2, Year = "2023" },
            new InterestApp { Id = 3, CompanyId = 3, Year = "2024" }
        );

        // Seed data for LIA Pitches
        modelBuilder.Entity<LiaPitch>().HasData(
            new LiaPitch { Id = 1, CompanyId = 1, Year = "2024", HasOccurred = false },
            new LiaPitch { Id = 2, CompanyId = 2, Year = "2023", HasOccurred = true },
            new LiaPitch { Id = 3, CompanyId = 3, Year = "2024", HasOccurred = false }
        );

        // Seed data for Programs
        modelBuilder.Entity<StudyProgram>().HasData(
            new StudyProgram { Id = 1, Name = "SUVNET" },
            new StudyProgram { Id = 2, Name = "FRONTEND" }
        );

        // Seed data for Classes
        modelBuilder.Entity<Class>().HasData(
            new Class { Id = 1, Name = "SUVNET21", Year = 2021, StudyProgramId = 1 },
            new Class { Id = 2, Name = "FRONTEND22", Year = 2022, StudyProgramId = 2 }
        );

        // Seed data for Students
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Alice Andersson", StudyProgramId = 1, ClassId = 1 },
            new Student { Id = 2, Name = "Bob Bengtsson", StudyProgramId = 2, ClassId = 2 }
        );

        // Seed data for Employment
        modelBuilder.Entity<Employment>().HasData(
            new Employment { Id = 1, StudentId = 1, CompanyId = 1, EmploymentDate = new DateTime(2024, 01, 15) },
            new Employment { Id = 2, StudentId = 2, CompanyId = 2, EmploymentDate = new DateTime(2023, 11, 30) }
        );

    }
}
