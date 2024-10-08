using AutoMapper;
using Domain;
using MediatR;

namespace Application.Sensors.Create;

public class CreateSensorCommand : IRequest<Guid>
{
    public Guid? DataSource { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SensorType SensorType { get; set; }        
    public Kind Kind { get; set; } 
    
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateSensorCommand, Sensor>();
        }
        
    }
}