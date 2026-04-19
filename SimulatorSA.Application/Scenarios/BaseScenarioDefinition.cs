/*
Classe que concentra os parâmetros do cenário-base:
    setpoint
    temperatura externa
    duração total
    passo de tempo
    características do ambiente
    ganhos térmicos
    perturbações
Ela não executa nada. Apenas define o cenário
*/
namespace SimulatorSA.Application.Scenarios;

public class BaseScenarioDefinition
{
    public double Setpoint { get; init; } = 22.0;
    public double OutdoorTemperature { get; init; } = 10.0;
    public double DeltaTimeMinutes { get; init; } = 1.0;
    public int TotalSteps { get; init; } = 90;

    public string RoomName { get; init; } = "Room A";
    public double InitialTemperature { get; init; } = 18.0;

    public double HeatLossCoefficientKWPerDegree { get; init; } = 0.08;
    public double ThermalCapacityKWhPerDegree { get; init; } = 2.5;

    public double MaxHeatingPowerKW { get; init; } = 5.0;
    public string ActuatorName { get; init; } = "Heating Coil";
}