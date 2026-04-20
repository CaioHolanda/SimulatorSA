using SimulatorSA.Application.Scenarios;
using SimulatorSA.Application.Services;
using SimulatorSA.ConsoleApp.Output;
using SimulatorSA.ConsoleApp.Runtime;

namespace SimulatorSA.ConsoleApp;

public class Program
{
    public static void Main()
    {
        var scenario = new PidScenarioDefinition();
        var runner = new SimulationScenarioRunner();

        var result = runner.RunPidScenario(scenario);

        ResultPrinter.Print(result);

        Console.WriteLine();
        Console.WriteLine("=== Supervisory Replay Demo ===");

        var stateProvider = new ReplaySimulationStateProvider();
        var supervisoryService = new SupervisoryStateService(stateProvider);
        var pollingService = new HmiPollingService(supervisoryService);
        var trendService = new TrendHistoryService(maxSamples: 20);

        const int pollingIntervalMinutes = 5;
        const int trendingIntervalMinutes = 10;

        int pollingReads = 0;
        int trendSamples = 0;

        foreach (var snapshot in result.Snapshots)
        {
            stateProvider.UpdateFromSnapshot(
                snapshot,
                roomName: scenario.RoomName,
                outdoorTemperature: scenario.OutdoorTemperature,
                controllerType: "PID");

            Console.WriteLine(
                $"[BUFFER] t={snapshot.Time,4:0} min | " +
                $"Temp={snapshot.RoomTemperature,5:F2} °C | " +
                $"Setpoint={snapshot.Setpoint,5:F2} °C | " +
                $"Output={snapshot.ControllerOutput,6:F1} % | " +
                $"Heat={snapshot.HeatingPowerKW,5:F2} kW");

            if (((int)snapshot.Time) % pollingIntervalMinutes == 0)
            {
                var polled = pollingService.CollectSample();
                pollingReads++;

                Console.WriteLine(
                    $"  [POLL ] real={polled.Timestamp:HH:mm:ss} | " +
                    $"sim={snapshot.Time,4:0} min | " +
                    $"Indoor={polled.IndoorTemperature,5:F2} °C | " +
                    $"Outdoor={polled.OutdoorTemperature,5:F2} °C | " +
                    $"Setpoint={polled.Setpoint,5:F2} °C | " +
                    $"Heat={polled.HeaterOutput,5:F2} kW | " +
                    $"Enabled={polled.HeaterEnabled}");
            }

            if (((int)snapshot.Time) % trendingIntervalMinutes == 0)
            {
                var sample = pollingService.CollectSample();
                trendService.AddSample(sample);
                trendSamples++;

                Console.WriteLine(
                    $"  [TREND] stored sample #{trendSamples} at sim={snapshot.Time,4:0} min");
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Replay Summary ===");
        Console.WriteLine($"Simulation snapshots : {result.Snapshots.Count}");
        Console.WriteLine($"Polling reads        : {pollingReads}");
        Console.WriteLine($"Trend samples stored : {trendSamples}");

        var storedSamples = trendService.GetSamples();

        if (storedSamples.Count > 0)
        {
            var last = storedSamples.Last();

            Console.WriteLine(
                $"Last trend sample    : real={last.Timestamp:HH:mm:ss}, " +
                $"Indoor={last.IndoorTemperature:F2} °C, " +
                $"Outdoor={last.OutdoorTemperature:F2} °C, " +
                $"Setpoint={last.Setpoint:F2} °C, " +
                $"Heat={last.HeaterOutput:F2} kW");
        }

        Console.WriteLine();
        Console.WriteLine("=== Trend History ===");

        int index = 1;
        foreach (var sample in storedSamples)
        {
            Console.WriteLine(
                $"[{index:00}] real={sample.Timestamp:HH:mm:ss} | " +
                $"Indoor={sample.IndoorTemperature,5:F2} °C | " +
                $"Outdoor={sample.OutdoorTemperature,5:F2} °C | " +
                $"Setpoint={sample.Setpoint,5:F2} °C | " +
                $"Heat={sample.HeaterOutput,5:F2} kW | " +
                $"Enabled={sample.HeaterEnabled}");

            index++;
        }
    }
}