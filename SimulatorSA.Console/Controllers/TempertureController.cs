using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Console.Controllers
{

    public class TempertureController
    {
        public double Setpoint { get; }
        public double ProportionalGain { get; }
        public double IntegralGain { get; }
        public double DifferentialGain { get; }
        public double TimeChange { get; set; }
        private double _previousError;
        private double _integralAccumulator;

        public TempertureController(
            double setpoint, 
            double proportionalGain,
            double integralGain, 
            double differentialGain, 
            double timeChange) 
        {
            Setpoint = setpoint;
            ProportionalGain = proportionalGain;
            IntegralGain = integralGain;
            DifferentialGain = differentialGain;
            TimeChange = timeChange;
            _previousError = 0;
            _integralAccumulator = 0;
            
        }
        public double CalculateOpen(double actualTemperature)
        {
            double error = Setpoint - actualTemperature;
            double outputP = error * ProportionalGain;

            _integralAccumulator += error * TimeChange;
            double outputI = _integralAccumulator * IntegralGain;

            double derivative = (error - _previousError) / TimeChange;
            double outputD = derivative * DifferentialGain;

            double outputPID = outputP + outputI + outputD;
            
            if (outputPID < 0) outputPID = 0;
            if (outputPID >100) outputPID =100;

            _previousError = error;

            return outputPID;
        }
    }
}
