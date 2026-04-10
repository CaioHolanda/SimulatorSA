using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationAnalysis;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;
using SimulatorSA.Core.Spaces;

namespace SimulatorSA.ConsoleApp
{
    public class Program
    {
        private sealed record SimulationEvent(double Time, string Description);

        public static void Main()
        {
            double setpoint = 22.0;
            double deltaTimeMinutes = 1.0;
            int totalSteps = 90;
            double outdoorTemperature = 15.0;
            double maxHeatingPowerKW = 5.0;
            double comfortBandHalfWidth = 0.5;
            string roomName = "Office-A01";

            var pidController = new PidController(
                setpoint: setpoint,
                proportionalGain: 15.0,
                integralGain: 1.5,
                derivativeGain: 0.6);

            var onOffController = new OnOffController(
                setpoint: setpoint,
                outputWhenOn: 100,
                outputWhenOff: 0,
                hysteresis: 0,
                minOnTime: 1,
                minOffTime: 1);

            var pidRun = RunSimulation(
                controller: pidController,
                roomName: roomName,
                initialTemperature: 18.0,
                outdoorTemperature: outdoorTemperature,
                maxHeatingPowerKW: maxHeatingPowerKW,
                totalSteps: totalSteps,
                deltaTimeMinutes: deltaTimeMinutes);

            var onOffRun = RunSimulation(
                controller: onOffController,
                roomName: roomName,
                initialTemperature: 18.0,
                outdoorTemperature: outdoorTemperature,
                maxHeatingPowerKW: maxHeatingPowerKW,
                totalSteps: totalSteps,
                deltaTimeMinutes: deltaTimeMinutes);

            var metricsCalculator = new SimulationMetricsCalculator();

            var pidMetrics = metricsCalculator.Calculate(
                pidRun.Result,
                comfortBandHalfWidth,
                deltaTimeMinutes);

            var onOffMetrics = metricsCalculator.Calculate(
                onOffRun.Result,
                comfortBandHalfWidth,
                deltaTimeMinutes);

            PrintResult("PID Controller", pidRun.Result, pidRun.Events);
            PrintMetrics("PID Controller", pidMetrics);

            PrintResult("On-Off Controller", onOffRun.Result, onOffRun.Events);
            PrintMetrics("On-Off Controller", onOffMetrics);
        }

        private static (SimulationResult Result, List<SimulationEvent> Events) RunSimulation(
            IController controller,
            string roomName,
            double initialTemperature,
            double outdoorTemperature,
            double maxHeatingPowerKW,
            int totalSteps,
            double deltaTimeMinutes)
        {
            var room = new OfficeA(roomName, initialTemperature);
            var actuator = new ThermalActuator("Heating Actuator");
            var runner = new SimulationRunner();
            var events = new List<SimulationEvent>();

            Action<Room, int, double> perturbationScript = (currentRoom, step, time) =>
            {
                if (currentRoom is not OfficeA office)
                    return;

                if (step == 10)
                {
                    office.SetOccupied(true);
                    events.Add(new SimulationEvent(time, "Occupied"));
                }

                if (step == 15)
                {
                    office.SetComputerOn(true);
                    events.Add(new SimulationEvent(time, "Computer On"));
                }

                if (step == 25)
                {
                    office.SetWindowOpen(true);
                    events.Add(new SimulationEvent(time, "Window Opened"));
                }

                if (step == 40)
                {
                    office.SetWindowOpen(false);
                    events.Add(new SimulationEvent(time, "Window Closed"));
                }

                if (step == 50)
                {
                    office.SetComputerOn(false);
                    events.Add(new SimulationEvent(time, "Computer Off"));
                }

                if (step == 60)
                {
                    office.SetOccupied(false);
                    events.Add(new SimulationEvent(time, "Unoccupied"));
                }
            };

            var result = runner.Run(
                room: room,
                controller: controller,
                actuator: actuator,
                outdoorTemperature: outdoorTemperature,
                maxHeatingPowerKW: maxHeatingPowerKW,
                totalSteps: totalSteps,
                deltaTimeMinutes: deltaTimeMinutes,
                stepAction: perturbationScript);

            return (result, events);
        }

        private static void PrintResult(
            string title,
            SimulationResult result,
            List<SimulationEvent> events)
        {
            Console.WriteLine();
            Console.WriteLine($"===== {title} =====");

            foreach (var snapshot in result.Snapshots)
            {
                Console.WriteLine(
                    $"Time: {snapshot.Time,6:F1} min | " +
                    $"Temp: {snapshot.Values[SimulationVariables.Temperature]:F2} °C | " +
                    $"Output: {snapshot.Values[SimulationVariables.ValveOpening]:F2} % | " +
                    $"Thermal Power: {snapshot.Values[SimulationVariables.HeatingEffect]:F4} kW");

                var eventsAtThisTime = events
                    .Where(e => e.Time == snapshot.Time)
                    .ToList();

                foreach (var simulationEvent in eventsAtThisTime)
                {
                    Console.WriteLine($"                 [Event] {simulationEvent.Description}");
                }
            }
        }

        private static void PrintMetrics(string title, SimulationMetrics metrics)
        {
            Console.WriteLine();
            Console.WriteLine($"----- {title} Metrics -----");
            Console.WriteLine($"Final Temperature: {metrics.FinalTemperature:F2} °C");
            Console.WriteLine($"Final Output: {metrics.FinalOutput:F2} %");

            Console.WriteLine($"Mean Absolute Error: {metrics.MeanAbsoluteError:F4}");
            Console.WriteLine($"Mean Squared Error: {metrics.MeanSquaredError:F4}");
            Console.WriteLine($"Max Absolute Error: {metrics.MaxAbsoluteError:F4}");

            Console.WriteLine($"Oscillation Amplitude: {metrics.TemperatureOscillationAmplitude:F4} °C");
            Console.WriteLine($"Max Overshoot: {metrics.MaxOvershoot:F4} °C");
            Console.WriteLine($"Max Undershoot: {metrics.MaxUndershoot:F4} °C");

            Console.WriteLine($"Number of Turn Ons: {metrics.NumberOfTurnOns}");
            Console.WriteLine($"Number of Turn Offs: {metrics.NumberOfTurnOffs}");
            Console.WriteLine($"Total On Time: {metrics.TotalOnTime:F2} min");
            Console.WriteLine($"Average On Cycle: {metrics.AverageOnCycleDuration:F2} min");
            Console.WriteLine($"Average Off Cycle: {metrics.AverageOffCycleDuration:F2} min");

            Console.WriteLine($"Comfort Band: [{metrics.ComfortBandLowerLimit:F2}, {metrics.ComfortBandUpperLimit:F2}] °C");
            Console.WriteLine($"Time Within Comfort Band: {metrics.TimeWithinComfortBand:F2} min");
            Console.WriteLine($"Comfort Band Percentage: {metrics.ComfortBandPercentage:F2} %");

            Console.WriteLine($"Total Thermal Energy Delivered: {metrics.TotalThermalEnergyDelivered:F4} kWh");
            Console.WriteLine($"Thermal Energy Per Comfort Time: {metrics.ThermalEnergyPerComfortTime:F4} kWh/min");
        }
    }
}