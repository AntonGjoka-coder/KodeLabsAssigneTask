using AutoMapper;
using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.Sensors.Update;

public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand, string>
{
 private readonly ISensorRepository _sensorRepository;
 private readonly IMapper _mapper;

 public UpdateSensorCommandHandler(ISensorRepository sensorRepository, IMapper mapper)
 {
  _sensorRepository = sensorRepository;
  _mapper = mapper;
 }

 public async Task<string> Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
 {
  var entity = await _sensorRepository.GetById(request.Id);
  if (entity != null)
  {
   _mapper.Map(request, entity);
   await _sensorRepository.Update(entity);
   return "Update completed";
  }
  return "Sensor not found";
 }
}