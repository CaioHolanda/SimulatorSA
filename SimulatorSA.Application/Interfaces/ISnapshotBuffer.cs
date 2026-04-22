/*
    guardar últimos N snapshots
    consultar o mais recente
    consultar por sequência
    consultar faixa
    permitir verificar capacidade e contagem
 */

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