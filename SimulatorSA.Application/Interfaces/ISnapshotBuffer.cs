using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Application.Interfaces;

public interface ISnapshotBuffer
{
    int Capacity { get; }
    int Count { get; }

    void Add(SimulationSnapshot snapshot);

    SimulationSnapshot? GetLatest();

    SimulationSnapshot? GetBySequence(int sequenceNumber);

    IReadOnlyList<SimulationSnapshot> GetRange(int startSequenceNumber, int endSequenceNumber);

    IReadOnlyList<SimulationSnapshot> GetAll();
}