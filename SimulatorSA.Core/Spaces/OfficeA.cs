using SimulatorSA.Core.Enumerators;
using SimulatorSA.Core.Models;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Spaces
{
    public class OfficeA : Room
    {
        public SpacePerturbation Pertubation { get; set; }

        private static readonly Dictionary<SpacePerturbation, double> PerturbationEffect =
        new()
        {
            { SpacePerturbation.DoorOpen,       -0.5 },
            { SpacePerturbation.DoorClose,       0.5 },
            { SpacePerturbation.WindowOpen,     -2   },
            { SpacePerturbation.WindowClose,     2   },
            { SpacePerturbation.ComputerOn,      1   },
            { SpacePerturbation.ComputerOff,    -1   },
            { SpacePerturbation.PersonIn,        0.5 },
            { SpacePerturbation.PersonOut,      -0.5 }
        };
        public OfficeA(string name, double initialTemperature):base(name, initialTemperature,lossCoefficient:0.005)
        {
        }
        public void TemperatureEffect(SpacePerturbation pertubation)
        {
            ActualTemperature += PerturbationEffect[pertubation];
        }
        public void ApplyHeating(double valveOpening)
        {
            double maxHeatingPower = 0.4;
            double heatingEffect = (valveOpening / 100.0) * maxHeatingPower;
            ActualTemperature += heatingEffect;
            System.Console.Write($"Heating effect: {heatingEffect:F4} °C --> ");
        }
        public void ApplyThermalLoss(double outdoorTemperature)
        {
            double lossCoefficient = 0.005;
            double thermalLoss = (ActualTemperature - outdoorTemperature) * lossCoefficient;

            ActualTemperature -= thermalLoss;
        }
    }
}
