using AutoMapper;
using Domain;
using MediatR;

namespace Application.DataSources.Update;

public class UpdateDataSourceCommand : IRequest<string>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }             
    public string? Description { get; set; }    
    
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UpdateDataSourceCommand, DataSource>();
        }
    }
}