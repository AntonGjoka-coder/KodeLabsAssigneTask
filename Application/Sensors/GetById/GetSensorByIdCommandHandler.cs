using Domain;
using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.Sensors.GetById;

public class GetSensorByIdCommandHandler : IRequestHandler<GetSensorByIdCommand, Sensor>
{
    private readonly ISensorRepository _repository;

    public GetSensorByIdCommandHandler(ISensorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Sensor> Handle(GetSensorByIdCommand request, CancellationToken cancellationToken)
    => await _repository.GetById(request.Id);
}