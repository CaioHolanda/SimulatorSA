using SimulatorSA.Core.Actuators;
using SimulatorSA.Core.Constants;
using System.Linq;
using SimulatorSA.Core.Models.SimulationData;
using SimulatorSA.Core.Services;
using SimulatorSA.Core.Spaces;

namespace SimulatorSA.Winforms;

public partial class MainForm : Form
{
    private SimulationResult? _lastResult;
    public MainForm()
    {
        InitializeComponent();
        LoadDefaultValues();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label7_Click(object sender, EventArgs e)
    {

    }
    private void LoadDefaultValues()
    {
        txtRoomName.Text = "Office A";
        txtInitialTemp.Text = "18";
        txtOutdoorTemp.Text = "10";
        txtSetpoint.Text = "22";
        txtKp.Text = "10";
        txtKi.Text = "0.5";
        txtKd.Text = "0.1";
        txtTimeStep.Text = "1";
        txtSteps.Text = "70";
        txtHeatingDelta.Text = "0.2";
    }
    private double ReadDouble(TextBox textBox)
    {
        return double.Parse(textBox.Text);
    }

    private int ReadInt(TextBox textBox)
    {
        return int.Parse(textBox.Text);
    }

    private void btnRun_Click(object sender, EventArgs e)
    {
         try
        {
            var room = new OfficeA(
                txtRoomName.Text,
                ReadDouble(txtInitialTemp));

            var controller = new PidController(
                setpoint: ReadDouble(txtSetpoint),
                proportionalGain: ReadDouble(txtKp),
                integralGain: ReadDouble(txtKi),
                differentialGain: ReadDouble(txtKd),
                timeStep: ReadDouble(txtTimeStep));

            var valve = new ValveActuator("Heating Valve");

            var runner = new SimulationRunner();

            _lastResult = runner.Run(
                room: room,
                controller: controller,
                valve: valve,
                outdoorTemperature: ReadDouble(txtOutdoorTemp),
                maxHeatingDelta: ReadDouble(txtHeatingDelta),
                totalSteps: ReadInt(txtSteps));

            if (_lastResult is not null)
            {
                UpdateSummary(_lastResult);
                LoadGrid(_lastResult);
            }
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
    private void UpdateSummary(SimulationResult result)
    {
        var last = result.Snapshots.Last();

        lblFinalTemp.Text = $"Temp: {last.Values[SimulationVariables.Temperature]:F2} °C";
        lblFinalError.Text = $"Error: {last.Values[SimulationVariables.Error]:F2}";
        lblFinalValve.Text = $"Valve: {last.Values[SimulationVariables.ValveOpening]:F2} %";
        lblSteps.Text = $"Steps: {result.Snapshots.Count}";
    }
    private void LoadGrid(SimulationResult result)
    {
        var rows = result.Snapshots.Select(snapshot => new
        {
            Time = snapshot.Time,
            Temperature = snapshot.Values[SimulationVariables.Temperature],
            Error = snapshot.Values[SimulationVariables.Error],
            ValveOpening = snapshot.Values[SimulationVariables.ValveOpening],
            HeatingEffect = snapshot.Values[SimulationVariables.HeatingEffect],
            Setpoint = snapshot.Values[SimulationVariables.Setpoint]
        }).ToList();

        gridResults.DataSource = null;
        gridResults.DataSource = rows;

        gridResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridResults.ReadOnly = true;
        gridResults.AllowUserToAddRows = false;
        gridResults.AllowUserToDeleteRows = false;
        gridResults.AllowUserToResizeRows = false;
        gridResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridResults.MultiSelect = false;
    }
}
