// E' o retrato instantaneo que a HMI precisa.

namespace SimulatorSA.Application.DTOs;

public class SupervisoryStateDto
{
    public DateTime Timestamp { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public double IndoorTemperature { get; set; }
    public double OutdoorTemperature { get; set; }
    public double Setpoint { get; set; }

    public double HeaterOutput { get; set; }
    public bool HeaterEnabled { get; set; }

    public string ControllerType { get; set; } = string.Empty;

    public int CurrentStep { get; set; }
    public double SimulatedMinutes { get; set; }
}