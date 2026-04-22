using SimulatorSA.Application.DTOs;

namespace SimulatorSA.Application.Interfaces;

public interface IPollingCoordinator
{
    int PollingIntervalMinutes { get; }
    int? LastCollectedSequenceNumber { get; }

    PollingResult TryCollect();
}