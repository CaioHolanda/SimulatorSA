using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Core.Services
{
    public class SimulationRunner
    {
        public SimulationResult Run(
            Room room,
            PidController controller,
            ValveActuator valve,
            double outdoorTemperature,
            double maxHeatingDelta,
            int totalSteps)
        {
            ArgumentNullException.ThrowIfNull(room);
            ArgumentNullException.ThrowIfNull(controller);
            ArgumentNullException.ThrowIfNull(valve);

            if (totalSteps <= 0)
                throw new ArgumentOutOfRangeException(nameof(totalSteps), "Total steps must be greater than zero.");

            if (maxHeatingDelta < 0)
                throw new ArgumentOutOfRangeException(nameof(maxHeatingDelta), "Max heating delta cannot be negative.");

            controller.Reset();

            var result = new SimulationResult();

            var temperatureSeries = CreateSeries(
                SimulationVariables.Temperature,
                "°C",
                "Actual room temperature over time");

            var errorSeries = CreateSeries(
                SimulationVariables.Error,
                "°C",
                "Control error over time");

            var valveSeries = CreateSeries(
                SimulationVariables.ValveOpening,
                "%",
                "Valve opening percentage over time");

            var heatingSeries = CreateSeries(
                SimulationVariables.HeatingEffect,
                "°C",
                "Heating effect applied each step");

            var setpointSeries = CreateSeries(
                SimulationVariables.Setpoint,
                "°C",
                "Temperature setpoint over time");

            result.Series.Add(temperatureSeries);
            result.Series.Add(errorSeries);
            result.Series.Add(valveSeries);
            result.Series.Add(heatingSeries);
            result.Series.Add(setpointSeries);

            for (int step = 0; step < totalSteps; step++)
            {
                double time = step * controller.TimeStep;

                double pidOutput = controller.CalculateOutput(room.ActualTemperature);

                valve.SetOpening(pidOutput);

                double heatingEffect = (valve.OpeningPercentage / 100.0) * maxHeatingDelta;

                room.ApplyTemperatureDelta(heatingEffect);
                room.ApplyThermalLoss(outdoorTemperature);

                double error = controller.Setpoint - room.ActualTemperature;

                var snapshot = new SimulationSnapshot
                {
                    Time = time
                };

                snapshot.Values[SimulationVariables.Temperature] = room.ActualTemperature;
                snapshot.Values[SimulationVariables.Error] = error;
                snapshot.Values[SimulationVariables.ValveOpening] = valve.OpeningPercentage;
                snapshot.Values[SimulationVariables.HeatingEffect] = heatingEffect;
                snapshot.Values[SimulationVariables.Setpoint] = controller.Setpoint;

                result.Snapshots.Add(snapshot);

                AddPoint(temperatureSeries, time, room.ActualTemperature);
                AddPoint(errorSeries, time, error);
                AddPoint(valveSeries, time, valve.OpeningPercentage);
                AddPoint(heatingSeries, time, heatingEffect);
                AddPoint(setpointSeries, time, controller.Setpoint);
            }

            return result;
        }

        private static TimeSeries CreateSeries(string name, string unit, string description)
        {
            return new TimeSeries
            {
                Name = name,
                Unit = unit,
                Description = description
            };
        }

        private static void AddPoint(TimeSeries series, double time, double value)
        {
            series.Points.Add(new TimeValuePoint
            {
                Time = time,
                Value = value
            });
        }
    }
}