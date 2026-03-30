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

        public void ApplyTemperatureDelta(double temperatureDelta)
        {
            ActualTemperature += temperatureDelta;
        }
        public void ApplyThermalLoss(double outdoorTemperature)
        {
            double thermalLoss = (ActualTemperature - outdoorTemperature) * LossCoefficient;
            ActualTemperature -= thermalLoss;
        }
    }
}
