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


    }
}
