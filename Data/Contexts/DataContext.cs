using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

   
    public DataContext() { }

    public DbSet<ProjectEntity> Projects { get; set; } = null!;
    public DbSet<CustomerEntity> Customers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jubra\source\repos\DataStorage_Assignment_Manuella_Inlämningsuppgift\Data\DataBases\DataStorage_Manuella.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }
}
