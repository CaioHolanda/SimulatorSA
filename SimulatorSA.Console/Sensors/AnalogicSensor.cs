using SimulatorSA.Console.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Console.Sensors
{
    public class AnalogicSensor
    {
        public string Name { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public SinalType SinalType { get; set; }

        public AnalogicSensor(string name, double minValue, double maxValue, SinalType sinalType)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            SinalType = sinalType;
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
            return SinalType switch
            {

            }
        }
    }
}
