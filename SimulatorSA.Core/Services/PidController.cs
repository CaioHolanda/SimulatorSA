using SimulatorSA.Core.Interfaces;

namespace SimulatorSA.Core.Services
{

    public class PidController:IController
    {
        public double Setpoint { get; }
        public double Kp { get; }
        public double Ki { get; }
        public double Kd { get; }

        private double _previousError;
        private double _integralAccumulator;
        private bool _isFirstRun;

        public PidController(
            double setpoint, 
            double proportionalGain,
            double integralGain, 
            double derivativeGain) 
        {
            Setpoint = setpoint;
            Kp = proportionalGain;
            Ki = integralGain;
            Kd = derivativeGain;

            Reset();
            
        }
        public double CalculateOutput(double measuredValue, double deltaTime)
        {
            if (deltaTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "deltaTime must be greater than zero.");

            double error = Setpoint - measuredValue;

            double outputP = error * Kp;

            _integralAccumulator += error * deltaTime;
            double outputI = _integralAccumulator * Ki;

            double derivative = 0;

            if (!_isFirstRun)
            {
                derivative = (error - _previousError) / deltaTime;
            }

            double outputD = derivative * Kd;

            double output = outputP + outputI + outputD;

            _previousError = error;
            _isFirstRun = false;

            return output;
        }
        public void Reset()
        {
            _previousError = 0;
            _integralAccumulator = 0;
            _isFirstRun = true;
        }
    }
}
