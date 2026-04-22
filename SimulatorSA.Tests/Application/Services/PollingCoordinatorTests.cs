using SimulatorSA.Application.Interfaces;
using SimulatorSA.Application.Services;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Tests.Application.Services;

public class PollingCoordinatorTests
{
    [Fact]
    public void TryCollect_WhenCurrentStateIsMissing_ShouldReturnNoSample()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);

        var coordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        var result = coordinator.TryCollect();

        Assert.False(result.HasSample);
        Assert.False(result.RecoveredFromBuffer);
        Assert.False(result.GapDetected);
        Assert.Equal("No current state available.", result.Status);
        Assert.Null(result.Sample);
        Assert.Null(coordinator.LastCollectedSequenceNumber);
    }

    [Fact]
    public void TryCollect_WhenCurrentMatchesExpected_ShouldCollectFromCurrentState()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);

        currentStateStore.Update(CreateSnapshot(sequenceNumber: 0, simulatedMinutes: 0));

        var coordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        var result = coordinator.TryCollect();

        Assert.True(result.HasSample);
        Assert.False(result.RecoveredFromBuffer);
        Assert.False(result.GapDetected);
        Assert.Equal(0, result.ExpectedSequenceNumber);
        Assert.Equal(0, result.ActualSequenceNumber);
        Assert.Equal("Collected from current state.", result.Status);

        Assert.NotNull(result.Sample);
        Assert.Equal(0, result.Sample!.SequenceNumber);
        Assert.Equal(0, coordinator.LastCollectedSequenceNumber);
    }

    [Fact]
    public void TryCollect_WhenCurrentPassedExpectedAndBufferContainsExpected_ShouldRecoverFromBuffer()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);

        var coordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        // First successful collection at seq 0
        var snapshot0 = CreateSnapshot(sequenceNumber: 0, simulatedMinutes: 0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var firstResult = coordinator.TryCollect();
        Assert.True(firstResult.HasSample);
        Assert.Equal(0, coordinator.LastCollectedSequenceNumber);

        // Current has advanced to 7, expected is 5, buffer still has 5
        for (int seq = 1; seq <= 7; seq++)
        {
            var snapshot = CreateSnapshot(sequenceNumber: seq, simulatedMinutes: seq);
            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);
        }

        var result = coordinator.TryCollect();

        Assert.True(result.HasSample);
        Assert.True(result.RecoveredFromBuffer);
        Assert.False(result.GapDetected);
        Assert.Equal(5, result.ExpectedSequenceNumber);
        Assert.Equal(5, result.ActualSequenceNumber);
        Assert.Equal("Recovered expected sample from buffer.", result.Status);

        Assert.NotNull(result.Sample);
        Assert.Equal(5, result.Sample!.SequenceNumber);
        Assert.Equal(5, coordinator.LastCollectedSequenceNumber);
    }

    [Fact]
    public void TryCollect_WhenCurrentPassedExpectedAndBufferDoesNotContainExpected_ShouldDetectGap()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(3);

        var coordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        // First successful collection at seq 0
        var snapshot0 = CreateSnapshot(sequenceNumber: 0, simulatedMinutes: 0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var firstResult = coordinator.TryCollect();
        Assert.True(firstResult.HasSample);
        Assert.Equal(0, coordinator.LastCollectedSequenceNumber);

        // Advance current to 9, but buffer capacity 3 means seq 5 is not retained
        for (int seq = 7; seq <= 9; seq++)
        {
            var snapshot = CreateSnapshot(sequenceNumber: seq, simulatedMinutes: seq);
            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);
        }

        var result = coordinator.TryCollect();

        Assert.False(result.HasSample);
        Assert.False(result.RecoveredFromBuffer);
        Assert.True(result.GapDetected);
        Assert.Equal(5, result.ExpectedSequenceNumber);
        Assert.Equal(9, result.ActualSequenceNumber);
        Assert.Equal("Expected sample not found in current state or buffer.", result.Status);
        Assert.Null(result.Sample);

        Assert.Equal(0, coordinator.LastCollectedSequenceNumber);
    }

    [Fact]
    public void TryCollect_WhenExpectedSequenceHasNotBeenReachedYet_ShouldReturnNoSample()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);

        var coordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        // First successful collection at seq 0
        var snapshot0 = CreateSnapshot(sequenceNumber: 0, simulatedMinutes: 0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var firstResult = coordinator.TryCollect();
        Assert.True(firstResult.HasSample);
        Assert.Equal(0, coordinator.LastCollectedSequenceNumber);

        // Current is only at seq 3, expected is 5
        var snapshot3 = CreateSnapshot(sequenceNumber: 3, simulatedMinutes: 3);
        currentStateStore.Update(snapshot3);
        snapshotBuffer.Add(snapshot3);

        var result = coordinator.TryCollect();

        Assert.False(result.HasSample);
        Assert.False(result.RecoveredFromBuffer);
        Assert.False(result.GapDetected);
        Assert.Equal(5, result.ExpectedSequenceNumber);
        Assert.Equal(3, result.ActualSequenceNumber);
        Assert.Equal("Expected sequence not reached yet.", result.Status);
        Assert.Null(result.Sample);

        Assert.Equal(0, coordinator.LastCollectedSequenceNumber);
    }

    private static SimulationSnapshot CreateSnapshot(int sequenceNumber, double simulatedMinutes)
    {
        return new SimulationSnapshot
        {
            SequenceNumber = sequenceNumber,
            SimulatedMinutes = simulatedMinutes,
            ProducedAtUtc = DateTime.UtcNow,
            RoomTemperature = 18.0 + sequenceNumber * 0.1,
            Setpoint = 22.0,
            ControllerOutput = 50.0,
            ControlError = 22.0 - (18.0 + sequenceNumber * 0.1),
            HeatingPowerKW = 2.0
        };
    }
}