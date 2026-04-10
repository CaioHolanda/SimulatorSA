using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Models.SimulationAnalysis;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Core.Services
{
    public class SimulationMetricsCalculator
    {
        public SimulationMetrics Calculate(
            SimulationResult result,
            double comfortBandHalfWidth,
            double deltaTimeMinutes)
        {
            ArgumentNullException.ThrowIfNull(result);

            if (deltaTimeMinutes <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTimeMinutes), "Delta time must be greater than zero.");

            if (comfortBandHalfWidth < 0)
                throw new ArgumentOutOfRangeException(nameof(comfortBandHalfWidth), "Comfort band half width cannot be negative.");

            if (result.Snapshots.Count == 0)
                throw new ArgumentException("Simulation result contains no snapshots.", nameof(result));

            var temperatures = result.Snapshots
                .Select(s => s.Values[SimulationVariables.Temperature])
                .ToList();

            var errors = result.Snapshots
                .Select(s => s.Values[SimulationVariables.Error])
                .ToList();

            var outputs = result.Snapshots
                .Select(s => s.Values[SimulationVariables.ValveOpening])
                .ToList();

            var heatingPowersKW = result.Snapshots
                .Select(s => s.Values[SimulationVariables.HeatingEffect])
                .ToList();

            double setpoint = result.Snapshots[0].Values[SimulationVariables.Setpoint];
            double lowerComfortLimit = setpoint - comfortBandHalfWidth;
            double upperComfortLimit = setpoint + comfortBandHalfWidth;

            // Métricas de erro
            double meanAbsoluteError = errors.Average(e => Math.Abs(e));
            double meanSquaredError = errors.Average(e => e * e);
            double maxAbsoluteError = errors.Max(e => Math.Abs(e));

            // Métricas de estabilidade
            double maxTemperature = temperatures.Max();
            double minTemperature = temperatures.Min();
            double temperatureOscillationAmplitude = maxTemperature - minTemperature;

            double maxOvershoot = temperatures.Max(t => Math.Max(0, t - setpoint));
            double maxUndershoot = temperatures.Max(t => Math.Max(0, setpoint - t));

            // Métricas de conforto
            double timeWithinComfortBand = temperatures.Count(t => t >= lowerComfortLimit && t <= upperComfortLimit) * deltaTimeMinutes;
            double totalSimulatedTime = result.Snapshots.Count * deltaTimeMinutes;

            double comfortBandPercentage = totalSimulatedTime > 0
                ? (timeWithinComfortBand / totalSimulatedTime) * 100.0
                : 0.0;

            // Métricas operacionais
            const double onThreshold = 1.0; // %

            bool previousIsOn = outputs[0] >= onThreshold;

            int numberOfTurnOns = previousIsOn ? 1 : 0;
            int numberOfTurnOffs = 0;
            double totalOnTime = previousIsOn ? deltaTimeMinutes : 0.0;

            var onCycles = new List<double>();
            var offCycles = new List<double>();

            double currentCycleDuration = deltaTimeMinutes;

            for (int i = 1; i < outputs.Count; i++)
            {
                bool currentIsOn = outputs[i] >= onThreshold;

                if (currentIsOn == previousIsOn)
                {
                    currentCycleDuration += deltaTimeMinutes;
                }
                else
                {
                    if (previousIsOn)
                        onCycles.Add(currentCycleDuration);
                    else
                        offCycles.Add(currentCycleDuration);

                    if (!previousIsOn && currentIsOn)
                        numberOfTurnOns++;

                    if (previousIsOn && !currentIsOn)
                        numberOfTurnOffs++;

                    currentCycleDuration = deltaTimeMinutes;
                    previousIsOn = currentIsOn;
                }

                if (currentIsOn)
                    totalOnTime += deltaTimeMinutes;
            }

            if (previousIsOn)
                onCycles.Add(currentCycleDuration);
            else
                offCycles.Add(currentCycleDuration);

            double averageOnCycleDuration = onCycles.Count > 0 ? onCycles.Average() : 0.0;
            double averageOffCycleDuration = offCycles.Count > 0 ? offCycles.Average() : 0.0;

            // Métricas energéticas - agora em kWh real
            double deltaTimeHours = deltaTimeMinutes / 60.0;
            double totalThermalEnergyDelivered = heatingPowersKW.Sum(p => p * deltaTimeHours);

            double thermalEnergyPerComfortTime = timeWithinComfortBand > 0
                ? totalThermalEnergyDelivered / timeWithinComfortBand
                : 0.0;

            return new SimulationMetrics
            {
                FinalTemperature = temperatures.Last(),
                FinalOutput = outputs.Last(),

                MeanAbsoluteError = meanAbsoluteError,
                MeanSquaredError = meanSquaredError,
                MaxAbsoluteError = maxAbsoluteError,

                TemperatureOscillationAmplitude = temperatureOscillationAmplitude,
                MaxOvershoot = maxOvershoot,
                MaxUndershoot = maxUndershoot,

                NumberOfTurnOns = numberOfTurnOns,
                NumberOfTurnOffs = numberOfTurnOffs,
                TotalOnTime = totalOnTime,
                AverageOnCycleDuration = averageOnCycleDuration,
                AverageOffCycleDuration = averageOffCycleDuration,

                ComfortBandLowerLimit = lowerComfortLimit,
                ComfortBandUpperLimit = upperComfortLimit,
                TimeWithinComfortBand = timeWithinComfortBand,
                ComfortBandPercentage = comfortBandPercentage,

                TotalThermalEnergyDelivered = totalThermalEnergyDelivered,
                ThermalEnergyPerComfortTime = thermalEnergyPerComfortTime
            };
        }
    }
}