using SimulatorSA.Core.Models;

namespace SimulatorSA.Core.Spaces
{
    public class OfficeA : Room
    {
        public bool IsWindowOpen { get; private set; }
        public bool IsComputerOn { get; private set; }
        public bool IsOccupied { get; private set; }

        public OfficeA(string name, double initialTemperature)
            : base(name, initialTemperature, lossCoefficient: 0.005)
        {
        }

        public void SetWindowOpen(bool isOpen)
        {
            IsWindowOpen = isOpen;
        }

        public void SetComputerOn(bool isOn)
        {
            IsComputerOn = isOn;
        }

        public void SetOccupied(bool isOccupied)
        {
            IsOccupied = isOccupied;
        }

        public void ApplyInternalPerturbations(double deltaTime)
        {
            double perturbationRate = 0;

            if (IsWindowOpen)
                perturbationRate += -0.10;

            if (IsComputerOn)
                perturbationRate += 0.04;

            if (IsOccupied)
                perturbationRate += 0.03;

            ApplyHeatingRate(perturbationRate, deltaTime);
        }
    }
}