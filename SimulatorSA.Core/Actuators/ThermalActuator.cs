namespace SimulatorSA.Core.Actuators
{
    public class ThermalActuator
    {
        public string Name { get; }
        public double OutputPercentage { get; private set; }

        public ThermalActuator(string name)
        {
            Name = name;
            OutputPercentage = 0;
        }

        public void SetOutput(double outputPercentage)
        {
            if (double.IsNaN(outputPercentage) || double.IsInfinity(outputPercentage))
                throw new ArgumentOutOfRangeException(nameof(outputPercentage), "Output must be a valid number.");

            OutputPercentage = Math.Clamp(outputPercentage, 0.0, 100.0);
        }

        public double GetThermalPowerKW(double maxThermalPowerKW)
        {
            if (maxThermalPowerKW < 0)
                throw new ArgumentOutOfRangeException(nameof(maxThermalPowerKW), "Maximum thermal power cannot be negative.");

            return (OutputPercentage / 100.0) * maxThermalPowerKW;
        }
    }
}