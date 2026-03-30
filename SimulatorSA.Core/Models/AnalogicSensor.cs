using SimulatorSA.Core.Enumerators;


namespace SimulatorSA.Core.Models
{
    public class AnalogicSensor
    {
        public string Name { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public SignalType SignalType { get; set; }

        public AnalogicSensor(string name, double minValue, double maxValue, SignalType signalType)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            SignalType = signalType;
        }

        public double LimitValue(double value)
        {
            if (value < MinValue) return MinValue;
            if (value > MaxValue) return MaxValue;
            return value;
        }

        public double ConverterToSignal(double physicalValue)
        {
            physicalValue = LimitValue(physicalValue);
            double percentual = (physicalValue - MinValue) / (MaxValue - MinValue);
            return SignalType switch
            {
                SignalType.Current4to20mA => 4.0 + (percentual * 16.0),
                SignalType.Voltage0to10V => percentual * 10.0,
                _ => throw new InvalidOperationException($"Unsupported signal type: {SignalType}")
            };
        }
        public double ConverterSignalToPhysicalValue(double signal)
        {
            return SignalType switch
            {
                SignalType.Current4to20mA => MinValue + ((signal - 4.0) / 16) * (MaxValue - MinValue),
                SignalType.Voltage0to10V => MinValue + (signal / 10) * (MaxValue - MinValue),
                _ => throw new InvalidOperationException($"Unsupported signal type: {SignalType}")
            };
        }
    }
}
