using Domain;
using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.DataSources.GetById;

public class GetDataSourceByIdCommandHandler : IRequestHandler<GetDataSourceByIdCommand, DataSource>
{
    private readonly IDataSourceRepository _dataSourceRepository;

    public GetDataSourceByIdCommandHandler(IDataSourceRepository dataSourceRepository)
    {
        _dataSourceRepository = dataSourceRepository;
    }

    public async Task<DataSource> Handle(GetDataSourceByIdCommand request, CancellationToken cancellationToken)
    => await _dataSourceRepository.GetById(request.Id);
}