using Application.DataSources.Create;
using Application.DataSources.Delete;
using Application.DataSources.GetAllSensorsByDataSourceId;
using Application.DataSources.GetById;
using Application.DataSources.Update;
using Application.Sensors.Create;
using Application.Sensors.Delete;
using Application.Sensors.GetById;
using Application.Sensors.Update;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KodeLabsAssigneTask.Controllers;

[ApiController]
[Route("[controller]")]
public class DataSourceController :ControllerBase
{
    private readonly IMediator _mediator;

    public DataSourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<DataSource> GetById(Guid id)
        => await _mediator.Send(new GetDataSourceByIdCommand(id));

    [HttpPost]
    public async Task<Guid> Create(CreateDataSourceCommand command)
        => await _mediator.Send(command);
    
    [HttpPut]
    public async Task<string> Update(UpdateDataSourceCommand command)
        => await _mediator.Send(command);

    [HttpDelete]
    public async Task Delete(Guid id)
        => await _mediator.Send(new DeleteDataSourceCommand(id));
    
    [HttpGet("GetSensorsByDataSourceId/{id:guid}")]
    public async Task<List<Sensor>> GetSensorsByDataSourceId(Guid id)
        => await _mediator.Send(new GetAllSensorsByDataSourceIdCommand(id));
}