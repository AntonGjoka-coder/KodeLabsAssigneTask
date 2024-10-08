using System.Collections.Concurrent;
using Application.Sensors.Create;
using Application.Sensors.Delete;
using Application.Sensors.GenerateDataSensors;
using Application.Sensors.GetById;
using Application.Sensors.Update;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KodeLabsAssigneTask.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorController : ControllerBase
{
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> _cancellationTokenSources = new();
    private readonly IMediator _mediator;

    public SensorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<Sensor> GetById(Guid id)
        => await _mediator.Send(new GetSensorByIdCommand(id));

    [HttpPost]
    public async Task<Guid> Create(CreateSensorCommand command)
     => await _mediator.Send(command);
    
    [HttpPut]
    public async Task<string> Update(UpdateSensorCommand command)
    => await _mediator.Send(command);

    [HttpDelete]
    public async Task Delete(Guid id)
        => await _mediator.Send(new DeleteSensorCommand(id));

    [HttpPost("GenerateDataSensors")]
    public async Task<ActionResult<Unit>> GenerateDataSensors(GenerateDataSensorsCommand request)
    {
        if (_cancellationTokenSources.ContainsKey(request.Name))
        {
            return Conflict("A data generation process is already running for this sensor.");
        }

        var cancellationTokenSource = new CancellationTokenSource();
        _cancellationTokenSources[request.Name] = cancellationTokenSource;

        _ = Task.Run(async () =>
        {
            try
            {
                await _mediator.Send(new GenerateDataSensorsCommand
                {
                    Name = request.Name,
                    Description = request.Description,
                    SensorType = request.SensorType,
                    Kind = request.Kind,
                    Interval = request.Interval,
                    CancellationToken = cancellationTokenSource.Token
                });
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                _cancellationTokenSources.TryRemove(request.Name, out _);
            }
        });

        return Unit.Value;
    }

    
    [HttpPost("StopDataSensors")]
    public ActionResult<Unit> StopDataSensors(string name)
    {
        if (_cancellationTokenSources.TryRemove(name, out var cancellationTokenSource))
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            return Unit.Value; 
        }

        return NotFound("Sensor not found.");
    }
}