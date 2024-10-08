namespace Domain;
using System.Timers;

public class Sensor
{
    public Guid Id { get; set; }
    public Guid? DataSource { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SensorType SensorType { get; set; }        
    public Kind Kind { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}

public enum SensorType
{
    Temperature,
    Humidity,
    ConnectivityStatus,
}

public enum Kind
{
    String,
    Number,
    Boolean
}