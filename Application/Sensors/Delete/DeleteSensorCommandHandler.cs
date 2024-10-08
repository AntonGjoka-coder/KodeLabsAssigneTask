using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.Sensors.Delete;

public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand>
{
    private readonly ISensorRepository _repository;

    public DeleteSensorCommandHandler(ISensorRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
        => await _repository.Delete(request.Id);
}