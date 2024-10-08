using System.Runtime.InteropServices.JavaScript;
using Domain;
using MediatR;

namespace Application.Sensors.GetSensorsByPeriod;

public record GetSensorsByPeriodCommand(DateTime StartDate, DateTime EndDate) : IRequest<IQueryable<Sensor>>;