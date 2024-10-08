using Domain;
using Infrastructure.Interfaces.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class SensorRepository : ISensorRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SensorRepository> _logger;

    public SensorRepository(ApplicationDbContext context, ILogger<SensorRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Create(Sensor product)
    {
        try
        {
            var exists = _context.Sensors.Any(x=> x.Name == product.Name);
            if (exists)
            {
                throw new Exception("DataSource already exists");
            }
            var entityEntry = await _context.Sensors.AddAsync(product);
            await _context.SaveChangesAsync();
            return entityEntry.Entity.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task Update(Sensor product)
    {
        try
        {
            var dataSource = await _context.Sensors.FirstOrDefaultAsync(x=> x.Id == product.Id);
            if (dataSource != null)
            {
                _context.Sensors.Update(product);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task Delete(Guid Id)
    {
        try
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(x=> x.Id == Id);

            if (sensor != null)
            {
                _context.Sensors.Remove(sensor);
                return;
            }
            throw new Exception("DataSource not found");
        }
        catch (Exception e)
        {
           _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task<Sensor> GetById(Guid Id)
    {
        try
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(x=> x.Id == Id);
            return sensor;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task<List<Sensor>> GetAll()
    {
        try
        {
            var sensors = await _context.Sensors.ToListAsync();
            return sensors;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}