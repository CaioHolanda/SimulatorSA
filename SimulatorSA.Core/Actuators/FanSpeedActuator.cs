using SimulatorSA.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Actuators
{
    public class FanSpeedActuator : Actuator
    {
        public double SpeedPercentage { get; private set; }

        public FanSpeedActuator(string name) : base(name) { }

        public void SetSpeed(double percentage)
        {
            if (percentage < 0) percentage = 0;
            if (percentage > 100) percentage = 100;

            SpeedPercentage = percentage;
        }
    }
}
