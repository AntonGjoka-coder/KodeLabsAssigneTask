using AutoMapper;
using MediatR;

namespace Application.DataSources.Create;

public class CreateDataSourceCommand : IRequest<Guid>
{
    public string Name { get; set; }             
    public string? Description { get; set; }    
    
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateDataSourceCommand, Domain.DataSource>();
        }
    }
}