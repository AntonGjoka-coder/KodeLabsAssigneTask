namespace Domain;

public class SensorReading
{
    public Guid Id { get; set; }
    public Guid SensorId { get; set; }  
    public DateTime Timestamp { get; set; }
    public double Value { get; set; }
}