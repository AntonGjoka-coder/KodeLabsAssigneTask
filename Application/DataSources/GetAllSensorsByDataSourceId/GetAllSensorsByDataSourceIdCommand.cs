using Domain;
using MediatR;

namespace Application.DataSources.GetAllSensorsByDataSourceId;

public record GetAllSensorsByDataSourceIdCommand(Guid Id) : IRequest<List<Sensor>>;