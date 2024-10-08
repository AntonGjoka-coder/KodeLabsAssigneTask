using Domain;
using Infrastructure.Interfaces.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.DataSources.GetAllSensorsByDataSourceId;

public class GetAllSensorsByDataSourceIdCommandHandler : IRequestHandler<GetAllSensorsByDataSourceIdCommand, List<Sensor>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetAllSensorsByDataSourceIdCommandHandler> _logger;

    public GetAllSensorsByDataSourceIdCommandHandler(IApplicationDbContext context, ILogger<GetAllSensorsByDataSourceIdCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Sensor>> Handle(GetAllSensorsByDataSourceIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Sensors.Where(x => x.DataSource == request.Id).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}