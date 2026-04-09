using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
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
            double deltaTime = 1.0;
            int totalSteps = 70;
            double outdoorTemperature = 10.0;
            double maxHeatingRate = 0.2;
            string roomName = "Office-A01";

            var pidController = new PidController(
                setpoint: setpoint,
                proportionalGain: 10.0,
                integralGain: 0.2,
                derivativeGain: 1.0);

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
                maxHeatingRate: maxHeatingRate,
                totalSteps: totalSteps,
                deltaTime: deltaTime);

            var onOffRun = RunSimulation(
                controller: onOffController,
                roomName: roomName,
                initialTemperature: 18.0,
                outdoorTemperature: outdoorTemperature,
                maxHeatingRate: maxHeatingRate,
                totalSteps: totalSteps,
                deltaTime: deltaTime);

            PrintResult("PID Controller", pidRun.Result, pidRun.Events);
            PrintResult("On-Off Controller", onOffRun.Result, onOffRun.Events);
        }

        private static (SimulationResult Result, List<SimulationEvent> Events) RunSimulation(
            IController controller,
            string roomName,
            double initialTemperature,
            double outdoorTemperature,
            double maxHeatingRate,
            int totalSteps,
            double deltaTime)
        {
            var room = new OfficeA(roomName, initialTemperature);
            var valve = new ValveActuator("Heating Valve");
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
                valve: valve,
                outdoorTemperature: outdoorTemperature,
                maxHeatingRate: maxHeatingRate,
                totalSteps: totalSteps,
                deltaTime: deltaTime,
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
                    $"Valve: {snapshot.Values[SimulationVariables.ValveOpening]:F2} % | " +
                    $"Heat: {snapshot.Values[SimulationVariables.HeatingEffect]:F4} °C");

                var eventsAtThisTime = events
                    .Where(e => e.Time == snapshot.Time)
                    .ToList();

                foreach (var simulationEvent in eventsAtThisTime)
                {
                    Console.WriteLine($"                 [Event] {simulationEvent.Description}");
                }
            }
        }
    }
}