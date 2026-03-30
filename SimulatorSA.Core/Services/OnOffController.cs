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

    public OnOffController(double setpoint, double outputWhenOn = 100, double outputWhenOff = 0)
    {
        Setpoint = setpoint;
        OutputWhenOn = outputWhenOn;
        OutputWhenOff = outputWhenOff;
    }

    public double CalculateOutput(double measuredValue)
    {
        return measuredValue < Setpoint ? OutputWhenOn : OutputWhenOff;
    }

    public void Reset()
    {
    }
}