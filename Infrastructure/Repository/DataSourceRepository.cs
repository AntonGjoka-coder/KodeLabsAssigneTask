using Domain;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class DataSourceRepository : IDataSourceRepository
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DataSourceRepository> _logger;
    public DataSourceRepository(IApplicationDbContext context, ILogger<DataSourceRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Guid> Create(DataSource product)
    {
        try
        {
            var exists = _context.DataSources.Any(x=> x.Name == product.Name);
            if (exists)
            {
                throw new Exception("DataSource already exists");
            }
            var entityEntry = await _context.DataSources.AddAsync(product);
            await _context.SaveChangesAsync();
            return entityEntry.Entity.Id;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task Update(DataSource product)
    {
        try
        {
            var dataSource = await _context.DataSources.FirstOrDefaultAsync(x=> x.Id == product.Id);
            if (dataSource != null)
            {
                _context.DataSources.Update(product);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
           _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task Delete(Guid Id)
    {
        try
        {
            var dataSource = await _context.DataSources.FirstOrDefaultAsync(x=> x.Id == Id);

            if (dataSource != null)
            {
                _context.DataSources.Remove(dataSource);
                return;
            }
            throw new Exception("DataSource not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<DataSource> GetById(Guid Id)
    {
        try
        {
            var dataSource = await _context.DataSources.FirstOrDefaultAsync(x=> x.Id == Id);
            return dataSource;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<List<DataSource>> GetAll()
    {
        try
        {
            var dataSources = await _context.DataSources.ToListAsync();
            return dataSources;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}