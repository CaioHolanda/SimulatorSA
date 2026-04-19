/*Responsável por:
    receber ou criar um cenário;
    montar os objetos reais do domínio;
    executar a simulação;
    devolver um SimulationResult.
Não imprime nada em console. */
using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;

namespace SimulatorSA.Application.Scenarios;

public class SimulationScenarioRunner
{
    public SimulationResult RunBaseScenario()
    {
        var scenario = new PidScenarioDefinition();
        return RunPidScenario(scenario);
    }

    public SimulationResult RunPidScenario(PidScenarioDefinition scenario)
    {
        var room = new Room(
            scenario.RoomName,
            scenario.InitialTemperature,
            scenario.HeatLossCoefficientKWPerDegree,
            scenario.ThermalCapacityKWhPerDegree);

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
        var room = new Room(
            scenario.RoomName,
            scenario.InitialTemperature,
            scenario.HeatLossCoefficientKWPerDegree,
            scenario.ThermalCapacityKWhPerDegree);

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
}