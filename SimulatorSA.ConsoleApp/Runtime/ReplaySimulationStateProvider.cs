using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.ConsoleApp.Runtime;

public class ReplaySimulationStateProvider : ISimulationStateProvider
{
    private SimulationStateDto _currentState = new();

    public SimulationStateDto GetCurrentState()
    {
        return _currentState;
    }

    public void UpdateFromSnapshot(
        SimulationSnapshot snapshot,
        string roomName,
        double outdoorTemperature,
        string controllerType)
    {
        _currentState = new SimulationStateDto
        {
            RoomName = roomName,
            IndoorTemperature = snapshot.RoomTemperature,
            OutdoorTemperature = outdoorTemperature,
            Setpoint = snapshot.Setpoint,
            HeaterOutput = snapshot.HeatingPowerKW,
            HeaterEnabled = snapshot.HeatingPowerKW > 0,
            ControllerType = controllerType,
            CurrentStep = (int)(snapshot.Time),
            SimulatedMinutes = snapshot.Time
        };
    }
}