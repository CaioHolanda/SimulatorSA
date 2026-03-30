using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Models.SimulationData;

public class SimulationSnapshot
{
    public double Time { get; set; }
    public Dictionary<string, double> Values { get; set; } = new();
}