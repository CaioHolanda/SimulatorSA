using SimulatorSA.Core.Actuators;
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
        var valve = new ValveActuator("Heating Valve");

        double totalSimulationTime = 70.0; // minutes
        double sampleTime = 1.0;           // minutes
        double outdoorTemperature = 15.0;
        double maxHeatingPower = 0.4;



        for (double time = 0; time <= totalSimulationTime; time += sampleTime)
        {
            // Example perturbations
            if (time == 5)
                room.ApplyPerturbation(SpacePerturbation.PersonIn);

            if (time == 10)
                room.ApplyPerturbation(SpacePerturbation.ComputerOn);

            if (time == 15)
            {
                room.ApplyPerturbation(SpacePerturbation.PersonIn);
                room.ApplyPerturbation(SpacePerturbation.PersonIn);
                room.ApplyPerturbation(SpacePerturbation.ComputerOn);
            }

            double valveOpening = controller.CalculateOutput(room.ActualTemperature);
            if (valveOpening < 0) valveOpening = 0;
            if (valveOpening > 100) valveOpening = 100;

            double heatingEffect = (valveOpening / 100.0) * maxHeatingPower;

            room.ApplyTemperatureDelta(heatingEffect);
            room.ApplyThermalLoss(outdoorTemperature);

            Console.WriteLine(
                $"Time: {time,4} min | " +
                $"Temp: {room.ActualTemperature,6:F2} °C | " +
                $"Valve: {valveOpening,6:F2} % | " +
                $"Heat: {heatingEffect,6:F4} °C");

            Thread.Sleep(100);
        }
    }
}