using SimulatorSA.Application.Scenarios;
using SimulatorSA.ConsoleApp.Output;

namespace SimulatorSA.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            var runner = new SimulationScenarioRunner();
            var result = runner.RunBaseScenario();

            ResultPrinter.Print(result);
        }
    }
}