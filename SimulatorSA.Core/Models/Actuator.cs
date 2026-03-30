using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Core.Models;

public abstract class Actuator
{
    public string Name { get; }

    protected Actuator(string name) // obriga a heranca
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Actuator name cannot be empty.");
        Name = name;
    }

    public override string ToString() => Name;
}
