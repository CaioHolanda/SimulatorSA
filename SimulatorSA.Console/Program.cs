using SimulatorSA.Core.Enumerators;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Services;
using SimulatorSA.Core.Spaces;

public class Program
{
    public static void Main()
    {

        var room = new OfficeA("Office", 18.0);

        var controller = new PidController(
            setpoint: 22.0,
            proportionalGain: 10.0,
            integralGain: 0.2,
            differentialGain: 1.0,
            timeStep: 1.0);

        double totalSimulationTime = 70.0; // minutes
        double sampleTime = 1.0;           // minutes

        for (double time = 0; time <= totalSimulationTime; time += sampleTime)
        {
            // Example perturbations
            if (time == 5)
                room.TemperatureEffect(SpacePerturbation.PersonIn);

            if (time == 10)
                room.TemperatureEffect(SpacePerturbation.ComputerOn);

            if (time == 15)
            {
                room.TemperatureEffect(SpacePerturbation.PersonIn);
                room.TemperatureEffect(SpacePerturbation.PersonIn);
                room.TemperatureEffect(SpacePerturbation.ComputerOn);
            }

            double valveOpening = controller.CalculateOutput(room.ActualTemperature);
            room.ApplyHeating(valveOpening);
            room.ApplyThermalLoss(15,0.1);

            Console.WriteLine(
                $"Time: {time,4} min | " +
                $"Temp: {room.ActualTemperature,6:F2} °C | " +
                $"Valve: {valveOpening,6:F2} %");
            
            Thread.Sleep(100);
        }
    }
}