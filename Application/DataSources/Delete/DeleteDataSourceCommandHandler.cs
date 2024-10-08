using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.DataSources.Delete;

public class DeleteDataSourceCommandHandler : IRequestHandler<DeleteDataSourceCommand>
{
    private readonly IDataSourceRepository _dataSourceRepository;

    public DeleteDataSourceCommandHandler(IDataSourceRepository dataSourceRepository)
    {
        _dataSourceRepository = dataSourceRepository;
    }

    public async Task Handle(DeleteDataSourceCommand request, CancellationToken cancellationToken)
        => await _dataSourceRepository.Delete(request.Id);
}