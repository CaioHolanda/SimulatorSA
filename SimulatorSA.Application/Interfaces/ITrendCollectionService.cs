using SimulatorSA.Application.DTOs;

namespace SimulatorSA.Application.Interfaces;

public interface ITrendCollectionService
{
    PollingResult CollectAndStore();
}