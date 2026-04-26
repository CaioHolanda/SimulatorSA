using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Spaces;

namespace SimulatorSA.Core.Services;

public class SimulationEngine : ISimulationEngine
{
    private readonly Room _room;
    private readonly IController _controller;
    private readonly ThermalActuator _actuator;
    private readonly double _outdoorTemperature;
    private readonly double _maxHeatingPowerKW;
    private readonly double _deltaTimeMinutes;
    private readonly Action<Room, int, double>? _stepAction;

    public int CurrentStep { get; private set; }
    public double SimulatedMinutes => CurrentStep * _deltaTimeMinutes;

    public SimulationEngine(
        Room room,
        IController controller,
        ThermalActuator actuator,
        double outdoorTemperature,
        double maxHeatingPowerKW,
        double deltaTimeMinutes,
        Action<Room, int, double>? stepAction = null)
    {
        ArgumentNullException.ThrowIfNull(room);
        ArgumentNullException.ThrowIfNull(controller);
        ArgumentNullException.ThrowIfNull(actuator);

        if (maxHeatingPowerKW < 0)
            throw new ArgumentOutOfRangeException(nameof(maxHeatingPowerKW), "Maximum heating power cannot be negative.");

        if (deltaTimeMinutes <= 0)
            throw new ArgumentOutOfRangeException(nameof(deltaTimeMinutes), "Delta time must be greater than zero.");

        _room = room;
        _controller = controller;
        _actuator = actuator;
        _outdoorTemperature = outdoorTemperature;
        _maxHeatingPowerKW = maxHeatingPowerKW;
        _deltaTimeMinutes = deltaTimeMinutes;
        _stepAction = stepAction;

        Reset();
    }

    public SimulationSnapshot Step()
    {
        double simulatedMinutes = CurrentStep * _deltaTimeMinutes;

        _stepAction?.Invoke(_room, CurrentStep, simulatedMinutes);

        double controllerOutput = _controller.CalculateOutput(
            _room.ActualTemperature,
            _deltaTimeMinutes);

        _actuator.SetOutput(controllerOutput);

        double heatingPowerKW = _actuator.CalculateHeatingPowerKW(
            _maxHeatingPowerKW,
            _deltaTimeMinutes);

        _room.ApplyThermalPower(heatingPowerKW, _deltaTimeMinutes);

        if (_room is OfficeA office)
        {
            office.ApplyInternalPerturbations(_deltaTimeMinutes);
        }

        _room.ApplyEnvelopeHeatExchange(_outdoorTemperature, _deltaTimeMinutes);

        double error = _controller.Setpoint - _room.ActualTemperature;

        var snapshot = new SimulationSnapshot
        {
            SequenceNumber = CurrentStep,
            SimulatedMinutes = simulatedMinutes,
            ProducedAtUtc = DateTime.UtcNow,
            RoomTemperature = _room.ActualTemperature,
            Setpoint = _controller.Setpoint,
            ControllerOutput = _actuator.OutputPercentage,
            ControlError = error,
            HeatingPowerKW = heatingPowerKW
        };

        snapshot.Values[SimulationVariables.Temperature] = _room.ActualTemperature;
        snapshot.Values[SimulationVariables.Error] = error;
        snapshot.Values[SimulationVariables.ValveOpening] = _actuator.OutputPercentage;
        snapshot.Values[SimulationVariables.HeatingEffect] = heatingPowerKW;
        snapshot.Values[SimulationVariables.Setpoint] = _controller.Setpoint;

        CurrentStep++;

        return snapshot;
    }

    public void Reset()
    {
        _controller.Reset();
        _actuator.Reset();
        CurrentStep = 0;
    }
}