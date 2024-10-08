using Domain;
using Infrastructure.Interfaces.Services;
using MediatR;

namespace Application.Sensors.GenerateDataSensors;

public class GenerateDataSensorsCommandHandler : IRequestHandler<GenerateDataSensorsCommand>
{
    private readonly ISensorDataGeneratorService _sensorDataGeneratorService;

    public GenerateDataSensorsCommandHandler(ISensorDataGeneratorService sensorDataGeneratorService)
    {
        _sensorDataGeneratorService = sensorDataGeneratorService;
    }

    public async Task Handle(GenerateDataSensorsCommand request, CancellationToken cancellationToken)
        => await _sensorDataGeneratorService.GenerateSensor(request.Name, request.Description, request.SensorType,
            request.Kind, request.Interval, request.CancellationToken);

}