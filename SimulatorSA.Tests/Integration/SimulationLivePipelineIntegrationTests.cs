using SimulatorSA.Application.Interfaces;
using SimulatorSA.Application.Services;
using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Services;

namespace SimulatorSA.Tests.Integration;

public class SimulationLivePipelineIntegrationTests
{
    [Fact]
    public void EngineStateStoreAndBuffer_ShouldStayConsistent()
    {
        var room = new Room(
            name: "Room A",
            initialTemperature: 18.0,
            heatLossCoefficientKWPerDegree: 0.08,
            thermalCapacityKWhPerDegree: 2.5);

        var controller = new PidController(
            setpoint: 22.0,
            proportionalGain: 8.0,
            integralGain: 0.15,
            derivativeGain: 2.0);

        var actuator = new ThermalActuator("Heating Coil");

        var engine = new SimulationEngine(
            room,
            controller,
            actuator,
            outdoorTemperature: 10.0,
            maxHeatingPowerKW: 5.0,
            deltaTimeMinutes: 1.0);

        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer buffer = new CircularSnapshotBuffer(5);

        for (int i = 0; i < 12; i++)
        {
            var snapshot = engine.Step();
            currentStateStore.Update(snapshot);
            buffer.Add(snapshot);
        }

        var current = currentStateStore.GetCurrent();
        var latestFromBuffer = buffer.GetLatest();
        var allFromBuffer = buffer.GetAll();

        Assert.NotNull(current);
        Assert.NotNull(latestFromBuffer);

        Assert.Equal(11, current!.SequenceNumber);
        Assert.Equal(11, latestFromBuffer!.SequenceNumber);

        Assert.Equal(5, buffer.Count);
        Assert.Equal(new[] { 7, 8, 9, 10, 11 }, allFromBuffer.Select(s => s.SequenceNumber).ToArray());
    }
}