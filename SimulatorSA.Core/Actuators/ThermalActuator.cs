namespace SimulatorSA.Core.Actuators;

public class ThermalActuator
{
    private double _currentHeatingPowerKW;

    public string Name { get; }
    public double OutputPercentage { get; private set; }
    public double ResponseTimeMinutes { get; set; } = 0;
    public ThermalActuator(string name)
    {
        Name = name;
    }
    public void SetOutput(double outputPercentage)
    {
        OutputPercentage = Math.Clamp(outputPercentage, 0, 100);
    }
    public double CalculateHeatingPowerKW(
        double maxHeatingPowerKW,
        double deltaTimeMinutes)
    {
        double targetHeatingPowerKW =
            maxHeatingPowerKW * OutputPercentage / 100.0;

        if (ResponseTimeMinutes <= 0)
        {
            _currentHeatingPowerKW = targetHeatingPowerKW;
            return _currentHeatingPowerKW;
        }
        double responseFactor = Math.Clamp(
            deltaTimeMinutes / ResponseTimeMinutes,
            0,
            1);

        _currentHeatingPowerKW +=
            (targetHeatingPowerKW - _currentHeatingPowerKW) * responseFactor;

        return _currentHeatingPowerKW;
    }

    public void Reset()
    {
        OutputPercentage = 0;
        _currentHeatingPowerKW = 0;
    }
}