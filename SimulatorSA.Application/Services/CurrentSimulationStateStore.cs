using SimulatorSA.Application.Interfaces;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Application.Services;

public class CurrentSimulationStateStore : ICurrentSimulationStateStore
{
    private SimulationSnapshot? _currentSnapshot;

    public bool HasValue => _currentSnapshot is not null;

    public void Update(SimulationSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        _currentSnapshot = snapshot;
    }

    public SimulationSnapshot? GetCurrent()
    {
        return _currentSnapshot;
    }
}