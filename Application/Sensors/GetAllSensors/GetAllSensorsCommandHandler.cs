using Domain;
using Infrastructure.Interfaces.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Sensors.GetAllSensors;

public class GetAllSensorsCommandHandler : IRequestHandler<GetAllSensorsCommand, IQueryable<Sensor>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetAllSensorsCommandHandler> _logger;

    public GetAllSensorsCommandHandler(IApplicationDbContext context, ILogger<GetAllSensorsCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<IQueryable<Sensor>> Handle(GetAllSensorsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return Task.FromResult(_context.Sensors.AsQueryable());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}