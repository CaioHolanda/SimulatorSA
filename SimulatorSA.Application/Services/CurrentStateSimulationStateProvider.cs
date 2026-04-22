using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;

namespace SimulatorSA.Application.Services;

public class CurrentStateSimulationStateProvider : ISimulationStateProvider
{
    private readonly ICurrentSimulationStateStore _currentStateStore;
    private readonly string _roomName;
    private readonly string _controllerType;
    private readonly Func<double> _outdoorTemperatureProvider;

    public CurrentStateSimulationStateProvider(
        ICurrentSimulationStateStore currentStateStore,
        string roomName,
        string controllerType,
        Func<double> outdoorTemperatureProvider)
    {
        ArgumentNullException.ThrowIfNull(currentStateStore);
        ArgumentNullException.ThrowIfNull(outdoorTemperatureProvider);

        _currentStateStore = currentStateStore;
        _roomName = roomName;
        _controllerType = controllerType;
        _outdoorTemperatureProvider = outdoorTemperatureProvider;
    }

    public SimulationStateDto GetCurrentState()
    {
        var snapshot = _currentStateStore.GetCurrent();

        if (snapshot is null)
        {
            return new SimulationStateDto
            {
                RoomName = _roomName,
                OutdoorTemperature = _outdoorTemperatureProvider(),
                ControllerType = _controllerType
            };
        }

        return new SimulationStateDto
        {
            RoomName = _roomName,
            IndoorTemperature = snapshot.RoomTemperature,
            OutdoorTemperature = _outdoorTemperatureProvider(),
            Setpoint = snapshot.Setpoint,
            HeaterOutput = snapshot.HeatingPowerKW,
            HeaterEnabled = snapshot.HeatingPowerKW > 0,
            ControllerType = _controllerType,
            CurrentStep = snapshot.SequenceNumber,
            SimulatedMinutes = snapshot.SimulatedMinutes
        };
    }
}