/* Mudanca para a arquitetura
    separar:
        tempo real da máquina
        tempo simulado do ambiente
*/

namespace SimulatorSA.Application.Services;

using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

public class SupervisoryStateService : ISupervisoryStateService
{
    private readonly ISimulationStateProvider _simulationStateProvider;

    public SupervisoryStateService(ISimulationStateProvider simulationStateProvider)
    {
        _simulationStateProvider = simulationStateProvider;
    }

    public SupervisoryStateDto GetCurrentState()
    {
        var state = _simulationStateProvider.GetCurrentState();

        return new SupervisoryStateDto
        {
            Timestamp = DateTime.Now,
            RoomName = state.RoomName,
            IndoorTemperature = state.IndoorTemperature,
            OutdoorTemperature = state.OutdoorTemperature,
            Setpoint = state.Setpoint,
            HeaterOutput = state.HeaterOutput,
            HeaterEnabled = state.HeaterEnabled,
            ControllerType = state.ControllerType,
            CurrentStep = state.CurrentStep,
            SimulatedMinutes = state.SimulatedMinutes
        };
    }
}