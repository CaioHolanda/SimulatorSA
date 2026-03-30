using SimulatorSA.Core.Enumerators;
using SimulatorSA.Core.Models;

namespace SimulatorSA.Core.Spaces
{
    public class OfficeA : Room
    {
        public SpacePerturbation Perturbation { get; set; }

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
        public void ApplyPerturbation(SpacePerturbation perturbation)
        {
            ApplyTemperatureDelta(PerturbationEffect[perturbation]);
        }
    }
}
