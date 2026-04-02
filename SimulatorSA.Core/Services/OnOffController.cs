using SimulatorSA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Services;

public class OnOffController : IController
{
    public double Setpoint { get; }
    public double OutputWhenOn { get; }
    public double OutputWhenOff { get; }
    public double Hysteresis{ get; }
    public double MinOnTime { get; }
    public double MinOffTime { get; }

    private bool _isOn;
    private double _timeInCurrentState;

    public bool IsOn => _isOn;
    public OnOffController( double setpoint,
                            double outputWhenOn = 100, 
                            double outputWhenOff = 0, 
                            double hysteresis = 0,
                            double minOnTime =0,
                            double minOffTime=0,
                            bool initialState=false)
    {

        if (hysteresis < 0) throw new ArgumentOutOfRangeException(nameof(hysteresis));
        if (minOnTime < 0) throw new ArgumentOutOfRangeException(nameof(minOnTime));
        if (minOffTime < 0) throw new ArgumentOutOfRangeException(nameof(minOffTime));

        Setpoint = setpoint;
        OutputWhenOn = outputWhenOn;
        OutputWhenOff = outputWhenOff;
        Hysteresis = hysteresis;
        MinOffTime = minOffTime;
        MinOnTime = minOnTime;

        _isOn = initialState;
        _timeInCurrentState = 0;

    }

    public double CalculateOutput(double measuredValue, double deltaTime)
    {
        if (deltaTime < 0)
            throw new ArgumentOutOfRangeException(nameof(deltaTime));
        _timeInCurrentState += deltaTime;
        double lowerLimit = Setpoint - Hysteresis / 2.0;
        double upperLimit = Setpoint + Hysteresis / 2.0;
        if (_isOn)
        {
            bool canTurnOff = _timeInCurrentState >= MinOnTime;
            if(canTurnOff && measuredValue >= upperLimit)
            {
                _isOn = false;
                _timeInCurrentState = 0;
            }
        }
        else
        {
            bool canTurnOn = _timeInCurrentState >= MinOffTime;
            if (canTurnOn && measuredValue <= lowerLimit)
            {
                _isOn = true;
                _timeInCurrentState = 0;
            }
        }
        return _isOn ? OutputWhenOn : OutputWhenOff;
    }

    public void Reset()
    {
        _isOn = false;
        _timeInCurrentState = 0;
    }
}