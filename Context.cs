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
    public DbSet<StudyProgram> Programs { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Employment> Employments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LIA.db");
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
