using MediatR;

namespace Application.DataSources.Delete;

public record DeleteDataSourceCommand(Guid Id) : IRequest;