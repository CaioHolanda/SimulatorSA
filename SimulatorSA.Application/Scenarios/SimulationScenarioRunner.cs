using SimulatorSA.Application.Interfaces;
using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;

namespace SimulatorSA.Application.Scenarios;

public class SimulationScenarioRunner
{
    private readonly ICurrentSimulationStateStore? _stateStore;
    private readonly ISnapshotBuffer? _snapshotBuffer;

    public SimulationScenarioRunner()
    {
    }

    public SimulationScenarioRunner(
        ICurrentSimulationStateStore stateStore,
        ISnapshotBuffer snapshotBuffer)
    {
        _stateStore = stateStore;
        _snapshotBuffer = snapshotBuffer;
    }

    public SimulationResult RunBaseScenario()
    {
        var scenario = new PidScenarioDefinition();
        return RunPidScenario(scenario);
    }

    public SimulationResult RunPidScenario(PidScenarioDefinition scenario)
    {
        var room = CreateRoom(scenario);

        var controller = new PidController(
            scenario.Setpoint,
            scenario.Kp,
            scenario.Ki,
            scenario.Kd);

        var actuator = new ThermalActuator(scenario.ActuatorName);
        var runner = new SimulationRunner();

        return runner.Run(
            room: room,
            controller: controller,
            actuator: actuator,
            outdoorTemperature: scenario.OutdoorTemperature,
            maxHeatingPowerKW: scenario.MaxHeatingPowerKW,
            totalSteps: scenario.TotalSteps,
            deltaTimeMinutes: scenario.DeltaTimeMinutes);
    }

    public SimulationResult RunOnOffScenario(OnOffScenarioDefinition scenario)
    {
        var room = CreateRoom(scenario);

        var controller = new OnOffController(
            scenario.Setpoint,
            scenario.OutputWhenOn,
            scenario.OutputWhenOff,
            scenario.Hysteresis,
            scenario.MinOnTimeMinutes,
            scenario.MinOffTimeMinutes);

        var actuator = new ThermalActuator(scenario.ActuatorName);
        var runner = new SimulationRunner();

        return runner.Run(
            room: room,
            controller: controller,
            actuator: actuator,
            outdoorTemperature: scenario.OutdoorTemperature,
            maxHeatingPowerKW: scenario.MaxHeatingPowerKW,
            totalSteps: scenario.TotalSteps,
            deltaTimeMinutes: scenario.DeltaTimeMinutes);
    }

    public async Task<SimulationResult> RunPidScenarioLiveAsync(
        PidScenarioDefinition scenario,
        int delayMilliseconds,
        CancellationToken cancellationToken = default)
    {
        EnsureLiveDependencies();

        var room = CreateRoom(scenario);

        var controller = new PidController(
            scenario.Setpoint,
            scenario.Kp,
            scenario.Ki,
            scenario.Kd);

        var actuator = new ThermalActuator(scenario.ActuatorName);

        return await RunLiveAsync(
            room,
            controller,
            actuator,
            scenario.OutdoorTemperature,
            scenario.MaxHeatingPowerKW,
            scenario.TotalSteps,
            scenario.DeltaTimeMinutes,
            delayMilliseconds,
            cancellationToken);
    }

    public async Task<SimulationResult> RunOnOffScenarioLiveAsync(
        OnOffScenarioDefinition scenario,
        int delayMilliseconds,
        CancellationToken cancellationToken = default)
    {
        EnsureLiveDependencies();

        var room = CreateRoom(scenario);

        var controller = new OnOffController(
            scenario.Setpoint,
            scenario.OutputWhenOn,
            scenario.OutputWhenOff,
            scenario.Hysteresis,
            scenario.MinOnTimeMinutes,
            scenario.MinOffTimeMinutes);

        var actuator = new ThermalActuator(scenario.ActuatorName);

        return await RunLiveAsync(
            room,
            controller,
            actuator,
            scenario.OutdoorTemperature,
            scenario.MaxHeatingPowerKW,
            scenario.TotalSteps,
            scenario.DeltaTimeMinutes,
            delayMilliseconds,
            cancellationToken);
    }

    private async Task<SimulationResult> RunLiveAsync(
        Room room,
        IController controller,
        ThermalActuator actuator,
        double outdoorTemperature,
        double maxHeatingPowerKW,
        int totalSteps,
        double deltaTimeMinutes,
        int delayMilliseconds,
        CancellationToken cancellationToken)
    {
        var runner = new SimulationRunner();
        var result = new SimulationResult();

        for (int step = 0; step < totalSteps; step++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var stepResult = runner.Run(
                room: room,
                controller: controller,
                actuator: actuator,
                outdoorTemperature: outdoorTemperature,
                maxHeatingPowerKW: maxHeatingPowerKW,
                totalSteps: 1,
                deltaTimeMinutes: deltaTimeMinutes);

            var snapshot = stepResult.Snapshots.Last();

            result.Snapshots.Add(snapshot);

            _stateStore!.Update(snapshot);
            _snapshotBuffer!.Add(snapshot);

            await Task.Delay(delayMilliseconds, cancellationToken);
        }

        return result;
    }

    private static Room CreateRoom(BaseScenarioDefinition scenario)
    {
        return new Room(
            scenario.RoomName,
            scenario.InitialTemperature,
            scenario.HeatLossCoefficientKWPerDegree,
            scenario.ThermalCapacityKWhPerDegree);
    }

    private void EnsureLiveDependencies()
    {
        if (_stateStore is null || _snapshotBuffer is null)
            throw new InvalidOperationException(
                "Live simulation requires CurrentSimulationStateStore and SnapshotBuffer.");
    }
}