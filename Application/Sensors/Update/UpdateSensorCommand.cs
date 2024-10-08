using AutoMapper;
using Domain;
using MediatR;

namespace Application.Sensors.Update;

public class UpdateSensorCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public Guid? DataSource { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public SensorType? SensorType { get; set; }        
    public Kind? Kind { get; set; } 
    
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UpdateSensorCommand, Sensor>();
        }
    }
}