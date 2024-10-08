using Domain;
using MediatR;

namespace Application.Sensors.GenerateDataSensors;

public class GenerateDataSensorsCommand : IRequest
{
    public Guid? DataSource { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SensorType SensorType { get; set; }        
    public Kind Kind { get; set; }
    public int Interval { get; set; }
    public CancellationToken CancellationToken { get; set; }
}