/*
 Especializa ou encapsula os parâmetros do controlador PID (Proporcional, Integral e Derivativo):
        Kp
        Ki
        Kd
 */
namespace SimulatorSA.Application.Scenarios;

public class PidScenarioDefinition : BaseScenarioDefinition
{
    public double Kp { get; init; } = 8.0;
    public double Ki { get; init; } = 0.15;
    public double Kd { get; init; } = 2.0;
}