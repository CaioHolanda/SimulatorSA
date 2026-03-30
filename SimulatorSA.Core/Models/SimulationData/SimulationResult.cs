namespace SimulatorSA.Core.Models.SimulationData;

public class SimulationResult
{
    public List<SimulationSnapshot> Snapshots { get; set; } = new();
    public List<TimeSeries> Series { get; set; } = new();
    public TimeSeries? GetSeries(string name)
    {
        return Series.FirstOrDefault(s => s.Name == name);
    }

}