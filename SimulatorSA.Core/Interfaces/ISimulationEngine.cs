/*
manter o estado atual da simulação;
avançar um passo;
devolver um SimulationSnapshot.
 */
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Core.Interfaces;

public interface ISimulationEngine
{
    int CurrentStep { get; }
    double SimulatedMinutes { get; }

    SimulationSnapshot Step();

    //void Reset();
}