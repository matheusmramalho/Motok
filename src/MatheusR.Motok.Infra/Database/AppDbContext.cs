using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.OtherTables;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MatheusR.Motok.Infra.Database;
public class AppDbContext : DbContext
{
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Motorcycle2024> Motorcycles2024 { get; set; }

    #region Authentication
    
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    #endregion

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
