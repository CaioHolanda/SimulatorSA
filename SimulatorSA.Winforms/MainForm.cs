using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationAnalysis;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;
using SimulatorSA.Core.Spaces;
using System.Globalization;

namespace SimulatorSA.Winforms;

public partial class MainForm : Form
{
    private SimulationResult? _lastPidResult;
    private SimulationResult? _lastOnOffResult;
    private SimulationMetrics? _lastPidMetrics;
    private SimulationMetrics? _lastOnOffMetrics;

    public MainForm()
    {
        InitializeComponent();
        LoadDefaultValues();
        ClearResults();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void LoadDefaultValues()
    {
        txtRoomName.Text = "Office-A01";
        txtInitialTemp.Text = "18";
        txtOutdoorTemp.Text = "10";
        txtSetpoint.Text = "22";
        txtSteps.Text = "70";
        txtTimeStep.Text = "1";
        txtMaxHeatingPower.Text = "2.0";

        txtKp.Text = "10";
        txtKi.Text = "0.2";
        txtKd.Text = "1.0";

        txtPercentageWhenOn.Text = "100";
        txtPercentageWhenOff.Text = "0";
        txtHysteresis.Text = "1.0";
        txtMinOnTime.Text = "1";
        txtMinOffTime.Text = "1";
    }

    private double ReadDouble(TextBox textBox)
    {
        return double.Parse(textBox.Text, CultureInfo.InvariantCulture);
    }

    private int ReadInt(TextBox textBox)
    {
        return int.Parse(textBox.Text, CultureInfo.InvariantCulture);
    }

    private void btnRun_Click(object sender, EventArgs e)
    {
        try
        {
            string roomName = txtRoomName.Text.Trim();
            double initialTemperature = ReadDouble(txtInitialTemp);
            double outdoorTemperature = ReadDouble(txtOutdoorTemp);
            double setpoint = ReadDouble(txtSetpoint);
            int totalSteps = ReadInt(txtSteps);
            double deltaTimeMinutes = ReadDouble(txtTimeStep);
            double maxHeatingPowerKW = ReadDouble(txtMaxHeatingPower);

            IController pidController = new PidController(
                setpoint: setpoint,
                proportionalGain: ReadDouble(txtKp),
                integralGain: ReadDouble(txtKi),
                derivativeGain: ReadDouble(txtKd));

            IController onOffController = new OnOffController(
                setpoint: setpoint,
                outputWhenOn: ReadDouble(txtPercentageWhenOn),
                outputWhenOff: ReadDouble(txtPercentageWhenOff),
                hysteresis: ReadDouble(txtHysteresis),
                minOnTime: ReadDouble(txtMinOnTime),
                minOffTime: ReadDouble(txtMinOffTime));

            _lastPidResult = RunSimulation(
                controller: pidController,
                roomName: roomName,
                initialTemperature: initialTemperature,
                outdoorTemperature: outdoorTemperature,
                maxHeatingPowerKW: maxHeatingPowerKW,
                totalSteps: totalSteps,
                deltaTimeMinutes: deltaTimeMinutes);

            _lastOnOffResult = RunSimulation(
                controller: onOffController,
                roomName: roomName,
                initialTemperature: initialTemperature,
                outdoorTemperature: outdoorTemperature,
                maxHeatingPowerKW: maxHeatingPowerKW,
                totalSteps: totalSteps,
                deltaTimeMinutes: deltaTimeMinutes);

            var metricsCalculator = new SimulationMetricsCalculator();
            const double comfortBandHalfWidth = 0.5;

            _lastPidMetrics = metricsCalculator.Calculate(
                _lastPidResult,
                comfortBandHalfWidth,
                deltaTimeMinutes);

            _lastOnOffMetrics = metricsCalculator.Calculate(
                _lastOnOffResult,
                comfortBandHalfWidth,
                deltaTimeMinutes);

            UpdateSummary();
            PlotAllResults();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Simulation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private SimulationResult RunSimulation(
        IController controller,
        string roomName,
        double initialTemperature,
        double outdoorTemperature,
        double maxHeatingPowerKW,
        int totalSteps,
        double deltaTimeMinutes)
    {
        var room = new OfficeA(roomName, initialTemperature);
        var actuator = new ThermalActuator("Heating Actuator");
        var runner = new SimulationRunner();

        Action<Room, int, double> perturbationScript = BuildPerturbationScript();

        return runner.Run(
            room: room,
            controller: controller,
            actuator: actuator,
            outdoorTemperature: outdoorTemperature,
            maxHeatingPowerKW: maxHeatingPowerKW,
            totalSteps: totalSteps,
            deltaTimeMinutes: deltaTimeMinutes,
            stepAction: perturbationScript);
    }

    private static Action<Room, int, double> BuildPerturbationScript()
    {
        return (currentRoom, step, time) =>
        {
            if (currentRoom is not OfficeA office)
                return;

            if (step == 10)
                office.SetOccupied(true);

            if (step == 15)
                office.SetComputerOn(true);

            if (step == 25)
                office.SetWindowOpen(true);

            if (step == 40)
                office.SetWindowOpen(false);

            if (step == 50)
                office.SetComputerOn(false);

            if (step == 60)
                office.SetOccupied(false);
        };
    }

    private void UpdateSummary()
    {
        if (_lastPidMetrics is null || _lastOnOffMetrics is null ||
            _lastPidResult is null || _lastOnOffResult is null)
            return;

        lblPIDFinalTemperature.Text = $"Final Room Temperature: {_lastPidMetrics.FinalTemperature:F2} °C";
        lblPIDActuator.Text = $"Actuator: {_lastPidMetrics.FinalOutput:F2} %";
        lblPIDSteps.Text = $"Steps: {_lastPidResult.Snapshots.Count}";
        lblPIDAbsErrorAverage.Text = $"Abs Error Average: {_lastPidMetrics.MeanAbsoluteError:F3}";
        lblPIDMaxOvershoot.Text = $"Max Overshoot: {_lastPidMetrics.MaxOvershoot:F3} °C";
        lblPIDComfortZone.Text = $"Comfort Zone Permanency: {_lastPidMetrics.ComfortBandPercentage:F1} %";
        lblPIDThermalEnergy.Text = $"Total Thermal Energy Delivered: {_lastPidMetrics.TotalThermalEnergyDelivered:F3} kWh";

        lblOnOffFinalTemperature.Text = $"Final Room Temperature: {_lastOnOffMetrics.FinalTemperature:F2} °C";
        lblOnOffActuator.Text = $"Actuator: {_lastOnOffMetrics.FinalOutput:F2} %";
        lblOnOffSteps.Text = $"Steps: {_lastOnOffResult.Snapshots.Count}";
        lblOnOffAbsErrorAverage.Text = $"Abs Error Average: {_lastOnOffMetrics.MeanAbsoluteError:F3}";
        lblOnOffMaxOvershoot.Text = $"Max Overshoot: {_lastOnOffMetrics.MaxOvershoot:F3} °C";
        lblOnOffComfortZone.Text = $"Comfort Zone Permanency: {_lastOnOffMetrics.ComfortBandPercentage:F1} %";
        lblOnOffThermalEnergy.Text = $"Total Thermal Energy Delivered: {_lastOnOffMetrics.TotalThermalEnergyDelivered:F3} kWh";
    }

    private void PlotAllResults()
    {
        if (_lastPidResult is null || _lastOnOffResult is null)
            return;

        PlotTemperature(graphPIDTemperature, _lastPidResult, "PID Temperature");
        PlotActuator(graphPIDActuator, _lastPidResult, "PID Actuator Output");

        PlotTemperature(graphOnOffTemperature, _lastOnOffResult, "On-Off Temperature");
        PlotActuator(graphOnOffActuator, _lastOnOffResult, "On-Off Actuator Output");
    }

    private void PlotTemperature(ScottPlot.WinForms.FormsPlot plot, SimulationResult result, string title)
    {
        var temperatureSeries = result.GetSeries(SimulationVariables.Temperature);
        var setpointSeries = result.GetSeries(SimulationVariables.Setpoint);

        if (temperatureSeries is null || setpointSeries is null)
            return;

        double[] tempX = temperatureSeries.Points.Select(p => p.Time).ToArray();
        double[] tempY = temperatureSeries.Points.Select(p => p.Value).ToArray();

        double[] setpointX = setpointSeries.Points.Select(p => p.Time).ToArray();
        double[] setpointY = setpointSeries.Points.Select(p => p.Value).ToArray();

        plot.Plot.Clear();
        plot.Plot.Add.Scatter(tempX, tempY);
        plot.Plot.Add.Scatter(setpointX, setpointY);
        plot.Plot.Title(title);
        plot.Plot.XLabel("Time (min)");
        plot.Plot.YLabel("Temperature (°C)");
        plot.Plot.Axes.AutoScale();
        plot.Refresh();
    }

    private void PlotActuator(ScottPlot.WinForms.FormsPlot plot, SimulationResult result, string title)
    {
        var outputSeries = result.GetSeries(SimulationVariables.ValveOpening);

        if (outputSeries is null)
            return;

        double[] x = outputSeries.Points.Select(p => p.Time).ToArray();
        double[] y = outputSeries.Points.Select(p => p.Value).ToArray();

        plot.Plot.Clear();
        plot.Plot.Add.Scatter(x, y);
        plot.Plot.Title(title);
        plot.Plot.XLabel("Time (min)");
        plot.Plot.YLabel("Output (%)");
        plot.Plot.Axes.AutoScale();
        plot.Refresh();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        _lastPidResult = null;
        _lastOnOffResult = null;
        _lastPidMetrics = null;
        _lastOnOffMetrics = null;

        LoadDefaultValues();
        ClearResults();
    }

    private void ClearResults()
    {
        lblPIDFinalTemperature.Text = "Final Room Temperature: -";
        lblPIDActuator.Text = "Actuator: -";
        lblPIDSteps.Text = "Steps: -";
        lblPIDAbsErrorAverage.Text = "Abs Error Average: -";
        lblPIDMaxOvershoot.Text = "Max Overshoot: -";
        lblPIDComfortZone.Text = "Comfort Zone Permanency: -";
        lblPIDThermalEnergy.Text = "Total Thermal Energy Delivered: -";

        lblOnOffFinalTemperature.Text = "Final Room Temperature: -";
        lblOnOffActuator.Text = "Actuator: -";
        lblOnOffSteps.Text = "Steps: -";
        lblOnOffAbsErrorAverage.Text = "Abs Error Average: -";
        lblOnOffMaxOvershoot.Text = "Max Overshoot: -";
        lblOnOffComfortZone.Text = "Comfort Zone Permanency: -";
        lblOnOffThermalEnergy.Text = "Total Thermal Energy Delivered: -";

        graphPIDTemperature.Plot.Clear();
        graphPIDTemperature.Refresh();

        graphPIDActuator.Plot.Clear();
        graphPIDActuator.Refresh();

        graphOnOffTemperature.Plot.Clear();
        graphOnOffTemperature.Refresh();

        graphOnOffActuator.Plot.Clear();
        graphOnOffActuator.Refresh();
    }

    private void tblParameters_Paint(object sender, PaintEventArgs e)
    {
    }

    private void label2_Click_1(object sender, EventArgs e)
    {
    }

    private void label4_Click(object sender, EventArgs e)
    {
    }

    private void label22_Click(object sender, EventArgs e)
    {
    }
}