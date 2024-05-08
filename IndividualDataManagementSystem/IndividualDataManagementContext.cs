using Microsoft.EntityFrameworkCore;

public class IndividualDataManagementContext : DbContext
{
    public DbSet<Individual> Individuals { get; set; }

    public IndividualDataManagementContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Individual>().HasIndex(individual => individual.PhoneNumber).IsUnique();
        modelBuilder.Entity<Individual>().HasIndex(individual => individual.PassportNumber).IsUnique();
        modelBuilder.Entity<Individual>().HasIndex(individual => individual.TaxNumber).IsUnique();
    }
}