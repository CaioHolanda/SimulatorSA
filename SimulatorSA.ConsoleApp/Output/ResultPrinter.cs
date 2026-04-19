/*
Responsável apenas por apresentar:
    resumo do resultado;
    temperatura final;
    métricas;
    talvez séries simplificadas no terminal.
Nada de lógica de domínio aqui.
 */
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.ConsoleApp.Output;

public static class ResultPrinter
{
    public static void Print(SimulationResult result)
    {
        Console.WriteLine("Simulation completed.");

        var temperatureSeries = result.GetSeries(SimulationVariables.Temperature);

        if (temperatureSeries is not null && temperatureSeries.Points.Count > 0)
        {
            var finalTemperature = temperatureSeries.Points.Last().Value;
            Console.WriteLine($"Final temperature: {finalTemperature:F2} °C");
        }

        foreach (var series in result.Series)
        {
            Console.WriteLine($"{series.Name}: {series.Points.Count} points");
        }
    }
}