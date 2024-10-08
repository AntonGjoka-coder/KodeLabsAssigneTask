using Domain;
using MediatR;

namespace Application.Sensors.GetById;

public record GetSensorByIdCommand(Guid Id) : IRequest<Sensor>;