using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Application.Services;

public class PollingCoordinator : IPollingCoordinator
{
    private readonly ICurrentSimulationStateStore _currentStateStore;
    private readonly ISnapshotBuffer _snapshotBuffer;
    private readonly double _outdoorTemperature;

    public int PollingIntervalMinutes { get; }
    public int? LastCollectedSequenceNumber { get; private set; }

    public PollingCoordinator(
        ICurrentSimulationStateStore currentStateStore,
        ISnapshotBuffer snapshotBuffer,
        int pollingIntervalMinutes,
        double outdoorTemperature)
    {
        ArgumentNullException.ThrowIfNull(currentStateStore);
        ArgumentNullException.ThrowIfNull(snapshotBuffer);

        if (pollingIntervalMinutes < 1 || pollingIntervalMinutes > 10)
            throw new ArgumentOutOfRangeException(nameof(pollingIntervalMinutes),
                "Polling interval must be between 1 and 10 minutes.");

        _currentStateStore = currentStateStore;
        _snapshotBuffer = snapshotBuffer;
        _outdoorTemperature = outdoorTemperature;
        PollingIntervalMinutes = pollingIntervalMinutes;
    }

    public PollingResult TryCollect()
    {
        var current = _currentStateStore.GetCurrent();

        if (current is null)
        {
            return new PollingResult
            {
                HasSample = false,
                Status = "No current state available."
            };
        }

        int expectedSequence = LastCollectedSequenceNumber is null
            ? 0
            : LastCollectedSequenceNumber.Value + PollingIntervalMinutes;

        if (current.SequenceNumber == expectedSequence)
        {
            var sample = CreateTrendSample(current);

            LastCollectedSequenceNumber = current.SequenceNumber;

            return new PollingResult
            {
                HasSample = true,
                RecoveredFromBuffer = false,
                GapDetected = false,
                ExpectedSequenceNumber = expectedSequence,
                ActualSequenceNumber = current.SequenceNumber,
                Status = "Collected from current state.",
                Sample = sample
            };
        }

        if (current.SequenceNumber > expectedSequence)
        {
            var recovered = _snapshotBuffer.GetBySequence(expectedSequence);

            if (recovered is not null)
            {
                var sample = CreateTrendSample(recovered);

                LastCollectedSequenceNumber = recovered.SequenceNumber;

                return new PollingResult
                {
                    HasSample = true,
                    RecoveredFromBuffer = true,
                    GapDetected = false,
                    ExpectedSequenceNumber = expectedSequence,
                    ActualSequenceNumber = recovered.SequenceNumber,
                    Status = "Recovered expected sample from buffer.",
                    Sample = sample
                };
            }

            return new PollingResult
            {
                HasSample = false,
                RecoveredFromBuffer = false,
                GapDetected = true,
                ExpectedSequenceNumber = expectedSequence,
                ActualSequenceNumber = current.SequenceNumber,
                Status = "Expected sample not found in current state or buffer."
            };
        }

        return new PollingResult
        {
            HasSample = false,
            RecoveredFromBuffer = false,
            GapDetected = false,
            ExpectedSequenceNumber = expectedSequence,
            ActualSequenceNumber = current.SequenceNumber,
            Status = "Expected sequence not reached yet."
        };
    }

    private TrendSampleDto CreateTrendSample(SimulationSnapshot snapshot)
    {
        return new TrendSampleDto
        {
            SequenceNumber = snapshot.SequenceNumber,
            SimulatedMinutes = snapshot.SimulatedMinutes,
            Timestamp = DateTime.UtcNow,
            CollectedAtUtc = DateTime.UtcNow,
            IndoorTemperature = snapshot.RoomTemperature,
            OutdoorTemperature = _outdoorTemperature,
            Setpoint = snapshot.Setpoint,
            HeaterOutput = snapshot.HeatingPowerKW,
            HeaterEnabled = snapshot.HeatingPowerKW > 0
        };
    }
}