using Domain;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<DataSource> DataSources { get; set; }
    public DbSet<SensorReading> SensorReadings { get; set; }
}