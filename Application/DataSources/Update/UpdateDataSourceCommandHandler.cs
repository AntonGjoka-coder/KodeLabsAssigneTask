using AutoMapper;
using Infrastructure.Interfaces.Repository;
using MediatR;

namespace Application.DataSources.Update;

public class UpdateDataSourceCommandHandler : IRequestHandler<UpdateDataSourceCommand, string>
{
    private readonly IDataSourceRepository _dataSourceRepository;
    private readonly IMapper _mapper;

    public UpdateDataSourceCommandHandler(IDataSourceRepository dataSourceRepository, IMapper mapper)
    {
        _dataSourceRepository = dataSourceRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateDataSourceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dataSourceRepository.GetById(request.Id);
            if (entity != null)
            {
                _mapper.Map(request, entity);
                await _dataSourceRepository.Update(entity);    
                return "Data updated";
            }
            return "Data not found";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}