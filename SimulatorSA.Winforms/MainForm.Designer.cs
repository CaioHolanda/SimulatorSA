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
        txtHeatingDelta = new TextBox();
        txtSteps = new TextBox();
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
        lblTotalSteps = new Label();
        lblMaxHeatDelta = new Label();
        btnRun = new Button();
        btnReset = new Button();
        grpResults = new GroupBox();
        tblResults = new TableLayoutPanel();
        grpSummary = new GroupBox();
        lblFinalError = new Label();
        lblFinalValve = new Label();
        lblSteps = new Label();
        lblFinalTemp = new Label();
        tableLayoutPanel1 = new TableLayoutPanel();
        formsPlotTemperature = new ScottPlot.WinForms.FormsPlot();
        formsPlotValve = new ScottPlot.WinForms.FormsPlot();
        saveFileDialog1 = new SaveFileDialog();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        grpParameters.SuspendLayout();
        tblParameters.SuspendLayout();
        grpResults.SuspendLayout();
        tblResults.SuspendLayout();
        grpSummary.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
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
        splitContainer1.Size = new Size(800, 450);
        splitContainer1.SplitterDistance = 266;
        splitContainer1.TabIndex = 0;
        // 
        // grpParameters
        // 
        grpParameters.Controls.Add(tblParameters);
        grpParameters.Dock = DockStyle.Fill;
        grpParameters.Location = new Point(0, 0);
        grpParameters.Name = "grpParameters";
        grpParameters.Size = new Size(266, 450);
        grpParameters.TabIndex = 1;
        grpParameters.TabStop = false;
        grpParameters.Text = "Simulation Parameters";
        // 
        // tblParameters
        // 
        tblParameters.ColumnCount = 2;
        tblParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.4545441F));
        tblParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.5454559F));
        tblParameters.Controls.Add(txtHeatingDelta, 1, 9);
        tblParameters.Controls.Add(txtSteps, 1, 8);
        tblParameters.Controls.Add(txtTimeStep, 1, 7);
        tblParameters.Controls.Add(txtKd, 1, 6);
        tblParameters.Controls.Add(txtKi, 1, 5);
        tblParameters.Controls.Add(txtKp, 1, 4);
        tblParameters.Controls.Add(txtSetpoint, 1, 3);
        tblParameters.Controls.Add(txtOutdoorTemp, 1, 2);
        tblParameters.Controls.Add(txtInitialTemp, 1, 1);
        tblParameters.Controls.Add(lblInitialTemp, 0, 1);
        tblParameters.Controls.Add(lblRoomName, 0, 0);
        tblParameters.Controls.Add(txtRoomName, 1, 0);
        tblParameters.Controls.Add(lblOutdoorTemp, 0, 2);
        tblParameters.Controls.Add(lblSetpoint, 0, 3);
        tblParameters.Controls.Add(lblKp, 0, 4);
        tblParameters.Controls.Add(lblKi, 0, 5);
        tblParameters.Controls.Add(lblKd, 0, 6);
        tblParameters.Controls.Add(lblTimeStep, 0, 7);
        tblParameters.Controls.Add(lblTotalSteps, 0, 8);
        tblParameters.Controls.Add(lblMaxHeatDelta, 0, 9);
        tblParameters.Controls.Add(btnRun, 0, 10);
        tblParameters.Controls.Add(btnReset, 1, 10);
        tblParameters.Dock = DockStyle.Fill;
        tblParameters.Location = new Point(3, 19);
        tblParameters.Name = "tblParameters";
        tblParameters.RowCount = 12;
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
        tblParameters.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblParameters.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblParameters.Size = new Size(260, 428);
        tblParameters.TabIndex = 0;
        // 
        // txtHeatingDelta
        // 
        txtHeatingDelta.Dock = DockStyle.Fill;
        txtHeatingDelta.Location = new Point(121, 264);
        txtHeatingDelta.Name = "txtHeatingDelta";
        txtHeatingDelta.Size = new Size(136, 23);
        txtHeatingDelta.TabIndex = 19;
        // 
        // txtSteps
        // 
        txtSteps.Dock = DockStyle.Fill;
        txtSteps.Location = new Point(121, 235);
        txtSteps.Name = "txtSteps";
        txtSteps.Size = new Size(136, 23);
        txtSteps.TabIndex = 18;
        // 
        // txtTimeStep
        // 
        txtTimeStep.Dock = DockStyle.Fill;
        txtTimeStep.Location = new Point(121, 206);
        txtTimeStep.Name = "txtTimeStep";
        txtTimeStep.Size = new Size(136, 23);
        txtTimeStep.TabIndex = 17;
        // 
        // txtKd
        // 
        txtKd.Dock = DockStyle.Fill;
        txtKd.Location = new Point(121, 177);
        txtKd.Name = "txtKd";
        txtKd.Size = new Size(136, 23);
        txtKd.TabIndex = 16;
        // 
        // txtKi
        // 
        txtKi.Dock = DockStyle.Fill;
        txtKi.Location = new Point(121, 148);
        txtKi.Name = "txtKi";
        txtKi.Size = new Size(136, 23);
        txtKi.TabIndex = 15;
        // 
        // txtKp
        // 
        txtKp.Dock = DockStyle.Fill;
        txtKp.Location = new Point(121, 119);
        txtKp.Name = "txtKp";
        txtKp.Size = new Size(136, 23);
        txtKp.TabIndex = 14;
        // 
        // txtSetpoint
        // 
        txtSetpoint.Dock = DockStyle.Fill;
        txtSetpoint.Location = new Point(121, 90);
        txtSetpoint.Name = "txtSetpoint";
        txtSetpoint.Size = new Size(136, 23);
        txtSetpoint.TabIndex = 13;
        // 
        // txtOutdoorTemp
        // 
        txtOutdoorTemp.Dock = DockStyle.Fill;
        txtOutdoorTemp.Location = new Point(121, 61);
        txtOutdoorTemp.Name = "txtOutdoorTemp";
        txtOutdoorTemp.Size = new Size(136, 23);
        txtOutdoorTemp.TabIndex = 12;
        // 
        // txtInitialTemp
        // 
        txtInitialTemp.Dock = DockStyle.Fill;
        txtInitialTemp.Location = new Point(121, 32);
        txtInitialTemp.Name = "txtInitialTemp";
        txtInitialTemp.Size = new Size(136, 23);
        txtInitialTemp.TabIndex = 11;
        // 
        // lblInitialTemp
        // 
        lblInitialTemp.AutoSize = true;
        lblInitialTemp.Dock = DockStyle.Fill;
        lblInitialTemp.Location = new Point(3, 29);
        lblInitialTemp.Name = "lblInitialTemp";
        lblInitialTemp.Size = new Size(112, 29);
        lblInitialTemp.TabIndex = 2;
        lblInitialTemp.Text = "Initial Temp (ºC)";
        lblInitialTemp.TextAlign = ContentAlignment.MiddleLeft;
        lblInitialTemp.Click += label2_Click;
        // 
        // lblRoomName
        // 
        lblRoomName.AutoSize = true;
        lblRoomName.Dock = DockStyle.Fill;
        lblRoomName.Location = new Point(3, 0);
        lblRoomName.Name = "lblRoomName";
        lblRoomName.Size = new Size(112, 29);
        lblRoomName.TabIndex = 0;
        lblRoomName.Text = "Room Name";
        lblRoomName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtRoomName
        // 
        txtRoomName.Dock = DockStyle.Fill;
        txtRoomName.Location = new Point(121, 3);
        txtRoomName.Name = "txtRoomName";
        txtRoomName.Size = new Size(136, 23);
        txtRoomName.TabIndex = 1;
        // 
        // lblOutdoorTemp
        // 
        lblOutdoorTemp.AutoSize = true;
        lblOutdoorTemp.Dock = DockStyle.Fill;
        lblOutdoorTemp.Location = new Point(3, 58);
        lblOutdoorTemp.Name = "lblOutdoorTemp";
        lblOutdoorTemp.Size = new Size(112, 29);
        lblOutdoorTemp.TabIndex = 3;
        lblOutdoorTemp.Text = "Outdoor Temp (ºC)";
        lblOutdoorTemp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSetpoint
        // 
        lblSetpoint.AutoSize = true;
        lblSetpoint.Dock = DockStyle.Fill;
        lblSetpoint.Location = new Point(3, 87);
        lblSetpoint.Name = "lblSetpoint";
        lblSetpoint.Size = new Size(112, 29);
        lblSetpoint.TabIndex = 4;
        lblSetpoint.Text = "Setpoint (ºC)";
        lblSetpoint.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblKp
        // 
        lblKp.AutoSize = true;
        lblKp.Dock = DockStyle.Fill;
        lblKp.Location = new Point(3, 116);
        lblKp.Name = "lblKp";
        lblKp.Size = new Size(112, 29);
        lblKp.TabIndex = 5;
        lblKp.Text = "Kp";
        lblKp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblKi
        // 
        lblKi.AutoSize = true;
        lblKi.Dock = DockStyle.Fill;
        lblKi.Location = new Point(3, 145);
        lblKi.Name = "lblKi";
        lblKi.Size = new Size(112, 29);
        lblKi.TabIndex = 6;
        lblKi.Text = "Ki";
        lblKi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblKd
        // 
        lblKd.AutoSize = true;
        lblKd.Dock = DockStyle.Fill;
        lblKd.Location = new Point(3, 174);
        lblKd.Name = "lblKd";
        lblKd.Size = new Size(112, 29);
        lblKd.TabIndex = 7;
        lblKd.Text = "Kd";
        lblKd.TextAlign = ContentAlignment.MiddleLeft;
        lblKd.Click += label7_Click;
        // 
        // lblTimeStep
        // 
        lblTimeStep.AutoSize = true;
        lblTimeStep.Dock = DockStyle.Fill;
        lblTimeStep.Location = new Point(3, 203);
        lblTimeStep.Name = "lblTimeStep";
        lblTimeStep.Size = new Size(112, 29);
        lblTimeStep.TabIndex = 8;
        lblTimeStep.Text = "Time Step (min)";
        lblTimeStep.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblTotalSteps
        // 
        lblTotalSteps.AutoSize = true;
        lblTotalSteps.Dock = DockStyle.Fill;
        lblTotalSteps.Location = new Point(3, 232);
        lblTotalSteps.Name = "lblTotalSteps";
        lblTotalSteps.Size = new Size(112, 29);
        lblTotalSteps.TabIndex = 9;
        lblTotalSteps.Text = "Total Steps";
        lblTotalSteps.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblMaxHeatDelta
        // 
        lblMaxHeatDelta.AutoSize = true;
        lblMaxHeatDelta.Dock = DockStyle.Fill;
        lblMaxHeatDelta.Location = new Point(3, 261);
        lblMaxHeatDelta.Name = "lblMaxHeatDelta";
        lblMaxHeatDelta.Size = new Size(112, 29);
        lblMaxHeatDelta.TabIndex = 10;
        lblMaxHeatDelta.Text = "Max Heating Delta";
        lblMaxHeatDelta.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnRun
        // 
        btnRun.Location = new Point(3, 293);
        btnRun.Name = "btnRun";
        btnRun.Size = new Size(98, 23);
        btnRun.TabIndex = 20;
        btnRun.Text = "Run Simulation";
        btnRun.UseVisualStyleBackColor = true;
        btnRun.Click += btnRun_Click;
        // 
        // btnReset
        // 
        btnReset.Location = new Point(121, 293);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(75, 23);
        btnReset.TabIndex = 21;
        btnReset.Text = "Reset";
        btnReset.UseVisualStyleBackColor = true;
        btnReset.Click += btnReset_Click_1;
        // 
        // grpResults
        // 
        grpResults.Controls.Add(tblResults);
        grpResults.Dock = DockStyle.Fill;
        grpResults.Location = new Point(0, 0);
        grpResults.Name = "grpResults";
        grpResults.Size = new Size(530, 450);
        grpResults.TabIndex = 0;
        grpResults.TabStop = false;
        grpResults.Text = "Results";
        // 
        // tblResults
        // 
        tblResults.ColumnCount = 1;
        tblResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblResults.Controls.Add(grpSummary, 0, 0);
        tblResults.Controls.Add(tableLayoutPanel1, 0, 1);
        tblResults.Dock = DockStyle.Fill;
        tblResults.Location = new Point(3, 19);
        tblResults.Name = "tblResults";
        tblResults.RowCount = 2;
        tblResults.RowStyles.Add(new RowStyle(SizeType.Percent, 13F));
        tblResults.RowStyles.Add(new RowStyle(SizeType.Percent, 87F));
        tblResults.Size = new Size(524, 428);
        tblResults.TabIndex = 0;
        // 
        // grpSummary
        // 
        grpSummary.Controls.Add(lblFinalError);
        grpSummary.Controls.Add(lblFinalValve);
        grpSummary.Controls.Add(lblSteps);
        grpSummary.Controls.Add(lblFinalTemp);
        grpSummary.Dock = DockStyle.Fill;
        grpSummary.Location = new Point(3, 3);
        grpSummary.Name = "grpSummary";
        grpSummary.Size = new Size(518, 49);
        grpSummary.TabIndex = 0;
        grpSummary.TabStop = false;
        grpSummary.Text = "Summary";
        // 
        // lblFinalError
        // 
        lblFinalError.AutoSize = true;
        lblFinalError.Location = new Point(130, 23);
        lblFinalError.Name = "lblFinalError";
        lblFinalError.Size = new Size(43, 15);
        lblFinalError.TabIndex = 3;
        lblFinalError.Text = "Error: -";
        // 
        // lblFinalValve
        // 
        lblFinalValve.AutoSize = true;
        lblFinalValve.Location = new Point(246, 23);
        lblFinalValve.Name = "lblFinalValve";
        lblFinalValve.Size = new Size(45, 15);
        lblFinalValve.TabIndex = 2;
        lblFinalValve.Text = "Valve: -";
        // 
        // lblSteps
        // 
        lblSteps.AutoSize = true;
        lblSteps.Location = new Point(393, 23);
        lblSteps.Name = "lblSteps";
        lblSteps.Size = new Size(46, 15);
        lblSteps.TabIndex = 1;
        lblSteps.Text = "Steps: -";
        // 
        // lblFinalTemp
        // 
        lblFinalTemp.AutoSize = true;
        lblFinalTemp.Location = new Point(6, 23);
        lblFinalTemp.Name = "lblFinalTemp";
        lblFinalTemp.Size = new Size(48, 15);
        lblFinalTemp.TabIndex = 0;
        lblFinalTemp.Text = "Temp: -";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(formsPlotTemperature, 0, 0);
        tableLayoutPanel1.Controls.Add(formsPlotValve, 0, 1);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(3, 58);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(518, 367);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // formsPlotTemperature
        // 
        formsPlotTemperature.Dock = DockStyle.Fill;
        formsPlotTemperature.Location = new Point(3, 3);
        formsPlotTemperature.Name = "formsPlotTemperature";
        formsPlotTemperature.Size = new Size(512, 177);
        formsPlotTemperature.TabIndex = 0;
        // 
        // formsPlotValve
        // 
        formsPlotValve.Dock = DockStyle.Fill;
        formsPlotValve.Location = new Point(3, 186);
        formsPlotValve.Name = "formsPlotValve";
        formsPlotValve.Size = new Size(512, 178);
        formsPlotValve.TabIndex = 1;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
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
        grpSummary.ResumeLayout(false);
        grpSummary.PerformLayout();
        tableLayoutPanel1.ResumeLayout(false);
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
    private Label lblOutdoorTemp;
    private Label lblSetpoint;
    private Label lblKp;
    private Label lblKi;
    private Label lblKd;
    private Label lblTimeStep;
    private TextBox txtHeatingDelta;
    private TextBox txtSteps;
    private TextBox txtTimeStep;
    private TextBox txtKd;
    private TextBox txtKi;
    private TextBox txtKp;
    private TextBox txtSetpoint;
    private TextBox txtOutdoorTemp;
    private TextBox txtInitialTemp;
    private Label lblTotalSteps;
    private Label lblMaxHeatDelta;
    private Button btnRun;
    private Button btnReset;
    private GroupBox grpResults;
    private TableLayoutPanel tblResults;
    private GroupBox grpSummary;
    private Label lblFinalError;
    private Label lblFinalValve;
    private Label lblSteps;
    private Label lblFinalTemp;
    private TableLayoutPanel tableLayoutPanel1;
    private ScottPlot.WinForms.FormsPlot formsPlotTemperature;
    private ScottPlot.WinForms.FormsPlot formsPlotValve;
}
