using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Spaces;

namespace SimulatorSA.Core.Services
{
    public class SimulationRunner
    {
        public SimulationResult Run(
            Room room,
            IController controller,
            ValveActuator valve,
            double outdoorTemperature,
            double maxHeatingRate,
            int totalSteps,
            double deltaTime,
            Action<Room, int, double>? stepAction = null)
        {
            ArgumentNullException.ThrowIfNull(room);
            ArgumentNullException.ThrowIfNull(controller);
            ArgumentNullException.ThrowIfNull(valve);

            if (totalSteps <= 0)
                throw new ArgumentOutOfRangeException(nameof(totalSteps), "Total steps must be greater than zero.");

            if (maxHeatingRate < 0)
                throw new ArgumentOutOfRangeException(nameof(maxHeatingRate), "Max heating rate cannot be negative.");

            if (deltaTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Delta time must be greater than zero.");

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
                double time = step * deltaTime;

                stepAction?.Invoke(room, step, time);

                if (room is OfficeA office)
                {
                    office.ApplyInternalPerturbations(deltaTime);
                }

                double controllerOutput = controller.CalculateOutput(room.ActualTemperature, deltaTime);

                valve.SetOpening(controllerOutput);

                double heatingEffect = (valve.OpeningPercentage / 100.0) * maxHeatingRate;

                room.ApplyHeatingRate(heatingEffect, deltaTime);
                room.ApplyThermalLoss(outdoorTemperature, deltaTime);

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