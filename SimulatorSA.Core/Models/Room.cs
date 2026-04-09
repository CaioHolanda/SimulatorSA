using System;

namespace SimulatorSA.Core.Models
{
    public class Room
    {
        public string Name { get; }
        public double ActualTemperature { get; protected set; }
        public double LossCoefficient { get; }

        public Room(string name, double initialTemperature, double lossCoefficient)
        {
            Name = name;
            ActualTemperature = initialTemperature;
            LossCoefficient = lossCoefficient;
        }

        public void ApplyHeatingRate(double temperatureRate, double deltaTime)
        {
            if (deltaTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time must be greater than zero.");

            ActualTemperature += temperatureRate * deltaTime;
        }

        public void ApplyThermalLoss(double outdoorTemperature, double deltaTime)
        {
            if (deltaTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time must be greater than zero.");

            double thermalLoss = (ActualTemperature - outdoorTemperature) * LossCoefficient * deltaTime;
            ActualTemperature -= thermalLoss;
        }
    }
}