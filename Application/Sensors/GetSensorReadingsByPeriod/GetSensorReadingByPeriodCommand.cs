using Domain;
using MediatR;

namespace Application.Sensors.GetSensorReadingsByPeriod;

public record GetSensorReadingByPeriodCommand(DateTime StartDate, DateTime EndDate) : IRequest<IQueryable<SensorReading>>;