using SimulatorSA.Application.Services;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Tests.Application.Services;

public class CircularSnapshotBufferTests
{
    [Fact]
    public void GetLatest_WhenBufferIsEmpty_ShouldReturnNull()
    {
        var buffer = new CircularSnapshotBuffer(5);

        var latest = buffer.GetLatest();

        Assert.Null(latest);
        Assert.Equal(0, buffer.Count);
    }

    [Fact]
    public void Add_ShouldStoreSnapshotsUntilCapacity()
    {
        var buffer = new CircularSnapshotBuffer(3);

        buffer.Add(CreateSnapshot(0));
        buffer.Add(CreateSnapshot(1));
        buffer.Add(CreateSnapshot(2));

        Assert.Equal(3, buffer.Count);
        Assert.Equal(2, buffer.GetLatest()!.SequenceNumber);
    }

    [Fact]
    public void Add_WhenCapacityIsExceeded_ShouldDiscardOldestSnapshot()
    {
        var buffer = new CircularSnapshotBuffer(3);

        buffer.Add(CreateSnapshot(0));
        buffer.Add(CreateSnapshot(1));
        buffer.Add(CreateSnapshot(2));
        buffer.Add(CreateSnapshot(3));

        var all = buffer.GetAll();

        Assert.Equal(3, buffer.Count);
        Assert.DoesNotContain(all, s => s.SequenceNumber == 0);
        Assert.Contains(all, s => s.SequenceNumber == 1);
        Assert.Contains(all, s => s.SequenceNumber == 2);
        Assert.Contains(all, s => s.SequenceNumber == 3);
    }

    [Fact]
    public void GetBySequence_ShouldReturnSnapshot_WhenItExistsInBuffer()
    {
        var buffer = new CircularSnapshotBuffer(3);

        buffer.Add(CreateSnapshot(10));
        buffer.Add(CreateSnapshot(11));
        buffer.Add(CreateSnapshot(12));

        var snapshot = buffer.GetBySequence(11);

        Assert.NotNull(snapshot);
        Assert.Equal(11, snapshot!.SequenceNumber);
    }

    [Fact]
    public void GetBySequence_ShouldReturnNull_WhenSnapshotWasDiscarded()
    {
        var buffer = new CircularSnapshotBuffer(3);

        buffer.Add(CreateSnapshot(10));
        buffer.Add(CreateSnapshot(11));
        buffer.Add(CreateSnapshot(12));
        buffer.Add(CreateSnapshot(13));

        var snapshot = buffer.GetBySequence(10);

        Assert.Null(snapshot);
    }

    [Fact]
    public void GetRange_ShouldReturnSnapshotsWithinSequenceInterval()
    {
        var buffer = new CircularSnapshotBuffer(10);

        for (int i = 0; i < 6; i++)
        {
            buffer.Add(CreateSnapshot(i));
        }

        var range = buffer.GetRange(2, 4);

        Assert.Equal(3, range.Count);
        Assert.Collection(range,
            s => Assert.Equal(2, s.SequenceNumber),
            s => Assert.Equal(3, s.SequenceNumber),
            s => Assert.Equal(4, s.SequenceNumber));
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