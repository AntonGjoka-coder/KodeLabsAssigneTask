using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces.Common
{
    public interface IApplicationDbContext
    {
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}