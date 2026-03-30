using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Enumerators;
using SimulatorSA.Core.Services;
using SimulatorSA.Core.Spaces;

public class Program
{
    public static void Main()
    {

        var room = new OfficeA("Office-A01", 18.0);

        var controller = new PidController(
            setpoint: 22.0,
            proportionalGain: 10.0,
            integralGain: 0.2,
            differentialGain: 1.0,
            timeStep: 1.0);
        var valve = new ValveActuator("Heating Valve");

        var runner = new SimulationRunner();

        var result = runner.Run(
            room: room,
            controller: controller,
            valve: valve,
            outdoorTemperature: 10.0,
            maxHeatingDelta: 0.2,
            totalSteps: 70);

        foreach (var snapshot in result.Snapshots)
        {
            Console.WriteLine(
                $"Time: {snapshot.Time,3} min | " +
                $"Temp: {snapshot.Values[SimulationVariables.Temperature]:F2} °C | " +
                $"Valve: {snapshot.Values[SimulationVariables.ValveOpening]:F2} % | " +
                $"Heat: {snapshot.Values[SimulationVariables.HeatingEffect]:F4} °C");
        }
    }
}
