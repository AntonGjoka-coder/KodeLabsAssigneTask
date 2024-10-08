using Domain;
using Infrastructure.Interfaces.Common;
using MediatR;

namespace Application.Sensors.GetSensorReadingsByPeriod;

public class GetSensorReadingByPeriodCommandHandler : IRequestHandler<GetSensorReadingByPeriodCommand, IQueryable<SensorReading>>
{
    private readonly IApplicationDbContext _context;

    public GetSensorReadingByPeriodCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<SensorReading>> Handle(GetSensorReadingByPeriodCommand request, CancellationToken cancellationToken)
    => Task.FromResult(_context.SensorReadings
    .Where(sensor => sensor.Timestamp >= request.StartDate && sensor.Timestamp <= request.EndDate));
}