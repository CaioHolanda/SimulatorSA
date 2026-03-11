using SimulatorSA.Console.Sensors;
using SimulatorSA.Console.Enumerators;
using SimulatorSA.Console.Actuators;
using SimulatorSA.Console.Controllers;

public class Program
{
    public static void Main()
    {
        var sensor=new AnalogicSensor(
            name: "AHU Temperature Sensor",
            minValue: 0,
            maxValue: 100,
            signalType: SignalType.Voltage0to10V
            );
        var valve = new ValveActuator(name: "Cold Water Valve");
        var controller = new TempertureController(setpoint: 22, proportionalGain: 8.0);
        
        var random=new Random();

        Console.WriteLine("\n\n===== Sensor and Actuator Simulation =====");
        Console.WriteLine();

        for (int i =1; i <= 15; i++)
        {
            // Simulate temperatures form 18 to 30ºC
            double simulatedTemperature = 18 + random.NextDouble() * 12;

            // Sensor convert temperature to electrical signal
            double signal = sensor.ConverterToSignal(simulatedTemperature);

            // Controller defines the valve open
            double openCalculated = controller.CalculateOpen(simulatedTemperature);
            valve.DefineOpen(openCalculated);

            Console.WriteLine($"Read {i:00}");
            Console.WriteLine($"Temperature: {simulatedTemperature:F2}ºC");
            Console.WriteLine($"Sensor signal: {signal:F2} {(sensor.SignalType == SignalType.Current4to20mA ? "mA" : "V")}");
            Console.WriteLine(valve);
            Console.WriteLine(new string('-',40));

            Thread.Sleep(500);
        }
        Console.WriteLine("End of Simulation.\n\n");
    }
}