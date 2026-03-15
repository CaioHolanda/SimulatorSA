using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Console.TemporalSeries
{
    public class SimulationPoint
    {
        public double Time { get; set; }
        public double Temperature { get; set; }
        public double Setpoint { get; set; }
        public double ValveOpening { get; set; }
        public double Error { get; set; }
    }
}
