using SimulatorSA.Application.Services;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Tests.Application.Services;

public class CurrentSimulationStateStoreTests
{
    [Fact]
    public void GetCurrent_WhenStoreIsEmpty_ShouldReturnNull()
    {
        var store = new CurrentSimulationStateStore();

        var current = store.GetCurrent();

        Assert.Null(current);
        Assert.False(store.HasValue);
    }

    [Fact]
    public void Update_ShouldStoreLatestSnapshot()
    {
        var store = new CurrentSimulationStateStore();

        var snapshot = CreateSnapshot(sequenceNumber: 5);

        store.Update(snapshot);

        var current = store.GetCurrent();

        Assert.True(store.HasValue);
        Assert.NotNull(current);
        Assert.Equal(5, current!.SequenceNumber);
    }

    [Fact]
    public void Update_WhenCalledMultipleTimes_ShouldKeepOnlyMostRecentSnapshot()
    {
        var store = new CurrentSimulationStateStore();

        store.Update(CreateSnapshot(1));
        store.Update(CreateSnapshot(2));
        store.Update(CreateSnapshot(3));

        var current = store.GetCurrent();

        Assert.NotNull(current);
        Assert.Equal(3, current!.SequenceNumber);
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
            ControlError = 4.0,
            HeatingPowerKW = 2.0
        };
    }
}