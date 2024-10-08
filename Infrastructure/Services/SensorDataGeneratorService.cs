using Domain;
using Infrastructure.Interfaces.Common;
using Infrastructure.Interfaces.Services;
using Hangfire; // Add this using directive
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces.Repository;

namespace Infrastructure.Services
{
    public class SensorDataGeneratorService : ISensorDataGeneratorService
    {
        private static readonly Random _random = new Random();
        private readonly IApplicationDbContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public SensorDataGeneratorService(IApplicationDbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            _dbContext = dbContext;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task GenerateSensor(string name, string description, SensorType sensorType, Kind kind, int interval, CancellationToken cancellationToken)
        {
            var sensorReadings = new List<SensorReading>();
            if (interval <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be greater than zero.");
            }

            var sensor = new Sensor
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                SensorType = sensorType,
                Kind = kind
            };

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(interval, cancellationToken);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                HandleTimedEvent(sensorReadings, sensor);
            }
            
            if (sensorReadings.Count > 0)
            {
                _backgroundJobClient.Enqueue<IDataCollectionRepository>(service => 
                    service.SaveSensorReadingsAsync(sensorReadings, cancellationToken));
            }
        }

        private void HandleTimedEvent(List<SensorReading> sensorReadings, Sensor sensor)
        {
            var reading = GenerateRandomReading(sensor);
            var sensorReading = new SensorReading
            {
                SensorId = sensor.Id,
                Timestamp = DateTime.UtcNow,
                Value = Convert.ToDouble(reading)
            };
            sensorReadings.Add(sensorReading);
        }

        private object GenerateRandomReading(Sensor sensor)
        {
            return sensor.Kind switch
            {
                Kind.String => GenerateRandomString(),
                Kind.Number => GenerateRandomNumber(sensor.SensorType),
                Kind.Boolean => _random.Next(2) == 0,
                _ => null
            };
        }

        private string GenerateRandomString()
        {
            string[] values = { "OK", "WARNING", "ERROR" };
            return values[_random.Next(values.Length)];
        }

        private double GenerateRandomNumber(SensorType sensorType)
        {
            return sensorType switch
            {
                SensorType.Temperature => _random.Next(-10, 41) + _random.NextDouble(),
                SensorType.Humidity => _random.Next(0, 101),
                SensorType.ConnectivityStatus => _random.Next(0, 2) * 1.0,
                _ => 0
            };
        }
    }
}
