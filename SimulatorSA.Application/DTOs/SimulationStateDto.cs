namespace SimulatorSA.Application.DTOs;

public class SimulationStateDto
{
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