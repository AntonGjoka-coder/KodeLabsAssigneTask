using AutoMapper;
using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.DataSources.Create;

public class CreateDataSourceCommandHandler : IRequestHandler<CreateDataSourceCommand, Guid>
{
    private readonly IDataSourceRepository _dataSourceRepository;
    private readonly IMapper _mapper;

    public CreateDataSourceCommandHandler(IDataSourceRepository dataSourceRepository, IMapper mapper)
    {
        _dataSourceRepository = dataSourceRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateDataSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.DataSource>(request);
        return await _dataSourceRepository.Create(entity);
    }
}