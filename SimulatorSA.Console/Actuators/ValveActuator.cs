using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Console.Actuators
{
    public class ValveActuator
    {
        public string Name { get;  }
        public double PercentualOpen { get; private set; }
        public ValveActuator(string name) 
        {
            Name = name;
            PercentualOpen = 0;
        }
        public void DefineOpen(double percentual)
        {
            if(percentual <0) percentual = 0;
            if(percentual>100) percentual = 100;
            PercentualOpen=percentual;
        }
        public override string ToString()
        {
            return $"{Name} - Open: {PercentualOpen:F1}%";
        }
    }
}
