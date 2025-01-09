using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using Models.Entities;

public class Context : DbContext
{
    public DbSet<Company> Companies { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LIA.db");
    }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
