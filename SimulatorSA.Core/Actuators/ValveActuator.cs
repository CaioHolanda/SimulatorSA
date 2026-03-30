using SimulatorSA.Core.Models;

namespace SimulatorSA.Core.Actuators;
public class ValveActuator : Actuator
{
    public double OpeningPercentage { get; private set; }

    public ValveActuator(string name) : base(name)
    {
        OpeningPercentage = 0;
    }

    public void SetOpening(double percentage)
    {
        if (percentage < 0) percentage = 0;
        if (percentage > 100) percentage = 100;

        OpeningPercentage = percentage;
    }

    public override string ToString()
    {
        return $"{Name} - Open: {OpeningPercentage:F1}%";
    }
}
