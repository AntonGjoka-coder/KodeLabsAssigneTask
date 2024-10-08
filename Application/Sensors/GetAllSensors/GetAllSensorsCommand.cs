using Domain;
using MediatR;

namespace Application.Sensors.GetAllSensors;

public class GetAllSensorsCommand : IRequest<IQueryable<Sensor>>
{
}