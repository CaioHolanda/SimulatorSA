using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using SimulatorSA.Core.Interfaces;
using SimulatorSA.Core.Models;
using SimulatorSA.Core.Models.SimulationAnalysis;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;
using SimulatorSA.Core.Spaces;
using System.Globalization;
using SimulatorSA.Application.Scenarios;
using SimulatorSA.Application.Services;
using SimulatorSA.Bacnet.Configuration;
using SimulatorSA.Bacnet.Handlers;
using SimulatorSA.Bacnet.Mapping;
using SimulatorSA.Bacnet.Responses;
using SimulatorSA.Bacnet.Services;
using SimulatorSA.Application.Interfaces;

namespace SimulatorSA.Winforms;

public partial class MainForm : Form
{
    private SimulationResult? _lastPidResult;
    private SimulationResult? _lastOnOffResult;
    private SimulationMetrics? _lastPidMetrics;
    private SimulationMetrics? _lastOnOffMetrics;
    private CurrentSimulationStateStore? _stateStore;
    private CurrentSimulationStateStore? _pidStateStore;
    private CurrentSimulationStateStore? _onOffStateStore;

    private CircularSnapshotBuffer? _pidSnapshotBuffer;
    private CircularSnapshotBuffer? _onOffSnapshotBuffer;

    private SimulationScenarioRunner? _pidScenarioRunner;
    private SimulationScenarioRunner? _onOffScenarioRunner;
    private CancellationTokenSource? _simulationCancellation;
    private System.Windows.Forms.Timer? _livePlotTimer;

    private bool _isSimulationPrepared;
    private bool _isSimulationRunning;
    private bool _isPaused;

    private PidScenarioDefinition? _preparedPidScenario;
    private OnOffScenarioDefinition? _preparedOnOffScenario;

    private BacnetServerService? _bacnetServer;

    public MainForm()
    {
        InitializeComponent();
        LoadDefaultValues();
        ClearResults();

        _livePlotTimer = new System.Windows.Forms.Timer();
        _livePlotTimer.Interval = 100;
        _livePlotTimer.Tick += LivePlotTimer_Tick;
    }
    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void LoadDefaultValues()
    {
        txtRoomName.Text = "Office";
        txtInitialTemp.Text = "18";
        txtOutdoorTemp.Text = "15";
        txtSetpoint.Text = "22";
        txtSteps.Text = "300";
        txtTimeStep.Text = "1";
        txtMaxHeatingPower.Text = "8.0";
        txtThermalCapacity.Text = "1.5";
        txtHeatLossCoefficient.Text = "0.15";
        txtActuatorResponseTime.Text = "10";

        txtKp.Text = "25";
        txtKi.Text = "3";
        txtKd.Text = "0";

        txtPercentageWhenOn.Text = "100";
        txtPercentageWhenOff.Text = "0";
        txtHysteresis.Text = "1.0";
        txtMinOnTime.Text = "1";
        txtMinOffTime.Text = "1";

        btnPlayPause.Text = "Play";
        btnPlayPause.Enabled = false;
    }
    private PidScenarioDefinition CreatePidScenarioFromInputs()
    {
        return new PidScenarioDefinition
        {
            RoomName = txtRoomName.Text.Trim(),
            InitialTemperature = ReadDouble(txtInitialTemp),
            OutdoorTemperature = ReadDouble(txtOutdoorTemp),
            Setpoint = ReadDouble(txtSetpoint),
            TotalSteps = ReadInt(txtSteps),
            DeltaTimeMinutes = ReadDouble(txtTimeStep),
            MaxHeatingPowerKW = ReadDouble(txtMaxHeatingPower),

            ThermalCapacityKWhPerDegree = ReadDouble(txtThermalCapacity),
            HeatLossCoefficientKWPerDegree = ReadDouble(txtHeatLossCoefficient),
            ActuatorResponseTimeMinutes = ReadDouble(txtActuatorResponseTime),

            Kp = ReadDouble(txtKp),
            Ki = ReadDouble(txtKi),
            Kd = ReadDouble(txtKd)
        };
    }
    private OnOffScenarioDefinition CreateOnOffScenarioFromInputs()
    {
        return new OnOffScenarioDefinition
        {
            RoomName = txtRoomName.Text.Trim(),
            InitialTemperature = ReadDouble(txtInitialTemp),
            OutdoorTemperature = ReadDouble(txtOutdoorTemp),
            Setpoint = ReadDouble(txtSetpoint),
            TotalSteps = ReadInt(txtSteps),
            DeltaTimeMinutes = ReadDouble(txtTimeStep),
            MaxHeatingPowerKW = ReadDouble(txtMaxHeatingPower),

            ThermalCapacityKWhPerDegree = ReadDouble(txtThermalCapacity),
            HeatLossCoefficientKWPerDegree = ReadDouble(txtHeatLossCoefficient),
            ActuatorResponseTimeMinutes = ReadDouble(txtActuatorResponseTime),

            OutputWhenOn = ReadDouble(txtPercentageWhenOn),
            OutputWhenOff = ReadDouble(txtPercentageWhenOff),
            Hysteresis = ReadDouble(txtHysteresis),
            MinOnTimeMinutes = ReadDouble(txtMinOnTime),
            MinOffTimeMinutes = ReadDouble(txtMinOffTime)
        };
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
            _simulationCancellation?.Cancel();

            ClearResults();

            _preparedPidScenario = CreatePidScenarioFromInputs();
            _preparedOnOffScenario = CreateOnOffScenarioFromInputs();

            _pidStateStore = new CurrentSimulationStateStore();
            _onOffStateStore = new CurrentSimulationStateStore();

            _pidSnapshotBuffer = new CircularSnapshotBuffer(500);
            _onOffSnapshotBuffer = new CircularSnapshotBuffer(500);

            _pidScenarioRunner = new SimulationScenarioRunner(
                _pidStateStore,
                _pidSnapshotBuffer);

            _onOffScenarioRunner = new SimulationScenarioRunner(
                _onOffStateStore,
                _onOffSnapshotBuffer);

            _pidStateStore.Update(CreateInitialSnapshot(_preparedPidScenario));
            _onOffStateStore.Update(CreateInitialSnapshot(_preparedOnOffScenario));

            StartBacnetServer(_preparedPidScenario, _pidStateStore);

            _simulationCancellation = new CancellationTokenSource();

            _isSimulationPrepared = true;
            _isSimulationRunning = false;
            _isPaused = true;

            btnPlayPause.Text = "Play";
            btnPlayPause.Enabled = true;

            AppendLog("[Simulation] Ready. Connect YABE, subscribe points, then press Play.");
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
    private static SimulationSnapshot CreateInitialSnapshot(BaseScenarioDefinition scenario)
    {
        return new SimulationSnapshot
        {
            RoomTemperature = scenario.InitialTemperature,
            Setpoint = scenario.Setpoint,
            HeatingPowerKW = 0,
            ControllerOutput = 0,
            SequenceNumber = 0,
            SimulatedMinutes = 0
        };
    }
    private void LivePlotTimer_Tick(object? sender, EventArgs e)
    {
        if (_pidSnapshotBuffer is not null)
        {
            var pidSnapshots = _pidSnapshotBuffer.GetAll();

            if (pidSnapshots.Count > 0)
            {
                PlotLivePidTemperature(pidSnapshots);
                PlotLivePidActuator(pidSnapshots);
            }
        }

        if (_onOffSnapshotBuffer is not null)
        {
            var onOffSnapshots = _onOffSnapshotBuffer.GetAll();

            if (onOffSnapshots.Count > 0)
            {
                PlotLiveOnOffTemperature(onOffSnapshots);
                PlotLiveOnOffActuator(onOffSnapshots);
            }
        }
    }
    private void PlotLiveOnOffTemperature(IReadOnlyList<SimulationSnapshot> snapshots)
    {
        double deltaTimeMinutes = ReadDouble(txtTimeStep);

        double[] x = snapshots
            .Select((s, index) => index * deltaTimeMinutes)
            .ToArray();

        double[] temperature = snapshots
            .Select(s => s.RoomTemperature)
            .ToArray();

        double[] setpoint = snapshots
            .Select(s => s.Setpoint)
            .ToArray();

        graphOnOffTemperature.Plot.Clear();
        graphOnOffTemperature.Plot.Add.Scatter(x, temperature);
        graphOnOffTemperature.Plot.Add.Scatter(x, setpoint);

        graphOnOffTemperature.Plot.Title("On-Off Temperature - Live");
        graphOnOffTemperature.Plot.XLabel("Time (min)");
        graphOnOffTemperature.Plot.YLabel("Temperature (°C)");

        graphOnOffTemperature.Plot.Axes.SetLimitsX(0, Math.Max(10, x.Last()));
        graphOnOffTemperature.Plot.Axes.AutoScaleY();

        graphOnOffTemperature.Refresh();
    }
    private void PlotLiveOnOffActuator(IReadOnlyList<SimulationSnapshot> snapshots)
    {
        double deltaTimeMinutes = ReadDouble(txtTimeStep);

        double[] x = snapshots
            .Select((s, index) => index * deltaTimeMinutes)
            .ToArray();

        double[] output = snapshots
            .Select(s => s.ControllerOutput)
            .ToArray();

        graphOnOffActuator.Plot.Clear();
        graphOnOffActuator.Plot.Add.Scatter(x, output);

        graphOnOffActuator.Plot.Title("On-Off Actuator Output - Live");
        graphOnOffActuator.Plot.XLabel("Time (min)");
        graphOnOffActuator.Plot.YLabel("Output (%)");

        graphOnOffActuator.Plot.Axes.SetLimitsX(0, Math.Max(10, x.Last()));
        graphOnOffActuator.Plot.Axes.AutoScaleY();

        graphOnOffActuator.Refresh();
    }
    private void PlotLivePidTemperature(IReadOnlyList<SimulationSnapshot> snapshots)
    {
        double deltaTimeMinutes = ReadDouble(txtTimeStep);

        double[] x = snapshots
            .Select((s, index) => index * deltaTimeMinutes)
            .ToArray();

        double[] temperature = snapshots
            .Select(s => s.RoomTemperature)
            .ToArray();

        double[] setpoint = snapshots
            .Select(s => s.Setpoint)
            .ToArray();

        graphPIDTemperature.Plot.Clear();
        graphPIDTemperature.Plot.Add.Scatter(x, temperature);
        graphPIDTemperature.Plot.Add.Scatter(x, setpoint);

        graphPIDTemperature.Plot.Title("PID Temperature - Live");
        graphPIDTemperature.Plot.XLabel("Time (min)");
        graphPIDTemperature.Plot.YLabel("Temperature (°C)");

        graphPIDTemperature.Plot.Axes.SetLimitsX(0, Math.Max(10, x.Last()));
        graphPIDTemperature.Plot.Axes.AutoScaleY();

        graphPIDTemperature.Refresh();
    }
    private void PlotLivePidActuator(IReadOnlyList<SimulationSnapshot> snapshots)
    {
        double deltaTimeMinutes = ReadDouble(txtTimeStep);

        double[] x = snapshots
            .Select((s, index) => index * deltaTimeMinutes)
            .ToArray();

        double[] output = snapshots
            .Select(s => s.HeatingPowerKW)
            .ToArray();

        graphPIDActuator.Plot.Clear();
        graphPIDActuator.Plot.Add.Scatter(x, output);

        graphPIDActuator.Plot.Title("PID Heating Power - Live");
        graphPIDActuator.Plot.XLabel("Time (min)");
        graphPIDActuator.Plot.YLabel("Heating Power (kW)");

        graphPIDActuator.Plot.Axes.SetLimitsX(0, Math.Max(10, x.Last()));
        graphPIDActuator.Plot.Axes.AutoScaleY();

        graphPIDActuator.Refresh();
    }
    private static Action<Room, int, double> BuildPerturbationScript()
    {
        return (currentRoom, step, time) =>
        {
            if (currentRoom is not OfficeA office)
                return;

            //if (step == 10)
            //    office.SetOccupied(true);

            //if (step == 15)
            //    office.SetComputerOn(true);

            //if (step == 25)
            //    office.SetWindowOpen(true);

            //if (step == 40)
            //    office.SetWindowOpen(false);

            //if (step == 50)
            //    office.SetComputerOn(false);

            //if (step == 60)
            //    office.SetOccupied(false);
        };
    }
    private void UpdatePidSummaryOnly()
    {
        if (_lastPidMetrics is null || _lastPidResult is null)
            return;

        lblPIDFinalTemperature.Text = $"Final Room Temperature: {_lastPidMetrics.FinalTemperature:F2} °C";
        lblPIDActuator.Text = $"Actuator: {_lastPidMetrics.FinalOutput:F2} %";
        lblPIDSteps.Text = $"Steps: {_lastPidResult.Snapshots.Count}";
        lblPIDAbsErrorAverage.Text = $"Abs Error Average: {_lastPidMetrics.MeanAbsoluteError:F3}";
        lblPIDMaxOvershoot.Text = $"Max Overshoot: {_lastPidMetrics.MaxOvershoot:F3} °C";
        lblPIDComfortZone.Text = $"Comfort Zone Permanency: {_lastPidMetrics.ComfortBandPercentage:F1} %";
        lblPIDThermalEnergy.Text = $"Total Thermal Energy Delivered: {_lastPidMetrics.TotalThermalEnergyDelivered:F3} kWh";
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
        _simulationCancellation?.Cancel();
        _simulationCancellation?.Dispose();
        _simulationCancellation = null;

        _livePlotTimer?.Stop();

        _lastPidResult = null;
        _lastOnOffResult = null;
        _lastPidMetrics = null;
        _lastOnOffMetrics = null;

        _pidStateStore = null;
        _onOffStateStore = null;
        _pidSnapshotBuffer = null;
        _onOffSnapshotBuffer = null;
        _pidScenarioRunner = null;
        _onOffScenarioRunner = null;
        _bacnetServer?.Stop();
        _bacnetServer = null;

        AppendLog("[BACnet] Server stopped");
        LoadDefaultValues();
        ClearResults();
        ResetEmptyPlots();

        btnRun.Enabled = true;
    }
    private void ResetEmptyPlots()
    {
        ConfigureEmptyPlot(
            graphPIDTemperature,
            "PID Temperature - Live",
            "Time (min)",
            "Temperature (°C)",
            0, 300,
            15, 25);

        ConfigureEmptyPlot(
            graphPIDActuator,
            "PID Heating Power - Live",
            "Time (min)",
            "Heating Power (kW)",
            0, 300,
            0, 10);

        ConfigureEmptyPlot(
            graphOnOffTemperature,
            "On-Off Temperature - Live",
            "Time (min)",
            "Temperature (°C)",
            0, 300,
            15, 25);

        ConfigureEmptyPlot(
            graphOnOffActuator,
            "On-Off Actuator Output - Live",
            "Time (min)",
            "Output (%)",
            0, 300,
            0, 100);
    }
    private static void ConfigureEmptyPlot(
    ScottPlot.WinForms.FormsPlot plot,
    string title,
    string xLabel,
    string yLabel,
    double xMin,
    double xMax,
    double yMin,
    double yMax)
    {
        plot.Plot.Clear();
        plot.Plot.Title(title);
        plot.Plot.XLabel(xLabel);
        plot.Plot.YLabel(yLabel);
        plot.Plot.Axes.SetLimits(xMin, xMax, yMin, yMax);
        plot.Refresh();
    }
    private void StartBacnetServer(
    PidScenarioDefinition scenario,
    CurrentSimulationStateStore stateStore)
    {
        _bacnetServer?.Stop();

        var configuration = new BacnetDeviceConfiguration
        {
            DeviceInstance = 1234,
            DeviceName = "SimulatorSA BACnet Device",
            VendorName = "SimulatorSA",
            VendorId = 999,
            UdpPort = 47808
        };

        var stateProvider = new CurrentStateSimulationStateProvider(
            stateStore,
            roomName: scenario.RoomName,
            controllerType: "PID",
            outdoorTemperatureProvider: () => scenario.OutdoorTemperature);

        var pointResolver = new BacnetPointResolver();

        var readService = new BacnetValueReadService(
            stateProvider,
            pointResolver);

        var responseService = new BacnetResponseService();

        var whoIsHandler = new WhoIsHandler(configuration);

        var readPropertyHandler = new ReadPropertyHandler(
            configuration,
            readService,
            responseService);

        _bacnetServer = new BacnetServerService(
            configuration,
            whoIsHandler,
            readPropertyHandler);

        _bacnetServer.Start();

        AppendLog("[BACnet] Server started on UDP 47808");
    }
    private void AppendLog(string message)
    {
        if (txtLog.InvokeRequired)
        {
            txtLog.Invoke(() => AppendLog(message));
            return;
        }

        txtLog.AppendText($"{DateTime.Now:HH:mm:ss} {message}{Environment.NewLine}");
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
    private void txtThermalCapacity_TextChanged(object sender, EventArgs e)
    {
    }
    private async Task StartLiveSimulationAsync()
    {
 
        if (_preparedPidScenario is null ||
            _preparedOnOffScenario is null ||
            _pidScenarioRunner is null ||
            _onOffScenarioRunner is null ||
            _simulationCancellation is null)
            return;

        try
        {
            _isSimulationRunning = true;
            _isPaused = false;

            btnPlayPause.Text = "Pause";
            btnRun.Enabled = false;

            AppendLog("[Simulation] Started.");

            _livePlotTimer?.Start();

            var pidTask = _pidScenarioRunner.RunPidScenarioLiveAsync(
                _preparedPidScenario,
                delayMilliseconds: 50,
                cancellationToken: _simulationCancellation.Token,
                isPaused: () => _isPaused);

            var onOffTask = _onOffScenarioRunner.RunOnOffScenarioLiveAsync(
                _preparedOnOffScenario,
                delayMilliseconds: 50,
                cancellationToken: _simulationCancellation.Token,
                isPaused: () => _isPaused);

            await Task.WhenAll(pidTask, onOffTask);

            _lastPidResult = await pidTask;
            _lastOnOffResult = await onOffTask;

            var metricsCalculator = new SimulationMetricsCalculator();
            const double comfortBandHalfWidth = 0.5;

            _lastPidMetrics = metricsCalculator.Calculate(
                _lastPidResult,
                comfortBandHalfWidth,
                _preparedPidScenario.DeltaTimeMinutes);

            _lastOnOffMetrics = metricsCalculator.Calculate(
                _lastOnOffResult,
                comfortBandHalfWidth,
                _preparedOnOffScenario.DeltaTimeMinutes);

            UpdateSummary();

            AppendLog("[Simulation] Completed.");
        }
        catch (OperationCanceledException)
        {
            AppendLog("[Simulation] Cancelled.");
        }
        finally
        {
            _livePlotTimer?.Stop();

            _isSimulationRunning = false;
            btnPlayPause.Text = "Play";
            btnRun.Enabled = true;
        }
    }
    private async void btnPlayPause_Click(object sender, EventArgs e)
    {
        if (!_isSimulationPrepared)
            return;

        if (!_isSimulationRunning)
        {
            await StartLiveSimulationAsync();
            return;
        }

        _isPaused = !_isPaused;

        btnPlayPause.Text = _isPaused ? "Play" : "Pause";

        AppendLog(_isPaused
            ? "[Simulation] Paused."
            : "[Simulation] Resumed.");
    }
}