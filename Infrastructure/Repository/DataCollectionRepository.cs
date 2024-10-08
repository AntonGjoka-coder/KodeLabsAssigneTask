using Domain;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class DataCollectionRepository : IDataCollectionRepository
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<DataCollectionRepository> _logger;

    public DataCollectionRepository(IApplicationDbContext dbContext, ILogger<DataCollectionRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task StoreReadingAsync(SensorReading reading)
    {
        try
        {
            await _dbContext.SensorReadings.AddAsync(reading);
        }
        catch (Exception e)
        {
           _logger.LogError(e.Message);
            throw;
        }
        
    }

    public async Task SaveSensorReadingsAsync(List<SensorReading>? sensorReadings, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.SensorReadings.AddRangeAsync(sensorReadings, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}