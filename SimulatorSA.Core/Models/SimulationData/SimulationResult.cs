using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Models.SimulationData;

public class SimulationResult
{
    public List<SimulationSnapshot> Snapshots { get; set; } = new();
    public List<TimeSeries> Series { get; set; } = new();
}