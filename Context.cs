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
                    .WithMany(c => c.InterestApplications);
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
    }
}
