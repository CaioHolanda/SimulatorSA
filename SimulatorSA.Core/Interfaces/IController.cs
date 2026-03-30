using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Interfaces
{
    public interface IController
    {
        double Setpoint { get; }
        double CalculateOutput(double measuredValue);
        void Reset();
    }
}
