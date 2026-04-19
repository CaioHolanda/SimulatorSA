// E' o ponto armazenado no historico da HMI
namespace SimulatorSA.Application.DTOs;

public class TrendSampleDto
{
    public DateTime Timestamp { get; set; }

    public double IndoorTemperature { get; set; }
    public double OutdoorTemperature { get; set; }
    public double Setpoint { get; set; }
    public double HeaterOutput { get; set; }
    public bool HeaterEnabled { get; set; }
}