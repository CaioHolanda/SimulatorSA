using System;

namespace SimulatorSA.Core.Models
{
    public class Room
    {
        public string Name { get; }
        public double ActualTemperature { get; protected set; }

        // UA simplificado do ambiente
        public double HeatLossCoefficientKWPerDegree { get; }

        // Capacidade térmica efetiva do ambiente
        public double ThermalCapacityKWhPerDegree { get; }

        public Room(
            string name,
            double initialTemperature,
            double heatLossCoefficientKWPerDegree,
            double thermalCapacityKWhPerDegree)
        {
            if (thermalCapacityKWhPerDegree <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(thermalCapacityKWhPerDegree),
                    "Thermal capacity must be greater than zero.");

            if (heatLossCoefficientKWPerDegree < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(heatLossCoefficientKWPerDegree),
                    "Heat loss coefficient cannot be negative.");

            Name = name;
            ActualTemperature = initialTemperature;
            HeatLossCoefficientKWPerDegree = heatLossCoefficientKWPerDegree;
            ThermalCapacityKWhPerDegree = thermalCapacityKWhPerDegree;
        }

        public void ApplyThermalPower(double thermalPowerKW, double deltaTimeMinutes)
        {
            if (deltaTimeMinutes <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(deltaTimeMinutes),
                    "Delta time must be greater than zero.");

            double deltaTimeHours = deltaTimeMinutes / 60.0;
            double transferredEnergyKWh = thermalPowerKW * deltaTimeHours;
            double deltaTemperature = transferredEnergyKWh / ThermalCapacityKWhPerDegree;

            ActualTemperature += deltaTemperature;
        }

        public double CalculateEnvelopeThermalPowerKW(double outdoorTemperature)
        {
            return HeatLossCoefficientKWPerDegree * (outdoorTemperature - ActualTemperature);
        }

        public void ApplyEnvelopeHeatExchange(double outdoorTemperature, double deltaTimeMinutes)
        {
            double envelopeThermalPowerKW = CalculateEnvelopeThermalPowerKW(outdoorTemperature);
            ApplyThermalPower(envelopeThermalPowerKW, deltaTimeMinutes);
        }
    }
}