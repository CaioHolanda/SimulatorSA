using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Models.SimulationData;

public class TimeSeries
{
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public List<TimeValuePoint> Points { get; set; } = new();
}