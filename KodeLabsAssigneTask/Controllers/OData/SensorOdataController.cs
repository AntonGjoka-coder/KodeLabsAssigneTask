using Application.Sensors.GetAllSensors;
using Application.Sensors.GetSensorReadingsByPeriod;
using Application.Sensors.GetSensorsByPeriod;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace KodeLabsAssigneTask.Controllers.OData;

public class SensorOdataController : ODataBaseController
{
    private readonly IMediator _mediator;

    public SensorOdataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [EnableQuery]
    [HttpPost("SensorOdata")]
    public async Task<IQueryable<Sensor>> GetSensors()
        => await _mediator.Send(new GetAllSensorsCommand());

    [EnableQuery]
    [HttpPost("GetSensorsByPeriod")]
    public async Task<IQueryable<Sensor>> GetSensorsByOeriod(GetSensorsByPeriodCommand command)
        => await _mediator.Send(command);
    
    [EnableQuery]
    [HttpPost("GetSensorReadingByPeriod")]
    public async Task<IQueryable<SensorReading>> GetSensorReadingByPeriod(GetSensorReadingByPeriodCommand command)
        => await _mediator.Send(command);
}