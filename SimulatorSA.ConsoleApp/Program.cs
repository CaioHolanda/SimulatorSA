using SimulatorSA.Application.DTOs;
using SimulatorSA.Application.Interfaces;
using SimulatorSA.Application.Services;
using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;

namespace SimulatorSA.ConsoleApp;

public class Program
{
    public static void Main()
    {
        RunTrendCollectionDemo();
    }
    private static void RunTrendCollectionDemo()
    {
        Console.WriteLine("=== Trend Collection Demo ===");
        Console.WriteLine();

        var room = new Room(
            name: "Room A",
            initialTemperature: 18.0,
            heatLossCoefficientKWPerDegree: 0.08,
            thermalCapacityKWhPerDegree: 2.5);

        var controller = new PidController(
            setpoint: 22.0,
            proportionalGain: 8.0,
            integralGain: 0.15,
            derivativeGain: 2.0);

        var actuator = new ThermalActuator("Heating Coil");

        var engine = new SimulationEngine(
            room,
            controller,
            actuator,
            outdoorTemperature: 10.0,
            maxHeatingPowerKW: 5.0,
            deltaTimeMinutes: 1.0);

        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(capacity: 5);
        ITrendHistoryService trendHistoryService = new TrendHistoryService();

        IPollingCoordinator pollingCoordinator = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        ITrendCollectionService trendCollectionService = new TrendCollectionService(
            pollingCoordinator,
            trendHistoryService);

        Console.WriteLine("---- Phase 1: Initial collection (seq 0) ----");
        AdvanceSimulation(engine, currentStateStore, snapshotBuffer, steps: 1);
        var result0 = trendCollectionService.CollectAndStore();
        PrintCollectionResult(result0, trendHistoryService);

        Console.WriteLine();
        Console.WriteLine("---- Phase 2: Recovery from buffer (expected seq 5, current seq 7) ----");
        AdvanceSimulation(engine, currentStateStore, snapshotBuffer, steps: 7);
        var resultRecovery = trendCollectionService.CollectAndStore();
        PrintCollectionResult(resultRecovery, trendHistoryService);

        Console.WriteLine();
        Console.WriteLine("---- Phase 3: Gap detection (expected seq 10, current seq 15, buffer lost seq 10) ----");
        AdvanceSimulation(engine, currentStateStore, snapshotBuffer, steps: 8);
        var resultGap = trendCollectionService.CollectAndStore();
        PrintCollectionResult(resultGap, trendHistoryService);

        Console.WriteLine();
        Console.WriteLine("=== Final Buffer State ===");
        foreach (var snapshot in snapshotBuffer.GetAll())
        {
            Console.WriteLine(
                $"seq={snapshot.SequenceNumber,2} | " +
                $"sim={snapshot.SimulatedMinutes,4:0} min | " +
                $"temp={snapshot.RoomTemperature:F2} °C | " +
                $"heat={snapshot.HeatingPowerKW:F2} kW");
        }

        Console.WriteLine();
        PrintTrendHistory(trendHistoryService);
    }

    private static void AdvanceSimulation(
        SimulationEngine engine,
        ICurrentSimulationStateStore currentStateStore,
        ISnapshotBuffer snapshotBuffer,
        int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            var snapshot = engine.Step();

            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);

            Console.WriteLine(
                $"[STEP] seq={snapshot.SequenceNumber,2} | " +
                $"sim={snapshot.SimulatedMinutes,4:0} min | " +
                $"temp={snapshot.RoomTemperature:F2} °C | " +
                $"output={snapshot.ControllerOutput:F1} % | " +
                $"heat={snapshot.HeatingPowerKW:F2} kW");
        }
    }

    private static void PrintCollectionResult(
        Application.DTOs.PollingResult result,
        ITrendHistoryService trendHistoryService)
    {
        Console.WriteLine();
        Console.WriteLine($"Has sample            : {result.HasSample}");
        Console.WriteLine($"Recovered from buffer : {result.RecoveredFromBuffer}");
        Console.WriteLine($"Gap detected          : {result.GapDetected}");
        Console.WriteLine($"Expected sequence     : {result.ExpectedSequenceNumber}");
        Console.WriteLine($"Actual sequence       : {(result.ActualSequenceNumber?.ToString() ?? "null")}");
        Console.WriteLine($"Status                : {result.Status}");

        if (result.Sample is not null)
        {
            Console.WriteLine(
                $"Stored sample         : seq={result.Sample.SequenceNumber}, " +
                $"sim={result.Sample.SimulatedMinutes:0} min, " +
                $"temp={result.Sample.IndoorTemperature:F2} °C, " +
                $"heat={result.Sample.HeaterOutput:F2} kW, " +
                $"timestamp={result.Sample.Timestamp:HH:mm:ss}");
        }

        Console.WriteLine($"Trend sample count    : {trendHistoryService.GetSamples().Count}");
    }

    private static void PrintTrendHistory(ITrendHistoryService trendHistoryService)
    {
        var samples = trendHistoryService.GetSamples();

        Console.WriteLine("=== Final Trend History ===");

        if (samples.Count == 0)
        {
            Console.WriteLine("No samples stored.");
            return;
        }

        foreach (var sample in samples)
        {
            Console.WriteLine(
                $"seq={sample.SequenceNumber,2} | " +
                $"sim={sample.SimulatedMinutes,4:0} min | " +
                $"temp={sample.IndoorTemperature:F2} °C | " +
                $"outdoor={sample.OutdoorTemperature:F2} °C | " +
                $"setpoint={sample.Setpoint:F2} °C | " +
                $"heat={sample.HeaterOutput:F2} kW | " +
                $"timestamp={sample.Timestamp:HH:mm:ss}");
        }
    }
    private static void RunLiveBufferDemo()
    {
        var room = new Room(
            name: "Room A",
            initialTemperature: 18.0,
            heatLossCoefficientKWPerDegree: 0.08,
            thermalCapacityKWhPerDegree: 2.5);

        var controller = new PidController(
            setpoint: 22.0,
            proportionalGain: 8.0,
            integralGain: 0.15,
            derivativeGain: 2.0);

        var actuator = new ThermalActuator("Heating Coil");

        var engine = new SimulationEngine(
            room,
            controller,
            actuator,
            outdoorTemperature: 10.0,
            maxHeatingPowerKW: 5.0,
            deltaTimeMinutes: 1.0);

        ISnapshotBuffer buffer = new CircularSnapshotBuffer(capacity: 5);

        Console.WriteLine("=== Live Simulation Buffer Demo ===");
        Console.WriteLine();

        for (int i = 0; i < 12; i++)
        {
            var snapshot = engine.Step();
            buffer.Add(snapshot);

            Console.WriteLine(
                $"[STEP] seq={snapshot.SequenceNumber,2} | " +
                $"sim={snapshot.SimulatedMinutes,4:0} min | " +
                $"Temp={snapshot.RoomTemperature,5:F2} °C | " +
                $"Output={snapshot.ControllerOutput,6:F1} % | " +
                $"Heat={snapshot.HeatingPowerKW,5:F2} kW");
        }

        Console.WriteLine();
        Console.WriteLine("=== Buffer State ===");
        Console.WriteLine($"Capacity         : {buffer.Capacity}");
        Console.WriteLine($"Stored snapshots : {buffer.Count}");

        var allSnapshots = buffer.GetAll();

        Console.WriteLine();
        Console.WriteLine("Snapshots currently retained:");
        foreach (var snapshot in allSnapshots)
        {
            Console.WriteLine(
                $"seq={snapshot.SequenceNumber,2} | sim={snapshot.SimulatedMinutes,4:0} min | Temp={snapshot.RoomTemperature:F2} °C");
        }

        var oldest = allSnapshots.FirstOrDefault();
        var latest = buffer.GetLatest();

        Console.WriteLine();
        Console.WriteLine("=== Buffer Validation ===");

        if (oldest is not null)
            Console.WriteLine($"Oldest retained seq : {oldest.SequenceNumber}");

        if (latest is not null)
            Console.WriteLine($"Latest retained seq : {latest.SequenceNumber}");

        int expectedOldest = 7;
        int expectedLatest = 11;

        Console.WriteLine($"Expected oldest seq : {expectedOldest}");
        Console.WriteLine($"Expected latest seq : {expectedLatest}");

        bool sizeOk = buffer.Count == 5;
        bool oldestOk = oldest?.SequenceNumber == expectedOldest;
        bool latestOk = latest?.SequenceNumber == expectedLatest;

        Console.WriteLine($"Capacity respected  : {(sizeOk ? "YES" : "NO")}");
        Console.WriteLine($"Discard worked      : {(oldestOk && latestOk ? "YES" : "NO")}");
    }
    private static void RunPollingRecoveryDemo()
    {
        Console.WriteLine("=== Polling Recovery Demo ===");

        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(10);

        var polling = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        // First sample at seq 0
        var snapshot0 = CreateSnapshot(0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var first = polling.TryCollect();

        PrintPollingResult("Initial collection", first);

        // Advance simulation to seq 7
        for (int seq = 1; seq <= 7; seq++)
        {
            var snapshot = CreateSnapshot(seq);
            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);
        }

        var recovery = polling.TryCollect();

        PrintPollingResult("Recovery attempt", recovery);

        Console.WriteLine("Buffer content:");
        foreach (var snapshot in snapshotBuffer.GetAll())
        {
            Console.WriteLine(
                $"  seq={snapshot.SequenceNumber,2} | sim={snapshot.SimulatedMinutes,4:0} min | Temp={snapshot.RoomTemperature:F2} °C");
        }
    }
    private static void RunPollingGapDemo()
    {
        Console.WriteLine("=== Polling Gap Demo ===");

        ICurrentSimulationStateStore currentStateStore = new CurrentSimulationStateStore();
        ISnapshotBuffer snapshotBuffer = new CircularSnapshotBuffer(3);

        var polling = new PollingCoordinator(
            currentStateStore,
            snapshotBuffer,
            pollingIntervalMinutes: 5,
            outdoorTemperature: 10.0);

        // First sample at seq 0
        var snapshot0 = CreateSnapshot(0);
        currentStateStore.Update(snapshot0);
        snapshotBuffer.Add(snapshot0);

        var first = polling.TryCollect();

        PrintPollingResult("Initial collection", first);

        // Advance directly to seq 7, 8, 9 so seq 5 is no longer available
        for (int seq = 7; seq <= 9; seq++)
        {
            var snapshot = CreateSnapshot(seq);
            currentStateStore.Update(snapshot);
            snapshotBuffer.Add(snapshot);
        }

        var gap = polling.TryCollect();

        PrintPollingResult("Gap attempt", gap);

        Console.WriteLine("Buffer content:");
        foreach (var snapshot in snapshotBuffer.GetAll())
        {
            Console.WriteLine(
                $"  seq={snapshot.SequenceNumber,2} | sim={snapshot.SimulatedMinutes,4:0} min | Temp={snapshot.RoomTemperature:F2} °C");
        }
    }
    private static void PrintPollingResult(string title, PollingResult result)
    {
        Console.WriteLine($"-- {title} --");
        Console.WriteLine($"Has sample            : {result.HasSample}");
        Console.WriteLine($"Recovered from buffer : {result.RecoveredFromBuffer}");
        Console.WriteLine($"Gap detected          : {result.GapDetected}");
        Console.WriteLine($"Expected sequence     : {result.ExpectedSequenceNumber}");
        Console.WriteLine($"Actual sequence       : {(result.ActualSequenceNumber?.ToString() ?? "null")}");
        Console.WriteLine($"Status                : {result.Status}");

        if (result.Sample is not null)
        {
            Console.WriteLine(
                $"Sample => seq={result.Sample.SequenceNumber}, " +
                $"sim={result.Sample.SimulatedMinutes:0} min, " +
                $"temp={result.Sample.IndoorTemperature:F2} °C, " +
                $"heat={result.Sample.HeaterOutput:F2} kW, " +
                $"timestamp={result.Sample.Timestamp:HH:mm:ss}");
        }

        Console.WriteLine();
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
