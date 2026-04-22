using SimulatorSA.Application.Interfaces;
using SimulatorSA.Application.Services;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Tests.Application.Services;

public class TrendCollectionServiceTests
{
    [Fact]
    public void CollectAndStore_WhenPollingReturnsCurrentSample_ShouldAddSampleToTrend()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);
        ITrendHistoryService trendHistoryService = new TrendHistoryService();

        var pollingCoordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        var trendCollectionService = new TrendCollectionService(
            pollingCoordinator,
            trendHistoryService);

        var snapshot0 = CreateSnapshot(0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var result = trendCollectionService.CollectAndStore();
        var samples = trendHistoryService.GetSamples();

        Assert.True(result.HasSample);
        Assert.Single(samples);
        Assert.Equal(0, samples[0].SequenceNumber);
    }

    [Fact]
    public void CollectAndStore_WhenPollingRecoversFromBuffer_ShouldAddRecoveredSampleToTrend()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);
        ITrendHistoryService trendHistoryService = new TrendHistoryService();

        var pollingCoordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        var trendCollectionService = new TrendCollectionService(
            pollingCoordinator,
            trendHistoryService);

        var snapshot0 = CreateSnapshot(0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var first = trendCollectionService.CollectAndStore();
        Assert.True(first.HasSample);

        for (int seq = 1; seq <= 7; seq++)
        {
            var snapshot = CreateSnapshot(seq);
            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);
        }

        var recovery = trendCollectionService.CollectAndStore();
        var samples = trendHistoryService.GetSamples();

        Assert.True(recovery.HasSample);
        Assert.True(recovery.RecoveredFromBuffer);
        Assert.Equal(2, samples.Count);
        Assert.Equal(5, samples.Last().SequenceNumber);
    }

    [Fact]
    public void CollectAndStore_WhenPollingDetectsGap_ShouldNotAddSampleToTrend()
    {
        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(3);
        ITrendHistoryService trendHistoryService = new TrendHistoryService();

        var pollingCoordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        var trendCollectionService = new TrendCollectionService(
            pollingCoordinator,
            trendHistoryService);

        var snapshot0 = CreateSnapshot(0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var first = trendCollectionService.CollectAndStore();
        Assert.True(first.HasSample);

        for (int seq = 7; seq <= 9; seq++)
        {
            var snapshot = CreateSnapshot(seq);
            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);
        }

        var gap = trendCollectionService.CollectAndStore();
        var samples = trendHistoryService.GetSamples();

        Assert.False(gap.HasSample);
        Assert.True(gap.GapDetected);
        Assert.Single(samples);
        Assert.Equal(0, samples[0].SequenceNumber);
    }

    private static SimulationSnapshot CreateSnapshot(int sequenceNumber)
    {
        return new SimulationSnapshot
        {
            SequenceNumber = sequenceNumber,
            SimulatedMinutes = sequenceNumber,
            ProducedAtUtc = DateTime.UtcNow,
            RoomTemperature = 18.0 + sequenceNumber * 0.1,
            Setpoint = 22.0,
            ControllerOutput = 50.0,
            ControlError = 22.0 - (18.0 + sequenceNumber * 0.1),
            HeatingPowerKW = 2.0
        };
    }
}