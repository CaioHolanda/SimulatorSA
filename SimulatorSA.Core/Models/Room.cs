using System;
using System.Collections.Generic;
using System.Text;

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

        public void ApplyHeating(double heatingEffect)
        {
            ActualTemperature += heatingEffect;
        }

        public void ApplyThermalLoss(double outdoorTemperature, double lossCoefficient)
        {
            double thermalLoss = (ActualTemperature - outdoorTemperature) * lossCoefficient;
            ActualTemperature -= thermalLoss;
        }
    }
}
