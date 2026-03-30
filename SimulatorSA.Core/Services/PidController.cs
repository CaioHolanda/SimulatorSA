using SimulatorSA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Services
{

    public class PidController:IController
    {
        public double Setpoint { get; }
        public double Kp { get; }
        public double Ki { get; }
        public double Kd { get; }
        public double TimeStep { get; }
        private double _previousError;
        private double _integralAccumulator;

        public PidController(
            double setpoint, 
            double proportionalGain,
            double integralGain, 
            double differentialGain, 
            double timeStep) 
        {
            Setpoint = setpoint;
            Kp = proportionalGain;
            Ki = integralGain;
            Kd = differentialGain;
            TimeStep = timeStep;

            Reset();
            
        }
        public double CalculateOutput(double actualTemperature)
        {
            double error = Setpoint - actualTemperature;
            double outputP = error * Kp;

            _integralAccumulator += error * TimeStep;
            double outputI = _integralAccumulator * Ki;

            double derivative = (error - _previousError) / TimeStep;
            double outputD = derivative * Kd;

            double outputPID = outputP + outputI + outputD;
            
            _previousError = error;

            return outputPID;
        }

        public void Reset()
        {
            _previousError = 0;
            _integralAccumulator = 0;
        }
    }
}
