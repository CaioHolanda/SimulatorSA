using SimulatorSA.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Actuators
{
    public class BinaryActuator : Actuator
    {
        public bool IsOn { get; private set; }

        public BinaryActuator(string name) : base(name) { }

        public void TurnOn() => IsOn = true;
        public void TurnOff() => IsOn = false;
    }
}
