using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Console.Controllers
{
    //The function is only cooling
    public class TempertureController
    {
        public double Setpoint { get; }
        public double ProportionalGain { get; }
        public TempertureController(double setpoint, double proportionalGain) 
        {
            Setpoint = setpoint;
            ProportionalGain = proportionalGain;
        }
        public double CalculateOpen(double actualTemperature)
        {
            double error = actualTemperature - Setpoint;
            double output = error * ProportionalGain;

            if (output < 0) output = 0;
            if (output>100) output=100;

            return output;
        }
    }
}
