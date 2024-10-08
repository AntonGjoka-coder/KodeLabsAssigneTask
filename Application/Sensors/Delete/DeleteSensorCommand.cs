using MediatR;

namespace Application.Sensors.Delete;

public record DeleteSensorCommand(Guid Id) : IRequest;