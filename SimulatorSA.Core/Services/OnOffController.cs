using SimulatorSA.Core.Interfaces;
using System;

namespace SimulatorSA.Core.Services
{
    public class OnOffController : IController
    {
        public double Setpoint { get; }
        public double OutputWhenOn { get; }
        public double OutputWhenOff { get; }
        public double Hysteresis { get; }
        public double MinOnTime { get; }
        public double MinOffTime { get; }

        private readonly bool _initialState;
        private bool _isOn;
        private double _timeInCurrentState;

        public bool IsOn => _isOn;

        public OnOffController(
            double setpoint,
            double outputWhenOn = 100,
            double outputWhenOff = 0,
            double hysteresis = 0,
            double minOnTime = 0,
            double minOffTime = 0,
            bool initialState = false)
        {
            if (hysteresis < 0)
                throw new ArgumentOutOfRangeException(nameof(hysteresis));

            if (minOnTime < 0)
                throw new ArgumentOutOfRangeException(nameof(minOnTime));

            if (minOffTime < 0)
                throw new ArgumentOutOfRangeException(nameof(minOffTime));

            if (outputWhenOn < 0 || outputWhenOn > 100)
                throw new ArgumentOutOfRangeException(nameof(outputWhenOn));

            if (outputWhenOff < 0 || outputWhenOff > 100)
                throw new ArgumentOutOfRangeException(nameof(outputWhenOff));

            if (outputWhenOff > outputWhenOn)
                throw new ArgumentException("OutputWhenOff cannot be greater than OutputWhenOn.");

            Setpoint = setpoint;
            OutputWhenOn = outputWhenOn;
            OutputWhenOff = outputWhenOff;
            Hysteresis = hysteresis;
            MinOnTime = minOnTime;
            MinOffTime = minOffTime;

            _initialState = initialState;
            _isOn = initialState;
            _timeInCurrentState = 0;
        }

        public double CalculateOutput(double measuredValue, double deltaTime)
        {
            if (deltaTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "deltaTime must be greater than zero.");

            _timeInCurrentState += deltaTime;

            double turnOnThreshold = Setpoint - Hysteresis / 2.0;
            double turnOffThreshold = Setpoint + Hysteresis / 2.0;

            if (_isOn)
            {
                bool canTurnOff = _timeInCurrentState >= MinOnTime;

                if (canTurnOff && measuredValue >= turnOffThreshold)
                {
                    _isOn = false;
                    _timeInCurrentState = 0;
                }
            }
            else
            {
                bool canTurnOn = _timeInCurrentState >= MinOffTime;

                if (canTurnOn && measuredValue <= turnOnThreshold)
                {
                    _isOn = true;
                    _timeInCurrentState = 0;
                }
            }

            return _isOn ? OutputWhenOn : OutputWhenOff;
        }

        public void Reset()
        {
            _isOn = _initialState;
            _timeInCurrentState = 0;
        }
    }
}