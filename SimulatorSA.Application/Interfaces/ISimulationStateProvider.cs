using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Application.Interfaces;

/*
Essa interface será usada pelo SimulatorSA.Bacnet para perguntar:

    qual a temperatura atual?
    qual o setpoint?
    qual a saída do controlador?
    qual o estado atual da simulação?

Ela é uma porta de leitura do sistema.
*/

public interface ISimulationStateProvider
{
    SimulationSnapshot GetCurrentSnapshot();
}