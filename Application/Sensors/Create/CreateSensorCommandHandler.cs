using AutoMapper;
using Domain;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.Sensors.Create;

public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, Guid>
{
    private readonly ISensorRepository _sensorRepository;
    private readonly IMapper _mapper;

    public CreateSensorCommandHandler(ISensorRepository sensorRepository, IMapper mapper)
    {
        _sensorRepository = sensorRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateSensorCommand request, CancellationToken cancellationToken)
    {
        var enitity = _mapper.Map<Sensor>(request);
        var sensorId = await _sensorRepository.Create(enitity);
        return sensorId;    
    }
}