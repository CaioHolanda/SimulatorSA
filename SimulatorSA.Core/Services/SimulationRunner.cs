using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationData;

namespace SimulatorSA.Core.Services
{
    public class SimulationRunner
    {
        public SimulationResult Run(
            Room room,
            IController controller,
            ThermalActuator actuator,
            double outdoorTemperature,
            double maxHeatingPowerKW,
            int totalSteps,
            double deltaTimeMinutes,
            Action<Room, int, double>? stepAction = null)
        {
            ArgumentNullException.ThrowIfNull(room);
            ArgumentNullException.ThrowIfNull(controller);
            ArgumentNullException.ThrowIfNull(actuator);

            if (totalSteps <= 0)
                throw new ArgumentOutOfRangeException(nameof(totalSteps), "Total steps must be greater than zero.");

            if (maxHeatingPowerKW < 0)
                throw new ArgumentOutOfRangeException(nameof(maxHeatingPowerKW), "Maximum heating power cannot be negative.");

            if (deltaTimeMinutes <= 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTimeMinutes), "Delta time must be greater than zero.");

            var result = new SimulationResult();

            var temperatureSeries = CreateSeries(
                SimulationVariables.Temperature,
                "°C",
                "Actual room temperature over time");

            var errorSeries = CreateSeries(
                SimulationVariables.Error,
                "°C",
                "Control error over time");

            var actuatorSeries = CreateSeries(
                SimulationVariables.ValveOpening,
                "%",
                "Actuator output percentage over time");

            var heatingPowerSeries = CreateSeries(
                SimulationVariables.HeatingEffect,
                "kW",
                "Thermal power delivered by the heating system over time");

            var setpointSeries = CreateSeries(
                SimulationVariables.Setpoint,
                "°C",
                "Temperature setpoint over time");

            result.Series.Add(temperatureSeries);
            result.Series.Add(errorSeries);
            result.Series.Add(actuatorSeries);
            result.Series.Add(heatingPowerSeries);
            result.Series.Add(setpointSeries);

            var engine = new SimulationEngine(
                room,
                controller,
                actuator,
                outdoorTemperature,
                maxHeatingPowerKW,
                deltaTimeMinutes,
                stepAction);

            for (int step = 0; step < totalSteps; step++)
            {
                var snapshot = engine.Step();

                result.Snapshots.Add(snapshot);

                AddPoint(temperatureSeries, snapshot.SimulatedMinutes, snapshot.RoomTemperature);
                AddPoint(errorSeries, snapshot.SimulatedMinutes, snapshot.ControlError);
                AddPoint(actuatorSeries, snapshot.SimulatedMinutes, snapshot.ControllerOutput);
                AddPoint(heatingPowerSeries, snapshot.SimulatedMinutes, snapshot.HeatingPowerKW);
                AddPoint(setpointSeries, snapshot.SimulatedMinutes, snapshot.Setpoint);
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