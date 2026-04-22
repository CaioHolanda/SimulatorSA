using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Application.Interfaces;

public interface ICurrentSimulationStateStore
{
    void Update(SimulationSnapshot snapshot);
    SimulationSnapshot? GetCurrent();
    bool HasValue { get; }
}