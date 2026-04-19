/*
Responsável por validar:
    se o cenário executa;
    se as séries existem;
    se os resultados são coerentes;
    se diferentes estratégias geram respostas diferentes.
*/
using SimulatorSA.Application.Scenarios;
using SimulatorSA.Core.Constants;

namespace SimulatorSA.Tests.Integration;

public class SimulationScenarioRunnerIntegrationTests
{
    [Fact]
    public void RunBaseScenario_ShouldProduceValidResult()
    {
        var runner = new SimulationScenarioRunner();

        var result = runner.RunBaseScenario();

        Assert.NotNull(result);
        Assert.NotEmpty(result.Series);

        var temperatureSeries = result.GetSeries(SimulationVariables.Temperature);

        Assert.NotNull(temperatureSeries);
        Assert.NotEmpty(temperatureSeries!.Points);
        Assert.Equal(90, temperatureSeries.Points.Count);
    }

    [Fact]
    public void RunBaseScenario_ShouldIncreaseRoomTemperatureInPlausibleWay()
    {
        var runner = new SimulationScenarioRunner();

        var result = runner.RunBaseScenario();

        var temperatureSeries = result.GetSeries(SimulationVariables.Temperature);
        var heatingSeries = result.GetSeries(SimulationVariables.HeatingEffect);

        Assert.NotNull(temperatureSeries);
        Assert.NotNull(heatingSeries);

        Assert.NotEmpty(temperatureSeries!.Points);
        Assert.NotEmpty(heatingSeries!.Points);

        double initialTemperature = 18.0; // mesmo valor definido no cenário base
        double finalTemperature = temperatureSeries.Points.Last().Value;
        double maxTemperature = temperatureSeries.Points.Max(p => p.Value);
        double maxHeatingPower = heatingSeries.Points.Max(p => p.Value);

        Assert.True(finalTemperature > initialTemperature,
            $"Expected final temperature to be greater than initial temperature ({initialTemperature}), but got {finalTemperature:F2}.");

        Assert.True(maxHeatingPower > 0,
            "Expected heating power to be greater than zero during the simulation.");

        Assert.True(maxTemperature < 40.0,
            $"Expected room temperature to remain in a plausible range, but got max {maxTemperature:F2} °C.");
    }
}