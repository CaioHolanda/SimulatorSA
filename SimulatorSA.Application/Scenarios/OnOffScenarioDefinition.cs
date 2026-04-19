/*
 Especializa ou encapsula os parâmetros do controlador On-Off:
    histerese
    tempos mínimos ligado/desligado
    saídas on/off
*/
namespace SimulatorSA.Application.Scenarios;

public class OnOffScenarioDefinition : BaseScenarioDefinition
{
    public double OutputWhenOn { get; init; } = 100.0;
    public double OutputWhenOff { get; init; } = 0.0;
    public double Hysteresis { get; init; } = 0.5;
    public double MinOnTimeMinutes { get; init; } = 3.0;
    public double MinOffTimeMinutes { get; init; } = 3.0;
}