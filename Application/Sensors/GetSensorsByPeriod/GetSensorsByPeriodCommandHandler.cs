using Domain;
using Infrastructure.Interfaces.Common;
using MediatR;

namespace Application.Sensors.GetSensorsByPeriod;

public class GetSensorsByPeriodCommandHandler : IRequestHandler<GetSensorsByPeriodCommand, IQueryable<Sensor>>
{
    private readonly IApplicationDbContext _context;

    public GetSensorsByPeriodCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Sensor>> Handle(GetSensorsByPeriodCommand request, CancellationToken cancellationToken)
    => Task.FromResult(_context.Sensors
            .Where(sensor => sensor.CreatedTime >= request.StartDate && sensor.CreatedTime <= request.EndDate));
}