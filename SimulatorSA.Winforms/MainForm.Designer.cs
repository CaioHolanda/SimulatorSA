namespace SimulatorSA.Winforms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        splitContainer1 = new SplitContainer();
        grpParameters = new GroupBox();
        tblParameters = new TableLayoutPanel();
        txtMinOffTime = new TextBox();
        txtMinOnTime = new TextBox();
        txtHysteresis = new TextBox();
        txtPercentageWhenOff = new TextBox();
        txtPercentageWhenOn = new TextBox();
        lblMinOffTime = new Label();
        lblMinOnTime = new Label();
        lblHysteresis = new Label();
        lblPercentageWhenOff = new Label();
        lblPercentageWhenOn = new Label();
        lblOnOffTitle = new Label();
        lblPIDTitle = new Label();
        lblHeatingRate = new Label();
        lblCommonTitle = new Label();
        txtSteps = new TextBox();
        lblTotalSteps = new Label();
        txtMaxHeatingPower = new TextBox();
        txtTimeStep = new TextBox();
        txtKd = new TextBox();
        txtKi = new TextBox();
        txtKp = new TextBox();
        txtSetpoint = new TextBox();
        txtOutdoorTemp = new TextBox();
        txtInitialTemp = new TextBox();
        lblInitialTemp = new Label();
        lblRoomName = new Label();
        txtRoomName = new TextBox();
        lblOutdoorTemp = new Label();
        lblSetpoint = new Label();
        lblKp = new Label();
        lblKi = new Label();
        lblKd = new Label();
        lblTimeStep = new Label();
        btnRun = new Button();
        btnReset = new Button();
        grpResults = new GroupBox();
        tblResults = new TableLayoutPanel();
        OnOffResultGroup = new GroupBox();
        lblOnOffMetricsTitle = new Label();
        lblOnOffActuator = new Label();
        lblOnOffThermalEnergy = new Label();
        lblOnOffComfortZone = new Label();
        lblOnOffMaxOvershoot = new Label();
        lblOnOffAbsErrorAverage = new Label();
        lblOnOffSteps = new Label();
        lblOnOffFinalTemperature = new Label();
        PIDResultGroup = new GroupBox();
        lblPIDMetricsTitle = new Label();
        lblPIDActuator = new Label();
        lblPIDThermalEnergy = new Label();
        lblPIDComfortZone = new Label();
        lblPIDMaxOvershoot = new Label();
        lblPIDAbsErrorAverage = new Label();
        lblPIDSteps = new Label();
        lblPIDFinalTemperature = new Label();
        OnOffGraphsGroup = new TableLayoutPanel();
        graphOnOffActuator = new ScottPlot.WinForms.FormsPlot();
        graphOnOffTemperature = new ScottPlot.WinForms.FormsPlot();
        PIDGraphsGroup = new TableLayoutPanel();
        graphPIDActuator = new ScottPlot.WinForms.FormsPlot();
        graphPIDTemperature = new ScottPlot.WinForms.FormsPlot();
        saveFileDialog1 = new SaveFileDialog();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        grpParameters.SuspendLayout();
        tblParameters.SuspendLayout();
        grpResults.SuspendLayout();
        tblResults.SuspendLayout();
        OnOffResultGroup.SuspendLayout();
        PIDResultGroup.SuspendLayout();
        OnOffGraphsGroup.SuspendLayout();
        PIDGraphsGroup.SuspendLayout();
        SuspendLayout();
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = DockStyle.Fill;
        splitContainer1.Location = new Point(0, 0);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(grpParameters);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(grpResults);
        splitContainer1.Size = new Size(1311, 832);
        splitContainer1.SplitterDistance = 315;
        splitContainer1.TabIndex = 0;
        // 
        // grpParameters
        // 
        grpParameters.Controls.Add(tblParameters);
        grpParameters.Dock = DockStyle.Fill;
        grpParameters.Font = new Font("Segoe UI", 9F);
        grpParameters.Location = new Point(0, 0);
        grpParameters.Name = "grpParameters";
        grpParameters.Size = new Size(315, 832);
        grpParameters.TabIndex = 1;
        grpParameters.TabStop = false;
        grpParameters.Text = "Simulation Parameters";
        // 
        // tblParameters
        // 
        tblParameters.ColumnCount = 2;
        tblParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56.554306F));
        tblParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43.445694F));
        tblParameters.Controls.Add(txtMinOffTime, 1, 17);
        tblParameters.Controls.Add(txtMinOnTime, 1, 16);
        tblParameters.Controls.Add(txtHysteresis, 1, 15);
        tblParameters.Controls.Add(txtPercentageWhenOff, 1, 14);
        tblParameters.Controls.Add(txtPercentageWhenOn, 1, 13);
        tblParameters.Controls.Add(lblMinOffTime, 0, 17);
        tblParameters.Controls.Add(lblMinOnTime, 0, 16);
        tblParameters.Controls.Add(lblHysteresis, 0, 15);
        tblParameters.Controls.Add(lblPercentageWhenOff, 0, 14);
        tblParameters.Controls.Add(lblPercentageWhenOn, 0, 13);
        tblParameters.Controls.Add(lblOnOffTitle, 0, 12);
        tblParameters.Controls.Add(lblPIDTitle, 0, 8);
        tblParameters.Controls.Add(lblHeatingRate, 0, 7);
        tblParameters.Controls.Add(lblCommonTitle, 0, 0);
        tblParameters.Controls.Add(txtSteps, 1, 5);
        tblParameters.Controls.Add(lblTotalSteps, 0, 5);
        tblParameters.Controls.Add(txtMaxHeatingPower, 1, 7);
        tblParameters.Controls.Add(txtTimeStep, 1, 6);
        tblParameters.Controls.Add(txtKd, 1, 11);
        tblParameters.Controls.Add(txtKi, 1, 10);
        tblParameters.Controls.Add(txtKp, 1, 9);
        tblParameters.Controls.Add(txtSetpoint, 1, 4);
        tblParameters.Controls.Add(txtOutdoorTemp, 1, 3);
        tblParameters.Controls.Add(txtInitialTemp, 1, 2);
        tblParameters.Controls.Add(lblInitialTemp, 0, 2);
        tblParameters.Controls.Add(lblRoomName, 0, 1);
        tblParameters.Controls.Add(txtRoomName, 1, 1);
        tblParameters.Controls.Add(lblOutdoorTemp, 0, 3);
        tblParameters.Controls.Add(lblSetpoint, 0, 4);
        tblParameters.Controls.Add(lblKp, 0, 9);
        tblParameters.Controls.Add(lblKi, 0, 10);
        tblParameters.Controls.Add(lblKd, 0, 11);
        tblParameters.Controls.Add(lblTimeStep, 0, 6);
        tblParameters.Controls.Add(btnRun, 0, 18);
        tblParameters.Controls.Add(btnReset, 1, 18);
        tblParameters.Dock = DockStyle.Fill;
        tblParameters.Location = new Point(3, 19);
        tblParameters.Name = "tblParameters";
        tblParameters.RowCount = 19;
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.RowStyles.Add(new RowStyle());
        tblParameters.Size = new Size(309, 810);
        tblParameters.TabIndex = 0;
        tblParameters.Paint += tblParameters_Paint;
        // 
        // txtMinOffTime
        // 
        txtMinOffTime.Dock = DockStyle.Fill;
        txtMinOffTime.Location = new Point(177, 516);
        txtMinOffTime.Name = "txtMinOffTime";
        txtMinOffTime.Size = new Size(129, 23);
        txtMinOffTime.TabIndex = 40;
        // 
        // txtMinOnTime
        // 
        txtMinOnTime.Dock = DockStyle.Fill;
        txtMinOnTime.Location = new Point(177, 487);
        txtMinOnTime.Name = "txtMinOnTime";
        txtMinOnTime.Size = new Size(129, 23);
        txtMinOnTime.TabIndex = 39;
        // 
        // txtHysteresis
        // 
        txtHysteresis.Dock = DockStyle.Fill;
        txtHysteresis.Location = new Point(177, 458);
        txtHysteresis.Name = "txtHysteresis";
        txtHysteresis.Size = new Size(129, 23);
        txtHysteresis.TabIndex = 38;
        // 
        // txtPercentageWhenOff
        // 
        txtPercentageWhenOff.Dock = DockStyle.Fill;
        txtPercentageWhenOff.Location = new Point(177, 429);
        txtPercentageWhenOff.Name = "txtPercentageWhenOff";
        txtPercentageWhenOff.Size = new Size(129, 23);
        txtPercentageWhenOff.TabIndex = 37;
        // 
        // txtPercentageWhenOn
        // 
        txtPercentageWhenOn.Dock = DockStyle.Fill;
        txtPercentageWhenOn.Location = new Point(177, 400);
        txtPercentageWhenOn.Name = "txtPercentageWhenOn";
        txtPercentageWhenOn.Size = new Size(129, 23);
        txtPercentageWhenOn.TabIndex = 36;
        // 
        // lblMinOffTime
        // 
        lblMinOffTime.AutoSize = true;
        lblMinOffTime.Dock = DockStyle.Fill;
        lblMinOffTime.Location = new Point(3, 513);
        lblMinOffTime.Name = "lblMinOffTime";
        lblMinOffTime.Size = new Size(168, 29);
        lblMinOffTime.TabIndex = 35;
        lblMinOffTime.Text = "Min. Off Time";
        lblMinOffTime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblMinOnTime
        // 
        lblMinOnTime.AutoSize = true;
        lblMinOnTime.Dock = DockStyle.Fill;
        lblMinOnTime.Location = new Point(3, 484);
        lblMinOnTime.Name = "lblMinOnTime";
        lblMinOnTime.Size = new Size(168, 29);
        lblMinOnTime.TabIndex = 34;
        lblMinOnTime.Text = "Min. On Time";
        lblMinOnTime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblHysteresis
        // 
        lblHysteresis.AutoSize = true;
        lblHysteresis.Dock = DockStyle.Fill;
        lblHysteresis.Location = new Point(3, 455);
        lblHysteresis.Name = "lblHysteresis";
        lblHysteresis.Size = new Size(168, 29);
        lblHysteresis.TabIndex = 33;
        lblHysteresis.Text = "Hysteresis";
        lblHysteresis.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblPercentageWhenOff
        // 
        lblPercentageWhenOff.AutoSize = true;
        lblPercentageWhenOff.Dock = DockStyle.Fill;
        lblPercentageWhenOff.Location = new Point(3, 426);
        lblPercentageWhenOff.Name = "lblPercentageWhenOff";
        lblPercentageWhenOff.Size = new Size(168, 29);
        lblPercentageWhenOff.TabIndex = 32;
        lblPercentageWhenOff.Text = "Percentage When Off";
        lblPercentageWhenOff.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblPercentageWhenOn
        // 
        lblPercentageWhenOn.AutoSize = true;
        lblPercentageWhenOn.Dock = DockStyle.Fill;
        lblPercentageWhenOn.Location = new Point(3, 397);
        lblPercentageWhenOn.Name = "lblPercentageWhenOn";
        lblPercentageWhenOn.Size = new Size(168, 29);
        lblPercentageWhenOn.TabIndex = 31;
        lblPercentageWhenOn.Text = "Percentage When On";
        lblPercentageWhenOn.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblOnOffTitle
        // 
        lblOnOffTitle.AutoSize = true;
        lblOnOffTitle.Cursor = Cursors.No;
        lblOnOffTitle.Dock = DockStyle.Fill;
        lblOnOffTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblOnOffTitle.Location = new Point(3, 357);
        lblOnOffTitle.Name = "lblOnOffTitle";
        lblOnOffTitle.Padding = new Padding(0, 20, 0, 0);
        lblOnOffTitle.Size = new Size(168, 40);
        lblOnOffTitle.TabIndex = 30;
        lblOnOffTitle.Text = "On/Off Control";
        lblOnOffTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblOnOffTitle.Click += label4_Click;
        // 
        // lblPIDTitle
        // 
        lblPIDTitle.AutoSize = true;
        lblPIDTitle.Cursor = Cursors.No;
        lblPIDTitle.Dock = DockStyle.Fill;
        lblPIDTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblPIDTitle.Location = new Point(3, 229);
        lblPIDTitle.Name = "lblPIDTitle";
        lblPIDTitle.Padding = new Padding(0, 20, 0, 0);
        lblPIDTitle.Size = new Size(168, 41);
        lblPIDTitle.TabIndex = 29;
        lblPIDTitle.Text = "PID Control";
        lblPIDTitle.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblHeatingRate
        // 
        lblHeatingRate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblHeatingRate.Location = new Point(1, 200);
        lblHeatingRate.Margin = new Padding(1, 0, 1, 0);
        lblHeatingRate.Name = "lblHeatingRate";
        lblHeatingRate.Size = new Size(172, 29);
        lblHeatingRate.TabIndex = 28;
        lblHeatingRate.Text = "Heat Thermal Power (kW)";
        lblHeatingRate.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCommonTitle
        // 
        lblCommonTitle.AutoSize = true;
        lblCommonTitle.Dock = DockStyle.Fill;
        lblCommonTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCommonTitle.Location = new Point(3, 0);
        lblCommonTitle.Name = "lblCommonTitle";
        lblCommonTitle.Padding = new Padding(0, 5, 0, 0);
        lblCommonTitle.Size = new Size(168, 26);
        lblCommonTitle.TabIndex = 27;
        lblCommonTitle.Text = "Common";
        lblCommonTitle.TextAlign = ContentAlignment.MiddleLeft;
        lblCommonTitle.Click += label2_Click_1;
        // 
        // txtSteps
        // 
        txtSteps.Dock = DockStyle.Fill;
        txtSteps.Location = new Point(177, 145);
        txtSteps.Name = "txtSteps";
        txtSteps.Size = new Size(129, 23);
        txtSteps.TabIndex = 23;
        // 
        // lblTotalSteps
        // 
        lblTotalSteps.AutoSize = true;
        lblTotalSteps.Dock = DockStyle.Fill;
        lblTotalSteps.Location = new Point(3, 142);
        lblTotalSteps.Name = "lblTotalSteps";
        lblTotalSteps.Size = new Size(168, 29);
        lblTotalSteps.TabIndex = 22;
        lblTotalSteps.Text = "Total Steps";
        lblTotalSteps.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtMaxHeatingPower
        // 
        txtMaxHeatingPower.Dock = DockStyle.Fill;
        txtMaxHeatingPower.Location = new Point(177, 203);
        txtMaxHeatingPower.Name = "txtMaxHeatingPower";
        txtMaxHeatingPower.Size = new Size(129, 23);
        txtMaxHeatingPower.TabIndex = 19;
        // 
        // txtTimeStep
        // 
        txtTimeStep.Dock = DockStyle.Fill;
        txtTimeStep.Location = new Point(177, 174);
        txtTimeStep.Name = "txtTimeStep";
        txtTimeStep.Size = new Size(129, 23);
        txtTimeStep.TabIndex = 17;
        // 
        // txtKd
        // 
        txtKd.Dock = DockStyle.Fill;
        txtKd.Location = new Point(177, 331);
        txtKd.Name = "txtKd";
        txtKd.Size = new Size(129, 23);
        txtKd.TabIndex = 16;
        // 
        // txtKi
        // 
        txtKi.Dock = DockStyle.Fill;
        txtKi.Location = new Point(177, 302);
        txtKi.Name = "txtKi";
        txtKi.Size = new Size(129, 23);
        txtKi.TabIndex = 15;
        // 
        // txtKp
        // 
        txtKp.Dock = DockStyle.Fill;
        txtKp.Location = new Point(177, 273);
        txtKp.Name = "txtKp";
        txtKp.Size = new Size(129, 23);
        txtKp.TabIndex = 14;
        // 
        // txtSetpoint
        // 
        txtSetpoint.Dock = DockStyle.Fill;
        txtSetpoint.Location = new Point(177, 116);
        txtSetpoint.Name = "txtSetpoint";
        txtSetpoint.Size = new Size(129, 23);
        txtSetpoint.TabIndex = 13;
        // 
        // txtOutdoorTemp
        // 
        txtOutdoorTemp.Dock = DockStyle.Fill;
        txtOutdoorTemp.Location = new Point(177, 87);
        txtOutdoorTemp.Name = "txtOutdoorTemp";
        txtOutdoorTemp.Size = new Size(129, 23);
        txtOutdoorTemp.TabIndex = 12;
        // 
        // txtInitialTemp
        // 
        txtInitialTemp.Dock = DockStyle.Fill;
        txtInitialTemp.Location = new Point(177, 58);
        txtInitialTemp.Name = "txtInitialTemp";
        txtInitialTemp.Size = new Size(129, 23);
        txtInitialTemp.TabIndex = 11;
        // 
        // lblInitialTemp
        // 
        lblInitialTemp.AutoSize = true;
        lblInitialTemp.Dock = DockStyle.Fill;
        lblInitialTemp.Location = new Point(3, 55);
        lblInitialTemp.Name = "lblInitialTemp";
        lblInitialTemp.Size = new Size(168, 29);
        lblInitialTemp.TabIndex = 2;
        lblInitialTemp.Text = "Initial Temp (ºC)";
        lblInitialTemp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblRoomName
        // 
        lblRoomName.AutoSize = true;
        lblRoomName.Dock = DockStyle.Fill;
        lblRoomName.Location = new Point(3, 26);
        lblRoomName.Name = "lblRoomName";
        lblRoomName.Size = new Size(168, 29);
        lblRoomName.TabIndex = 0;
        lblRoomName.Text = "Room Name";
        lblRoomName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtRoomName
        // 
        txtRoomName.Dock = DockStyle.Fill;
        txtRoomName.Location = new Point(177, 29);
        txtRoomName.Name = "txtRoomName";
        txtRoomName.Size = new Size(129, 23);
        txtRoomName.TabIndex = 1;
        // 
        // lblOutdoorTemp
        // 
        lblOutdoorTemp.AutoSize = true;
        lblOutdoorTemp.Dock = DockStyle.Fill;
        lblOutdoorTemp.Location = new Point(3, 84);
        lblOutdoorTemp.Name = "lblOutdoorTemp";
        lblOutdoorTemp.Size = new Size(168, 29);
        lblOutdoorTemp.TabIndex = 3;
        lblOutdoorTemp.Text = "Outdoor Temp (ºC)";
        lblOutdoorTemp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSetpoint
        // 
        lblSetpoint.AutoSize = true;
        lblSetpoint.Dock = DockStyle.Fill;
        lblSetpoint.Location = new Point(3, 113);
        lblSetpoint.Name = "lblSetpoint";
        lblSetpoint.Size = new Size(168, 29);
        lblSetpoint.TabIndex = 4;
        lblSetpoint.Text = "Setpoint (ºC)";
        lblSetpoint.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblKp
        // 
        lblKp.AutoSize = true;
        lblKp.Dock = DockStyle.Fill;
        lblKp.Location = new Point(3, 270);
        lblKp.Name = "lblKp";
        lblKp.Size = new Size(168, 29);
        lblKp.TabIndex = 5;
        lblKp.Text = "Kp";
        lblKp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblKi
        // 
        lblKi.AutoSize = true;
        lblKi.Dock = DockStyle.Fill;
        lblKi.Location = new Point(3, 299);
        lblKi.Name = "lblKi";
        lblKi.Size = new Size(168, 29);
        lblKi.TabIndex = 6;
        lblKi.Text = "Ki";
        lblKi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblKd
        // 
        lblKd.AutoSize = true;
        lblKd.Dock = DockStyle.Fill;
        lblKd.Location = new Point(3, 328);
        lblKd.Name = "lblKd";
        lblKd.Size = new Size(168, 29);
        lblKd.TabIndex = 7;
        lblKd.Text = "Kd";
        lblKd.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTimeStep
        // 
        lblTimeStep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblTimeStep.Location = new Point(1, 171);
        lblTimeStep.Margin = new Padding(1, 0, 1, 0);
        lblTimeStep.Name = "lblTimeStep";
        lblTimeStep.Size = new Size(172, 29);
        lblTimeStep.TabIndex = 8;
        lblTimeStep.Text = "Time Step (min)";
        lblTimeStep.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnRun
        // 
        btnRun.Location = new Point(3, 562);
        btnRun.Margin = new Padding(3, 20, 3, 3);
        btnRun.Name = "btnRun";
        btnRun.Size = new Size(131, 23);
        btnRun.TabIndex = 20;
        btnRun.Text = "Run Simulation";
        btnRun.UseVisualStyleBackColor = true;
        btnRun.Click += btnRun_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(177, 562);
        btnReset.Margin = new Padding(3, 20, 3, 3);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(110, 23);
        btnReset.TabIndex = 21;
        btnReset.Text = "Reset";
        btnReset.UseVisualStyleBackColor = true;
        // 
        // grpResults
        // 
        grpResults.Controls.Add(tblResults);
        grpResults.Dock = DockStyle.Fill;
        grpResults.Location = new Point(0, 0);
        grpResults.Name = "grpResults";
        grpResults.Size = new Size(992, 832);
        grpResults.TabIndex = 0;
        grpResults.TabStop = false;
        grpResults.Text = "Results";
        // 
        // tblResults
        // 
        tblResults.ColumnCount = 1;
        tblResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblResults.Controls.Add(OnOffResultGroup, 0, 2);
        tblResults.Controls.Add(PIDResultGroup, 0, 0);
        tblResults.Controls.Add(OnOffGraphsGroup, 0, 3);
        tblResults.Controls.Add(PIDGraphsGroup, 0, 1);
        tblResults.Dock = DockStyle.Fill;
        tblResults.Location = new Point(3, 19);
        tblResults.Name = "tblResults";
        tblResults.RowCount = 4;
        tblResults.RowStyles.Add(new RowStyle());
        tblResults.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblResults.RowStyles.Add(new RowStyle());
        tblResults.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblResults.Size = new Size(986, 810);
        tblResults.TabIndex = 0;
        // 
        // OnOffResultGroup
        // 
        OnOffResultGroup.Controls.Add(lblOnOffMetricsTitle);
        OnOffResultGroup.Controls.Add(lblOnOffActuator);
        OnOffResultGroup.Controls.Add(lblOnOffThermalEnergy);
        OnOffResultGroup.Controls.Add(lblOnOffComfortZone);
        OnOffResultGroup.Controls.Add(lblOnOffMaxOvershoot);
        OnOffResultGroup.Controls.Add(lblOnOffAbsErrorAverage);
        OnOffResultGroup.Controls.Add(lblOnOffSteps);
        OnOffResultGroup.Controls.Add(lblOnOffFinalTemperature);
        OnOffResultGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        OnOffResultGroup.Location = new Point(3, 408);
        OnOffResultGroup.Name = "OnOffResultGroup";
        OnOffResultGroup.Size = new Size(846, 66);
        OnOffResultGroup.TabIndex = 6;
        OnOffResultGroup.TabStop = false;
        OnOffResultGroup.Text = "Summary On/Off";
        // 
        // lblOnOffMetricsTitle
        // 
        lblOnOffMetricsTitle.AutoSize = true;
        lblOnOffMetricsTitle.Location = new Point(308, 0);
        lblOnOffMetricsTitle.Name = "lblOnOffMetricsTitle";
        lblOnOffMetricsTitle.Size = new Size(49, 15);
        lblOnOffMetricsTitle.TabIndex = 3;
        lblOnOffMetricsTitle.Text = "Metrics";
        // 
        // lblOnOffActuator
        // 
        lblOnOffActuator.AutoSize = true;
        lblOnOffActuator.Location = new Point(6, 47);
        lblOnOffActuator.Name = "lblOnOffActuator";
        lblOnOffActuator.Size = new Size(67, 15);
        lblOnOffActuator.TabIndex = 2;
        lblOnOffActuator.Text = "Actuator: -";
        // 
        // lblOnOffThermalEnergy
        // 
        lblOnOffThermalEnergy.AutoSize = true;
        lblOnOffThermalEnergy.Location = new Point(308, 48);
        lblOnOffThermalEnergy.Name = "lblOnOffThermalEnergy";
        lblOnOffThermalEnergy.Size = new Size(193, 15);
        lblOnOffThermalEnergy.TabIndex = 1;
        lblOnOffThermalEnergy.Text = "Total Thermal Energy Delivered: -";
        // 
        // lblOnOffComfortZone
        // 
        lblOnOffComfortZone.AutoSize = true;
        lblOnOffComfortZone.Location = new Point(631, 23);
        lblOnOffComfortZone.Name = "lblOnOffComfortZone";
        lblOnOffComfortZone.Size = new Size(165, 15);
        lblOnOffComfortZone.TabIndex = 1;
        lblOnOffComfortZone.Text = "Comfort Zone Permanency:-";
        // 
        // lblOnOffMaxOvershoot
        // 
        lblOnOffMaxOvershoot.AutoSize = true;
        lblOnOffMaxOvershoot.Location = new Point(478, 23);
        lblOnOffMaxOvershoot.Name = "lblOnOffMaxOvershoot";
        lblOnOffMaxOvershoot.Size = new Size(101, 15);
        lblOnOffMaxOvershoot.TabIndex = 1;
        lblOnOffMaxOvershoot.Text = "Max Overshoot:-";
        // 
        // lblOnOffAbsErrorAverage
        // 
        lblOnOffAbsErrorAverage.AutoSize = true;
        lblOnOffAbsErrorAverage.Location = new Point(308, 23);
        lblOnOffAbsErrorAverage.Name = "lblOnOffAbsErrorAverage";
        lblOnOffAbsErrorAverage.Size = new Size(119, 15);
        lblOnOffAbsErrorAverage.TabIndex = 1;
        lblOnOffAbsErrorAverage.Text = "Abs Error Average: -";
        // 
        // lblOnOffSteps
        // 
        lblOnOffSteps.AutoSize = true;
        lblOnOffSteps.Location = new Point(176, 47);
        lblOnOffSteps.Name = "lblOnOffSteps";
        lblOnOffSteps.Size = new Size(49, 15);
        lblOnOffSteps.TabIndex = 1;
        lblOnOffSteps.Text = "Steps: -";
        // 
        // lblOnOffFinalTemperature
        // 
        lblOnOffFinalTemperature.AutoSize = true;
        lblOnOffFinalTemperature.Location = new Point(6, 23);
        lblOnOffFinalTemperature.Name = "lblOnOffFinalTemperature";
        lblOnOffFinalTemperature.Size = new Size(155, 15);
        lblOnOffFinalTemperature.TabIndex = 0;
        lblOnOffFinalTemperature.Text = "Final Room Temperature: -";
        // 
        // PIDResultGroup
        // 
        PIDResultGroup.Controls.Add(lblPIDMetricsTitle);
        PIDResultGroup.Controls.Add(lblPIDActuator);
        PIDResultGroup.Controls.Add(lblPIDThermalEnergy);
        PIDResultGroup.Controls.Add(lblPIDComfortZone);
        PIDResultGroup.Controls.Add(lblPIDMaxOvershoot);
        PIDResultGroup.Controls.Add(lblPIDAbsErrorAverage);
        PIDResultGroup.Controls.Add(lblPIDSteps);
        PIDResultGroup.Controls.Add(lblPIDFinalTemperature);
        PIDResultGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        PIDResultGroup.Location = new Point(3, 3);
        PIDResultGroup.Name = "PIDResultGroup";
        PIDResultGroup.Size = new Size(846, 66);
        PIDResultGroup.TabIndex = 5;
        PIDResultGroup.TabStop = false;
        PIDResultGroup.Text = "Summary PID";
        // 
        // lblPIDMetricsTitle
        // 
        lblPIDMetricsTitle.AutoSize = true;
        lblPIDMetricsTitle.Location = new Point(308, 0);
        lblPIDMetricsTitle.Name = "lblPIDMetricsTitle";
        lblPIDMetricsTitle.Size = new Size(49, 15);
        lblPIDMetricsTitle.TabIndex = 3;
        lblPIDMetricsTitle.Text = "Metrics";
        // 
        // lblPIDActuator
        // 
        lblPIDActuator.AutoSize = true;
        lblPIDActuator.Location = new Point(6, 48);
        lblPIDActuator.Name = "lblPIDActuator";
        lblPIDActuator.Size = new Size(67, 15);
        lblPIDActuator.TabIndex = 2;
        lblPIDActuator.Text = "Actuator: -";
        // 
        // lblPIDThermalEnergy
        // 
        lblPIDThermalEnergy.AutoSize = true;
        lblPIDThermalEnergy.Location = new Point(308, 48);
        lblPIDThermalEnergy.Name = "lblPIDThermalEnergy";
        lblPIDThermalEnergy.Size = new Size(193, 15);
        lblPIDThermalEnergy.TabIndex = 1;
        lblPIDThermalEnergy.Text = "Total Thermal Energy Delivered: -";
        lblPIDThermalEnergy.Click += label22_Click;
        // 
        // lblPIDComfortZone
        // 
        lblPIDComfortZone.AutoSize = true;
        lblPIDComfortZone.Location = new Point(631, 23);
        lblPIDComfortZone.Name = "lblPIDComfortZone";
        lblPIDComfortZone.Size = new Size(165, 15);
        lblPIDComfortZone.TabIndex = 1;
        lblPIDComfortZone.Text = "Comfort Zone Permanency:-";
        lblPIDComfortZone.Click += label22_Click;
        // 
        // lblPIDMaxOvershoot
        // 
        lblPIDMaxOvershoot.AutoSize = true;
        lblPIDMaxOvershoot.Location = new Point(478, 23);
        lblPIDMaxOvershoot.Name = "lblPIDMaxOvershoot";
        lblPIDMaxOvershoot.Size = new Size(101, 15);
        lblPIDMaxOvershoot.TabIndex = 1;
        lblPIDMaxOvershoot.Text = "Max Overshoot:-";
        lblPIDMaxOvershoot.Click += label22_Click;
        // 
        // lblPIDAbsErrorAverage
        // 
        lblPIDAbsErrorAverage.AutoSize = true;
        lblPIDAbsErrorAverage.Location = new Point(308, 23);
        lblPIDAbsErrorAverage.Name = "lblPIDAbsErrorAverage";
        lblPIDAbsErrorAverage.Size = new Size(119, 15);
        lblPIDAbsErrorAverage.TabIndex = 1;
        lblPIDAbsErrorAverage.Text = "Abs Error Average: -";
        lblPIDAbsErrorAverage.Click += label22_Click;
        // 
        // lblPIDSteps
        // 
        lblPIDSteps.AutoSize = true;
        lblPIDSteps.Location = new Point(176, 48);
        lblPIDSteps.Name = "lblPIDSteps";
        lblPIDSteps.Size = new Size(49, 15);
        lblPIDSteps.TabIndex = 1;
        lblPIDSteps.Text = "Steps: -";
        // 
        // lblPIDFinalTemperature
        // 
        lblPIDFinalTemperature.AutoSize = true;
        lblPIDFinalTemperature.Location = new Point(6, 23);
        lblPIDFinalTemperature.Name = "lblPIDFinalTemperature";
        lblPIDFinalTemperature.Size = new Size(155, 15);
        lblPIDFinalTemperature.TabIndex = 0;
        lblPIDFinalTemperature.Text = "Final Room Temperature: -";
        // 
        // OnOffGraphsGroup
        // 
        OnOffGraphsGroup.ColumnCount = 2;
        OnOffGraphsGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        OnOffGraphsGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        OnOffGraphsGroup.Controls.Add(graphOnOffActuator, 1, 0);
        OnOffGraphsGroup.Controls.Add(graphOnOffTemperature, 0, 0);
        OnOffGraphsGroup.Dock = DockStyle.Fill;
        OnOffGraphsGroup.Location = new Point(3, 480);
        OnOffGraphsGroup.Name = "OnOffGraphsGroup";
        OnOffGraphsGroup.RowCount = 1;
        OnOffGraphsGroup.RowStyles.Add(new RowStyle());
        OnOffGraphsGroup.Size = new Size(980, 327);
        OnOffGraphsGroup.TabIndex = 3;
        // 
        // graphOnOffActuator
        // 
        graphOnOffActuator.Dock = DockStyle.Fill;
        graphOnOffActuator.Location = new Point(493, 3);
        graphOnOffActuator.Name = "graphOnOffActuator";
        graphOnOffActuator.Size = new Size(484, 321);
        graphOnOffActuator.TabIndex = 3;
        // 
        // graphOnOffTemperature
        // 
        graphOnOffTemperature.Dock = DockStyle.Fill;
        graphOnOffTemperature.Location = new Point(3, 3);
        graphOnOffTemperature.Name = "graphOnOffTemperature";
        graphOnOffTemperature.Size = new Size(484, 321);
        graphOnOffTemperature.TabIndex = 0;
        // 
        // PIDGraphsGroup
        // 
        PIDGraphsGroup.ColumnCount = 2;
        PIDGraphsGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        PIDGraphsGroup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        PIDGraphsGroup.Controls.Add(graphPIDActuator, 1, 0);
        PIDGraphsGroup.Controls.Add(graphPIDTemperature, 0, 0);
        PIDGraphsGroup.Dock = DockStyle.Fill;
        PIDGraphsGroup.Location = new Point(3, 75);
        PIDGraphsGroup.Name = "PIDGraphsGroup";
        PIDGraphsGroup.RowCount = 1;
        PIDGraphsGroup.RowStyles.Add(new RowStyle());
        PIDGraphsGroup.Size = new Size(980, 327);
        PIDGraphsGroup.TabIndex = 1;
        // 
        // graphPIDActuator
        // 
        graphPIDActuator.Dock = DockStyle.Fill;
        graphPIDActuator.Location = new Point(493, 3);
        graphPIDActuator.Name = "graphPIDActuator";
        graphPIDActuator.Size = new Size(484, 321);
        graphPIDActuator.TabIndex = 3;
        // 
        // graphPIDTemperature
        // 
        graphPIDTemperature.Dock = DockStyle.Fill;
        graphPIDTemperature.Location = new Point(3, 3);
        graphPIDTemperature.Name = "graphPIDTemperature";
        graphPIDTemperature.Size = new Size(484, 321);
        graphPIDTemperature.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1311, 832);
        Controls.Add(splitContainer1);
        Name = "MainForm";
        Text = "Simulator";
        Load += MainForm_Load;
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        grpParameters.ResumeLayout(false);
        tblParameters.ResumeLayout(false);
        tblParameters.PerformLayout();
        grpResults.ResumeLayout(false);
        tblResults.ResumeLayout(false);
        OnOffResultGroup.ResumeLayout(false);
        OnOffResultGroup.PerformLayout();
        PIDResultGroup.ResumeLayout(false);
        PIDResultGroup.PerformLayout();
        OnOffGraphsGroup.ResumeLayout(false);
        PIDGraphsGroup.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private SplitContainer splitContainer1;
    private SaveFileDialog saveFileDialog1;
    private GroupBox grpParameters;
    private TableLayoutPanel tblParameters;
    private Label lblInitialTemp;
    private Label lblRoomName;
    private TextBox txtRoomName;
    private TextBox txtInitialTemp;
    private Button btnRun;
    private Button btnReset;
    private GroupBox grpResults;
    private TableLayoutPanel tblResults;
    private TableLayoutPanel PIDGraphsGroup;
    private TextBox txtMaxHeatingPower;
    private TextBox txtTimeStep;
    private TextBox txtKd;
    private TextBox txtKi;
    private TextBox txtKp;
    private TextBox txtSetpoint;
    private TextBox txtOutdoorTemp;
    private Label lblOutdoorTemp;
    private Label lblSetpoint;
    private Label lblKp;
    private Label lblKi;
    private Label lblKd;
    private Label lblTimeStep;
    private TextBox txtSteps;
    private Label lblTotalSteps;
    private Label lblCommonTitle;
    private Label lblOnOffTitle;
    private Label lblPIDTitle;
    private Label lblHeatingRate;
    private Label lblMinOffTime;
    private Label lblMinOnTime;
    private Label lblHysteresis;
    private Label lblPercentageWhenOff;
    private Label lblPercentageWhenOn;
    private TextBox txtMinOffTime;
    private TextBox txtMinOnTime;
    private TextBox txtHysteresis;
    private TextBox txtPercentageWhenOff;
    private TextBox txtPercentageWhenOn;
    private ScottPlot.WinForms.FormsPlot graphPIDActuator;
    private ScottPlot.WinForms.FormsPlot graphPIDTemperature;
    private TableLayoutPanel OnOffGraphsGroup;
    private ScottPlot.WinForms.FormsPlot graphOnOffActuator;
    private ScottPlot.WinForms.FormsPlot graphOnOffTemperature;
    private GroupBox PIDResultGroup;
    private Label lblPIDMetricsTitle;
    private Label lblPIDActuator;
    private Label lblPIDSteps;
    private Label lblPIDFinalTemperature;
    private Label lblPIDAbsErrorAverage;
    private Label lblPIDComfortZone;
    private Label lblPIDMaxOvershoot;
    private Label lblPIDThermalEnergy;
    private GroupBox OnOffResultGroup;
    private Label lblOnOffMetricsTitle;
    private Label lblOnOffActuator;
    private Label lblOnOffThermalEnergy;
    private Label lblOnOffComfortZone;
    private Label lblOnOffMaxOvershoot;
    private Label lblOnOffAbsErrorAverage;
    private Label lblOnOffSteps;
    private Label lblOnOffFinalTemperature;
}
