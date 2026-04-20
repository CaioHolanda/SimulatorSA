using SimulatorSA.Application.Interfaces;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Application.Services;

public class CircularSnapshotBuffer : ISnapshotBuffer
{
    private readonly Queue<SimulationSnapshot> _snapshots;

    public int Capacity { get; }
    public int Count => _snapshots.Count;

    public CircularSnapshotBuffer(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Buffer capacity must be greater than zero.");

        Capacity = capacity;
        _snapshots = new Queue<SimulationSnapshot>(capacity);
    }

    public void Add(SimulationSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        if (_snapshots.Count >= Capacity)
            _snapshots.Dequeue();

        _snapshots.Enqueue(snapshot);
    }

    public SimulationSnapshot? GetLatest()
    {
        return _snapshots.Count == 0 ? null : _snapshots.Last();
    }

    public SimulationSnapshot? GetBySequence(int sequenceNumber)
    {
        return _snapshots.FirstOrDefault(s => s.SequenceNumber == sequenceNumber);
    }

    public IReadOnlyList<SimulationSnapshot> GetRange(int startSequenceNumber, int endSequenceNumber)
    {
        if (endSequenceNumber < startSequenceNumber)
            throw new ArgumentException("End sequence number must be greater than or equal to start sequence number.");

        return _snapshots
            .Where(s => s.SequenceNumber >= startSequenceNumber && s.SequenceNumber <= endSequenceNumber)
            .ToList();
    }

    public IReadOnlyList<SimulationSnapshot> GetAll()
    {
        return _snapshots.ToList();
    }
}