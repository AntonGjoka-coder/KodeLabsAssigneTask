using Domain;
using MediatR;

namespace Application.DataSources.GetById;

public record GetDataSourceByIdCommand(Guid Id) : IRequest<DataSource>;