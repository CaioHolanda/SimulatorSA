using System.Collections.Generic;

namespace SimulatorSA.Core.Models.SimulationData;

public class SimulationSnapshot
{
    public double Time { get; set; }

    public double RoomTemperature { get; set; }
    public double Setpoint { get; set; }
    public double ControllerOutput { get; set; }
    public double ControlError { get; set; }
    public double HeatingPowerKW { get; set; }

    public Dictionary<string, double> Values { get; set; } = new();
}
