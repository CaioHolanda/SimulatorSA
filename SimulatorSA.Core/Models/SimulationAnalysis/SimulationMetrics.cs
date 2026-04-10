namespace SimulatorSA.Core.Models.SimulationAnalysis
{
    public class SimulationMetrics
    {
        public double FinalTemperature { get; set; }
        public double FinalOutput { get; set; }

        public double MeanAbsoluteError { get; set; }
        public double MeanSquaredError { get; set; }
        public double MaxAbsoluteError { get; set; }

        public double TemperatureOscillationAmplitude { get; set; }
        public double MaxOvershoot { get; set; }
        public double MaxUndershoot { get; set; }

        public int NumberOfTurnOns { get; set; }
        public int NumberOfTurnOffs { get; set; }
        public double TotalOnTime { get; set; }
        public double AverageOnCycleDuration { get; set; }
        public double AverageOffCycleDuration { get; set; }

        public double ComfortBandLowerLimit { get; set; }
        public double ComfortBandUpperLimit { get; set; }
        public double TimeWithinComfortBand { get; set; }
        public double ComfortBandPercentage { get; set; }

        public double TotalThermalEnergyDelivered { get; set; }   // kWh
        public double ThermalEnergyPerComfortTime { get; set; }   // kWh/min, na forma atual
    }
}