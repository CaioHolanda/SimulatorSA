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
        txtInitialTemp = new TextBox();
        lblInitialTemp = new Label();
        lblRoomName = new Label();
        txtRoomName = new TextBox();
        btnRun = new Button();
        btnReset = new Button();
        grpResults = new GroupBox();
        tblResults = new TableLayoutPanel();
        tableLayoutPanel1 = new TableLayoutPanel();
        saveFileDialog1 = new SaveFileDialog();
        txtHeatingDelta = new TextBox();
        lblTimeStep = new Label();
        txtTimeStep = new TextBox();
        lblKd = new Label();
        txtKd = new TextBox();
        lblKi = new Label();
        txtKi = new TextBox();
        lblKp = new Label();
        txtKp = new TextBox();
        lblSetpoint = new Label();
        txtSetpoint = new TextBox();
        lblOutdoorTemp = new Label();
        txtOutdoorTemp = new TextBox();
        label1 = new Label();
        txtSteps = new TextBox();
        label2 = new Label();
        lblHeatingRate = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        label8 = new Label();
        label9 = new Label();
        textBox1 = new TextBox();
        textBox2 = new TextBox();
        textBox3 = new TextBox();
        textBox4 = new TextBox();
        textBox5 = new TextBox();
        groupBox1 = new GroupBox();
        label10 = new Label();
        label11 = new Label();
        label12 = new Label();
        label13 = new Label();
        formsPlotTemperature = new ScottPlot.WinForms.FormsPlot();
        formsPlot2 = new ScottPlot.WinForms.FormsPlot();
        tableLayoutPanel2 = new TableLayoutPanel();
        formsPlot1 = new ScottPlot.WinForms.FormsPlot();
        formsPlot3 = new ScottPlot.WinForms.FormsPlot();
        groupBox2 = new GroupBox();
        label14 = new Label();
        label15 = new Label();
        label16 = new Label();
        label17 = new Label();
        groupBox3 = new GroupBox();
        label18 = new Label();
        label19 = new Label();
        label20 = new Label();
        label21 = new Label();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        grpParameters.SuspendLayout();
        tblParameters.SuspendLayout();
        grpResults.SuspendLayout();
        tblResults.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox1.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
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
        splitContainer1.Size = new Size(1135, 832);
        splitContainer1.SplitterDistance = 259;
        splitContainer1.TabIndex = 0;
        // 
        // grpParameters
        // 
        grpParameters.Controls.Add(tblParameters);
        grpParameters.Dock = DockStyle.Fill;
        grpParameters.Font = new Font("Segoe UI", 9F);
        grpParameters.Location = new Point(0, 0);
        grpParameters.Name = "grpParameters";
        grpParameters.Size = new Size(259, 832);
        grpParameters.TabIndex = 1;
        grpParameters.TabStop = false;
        grpParameters.Text = "Simulation Parameters";
        // 
        // tblParameters
        // 
        tblParameters.ColumnCount = 2;
        tblParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.2416458F));
        tblParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.7583542F));
        tblParameters.Controls.Add(textBox5, 1, 17);
        tblParameters.Controls.Add(textBox4, 1, 16);
        tblParameters.Controls.Add(textBox3, 1, 15);
        tblParameters.Controls.Add(textBox2, 1, 14);
        tblParameters.Controls.Add(textBox1, 1, 13);
        tblParameters.Controls.Add(label9, 0, 17);
        tblParameters.Controls.Add(label8, 0, 16);
        tblParameters.Controls.Add(label7, 0, 15);
        tblParameters.Controls.Add(label6, 0, 14);
        tblParameters.Controls.Add(label5, 0, 13);
        tblParameters.Controls.Add(label4, 0, 12);
        tblParameters.Controls.Add(label3, 0, 8);
        tblParameters.Controls.Add(lblHeatingRate, 0, 7);
        tblParameters.Controls.Add(label2, 0, 0);
        tblParameters.Controls.Add(txtSteps, 1, 5);
        tblParameters.Controls.Add(label1, 0, 5);
        tblParameters.Controls.Add(txtHeatingDelta, 1, 7);
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
        tblParameters.Size = new Size(253, 810);
        tblParameters.TabIndex = 0;
        tblParameters.Paint += tblParameters_Paint;
        // 
        // txtInitialTemp
        // 
        txtInitialTemp.Dock = DockStyle.Fill;
        txtInitialTemp.Location = new Point(140, 58);
        txtInitialTemp.Name = "txtInitialTemp";
        txtInitialTemp.Size = new Size(110, 23);
        txtInitialTemp.TabIndex = 11;
        // 
        // lblInitialTemp
        // 
        lblInitialTemp.AutoSize = true;
        lblInitialTemp.Dock = DockStyle.Fill;
        lblInitialTemp.Location = new Point(3, 55);
        lblInitialTemp.Name = "lblInitialTemp";
        lblInitialTemp.Size = new Size(131, 29);
        lblInitialTemp.TabIndex = 2;
        lblInitialTemp.Text = "Initial Temp (ºC)";
        lblInitialTemp.TextAlign = ContentAlignment.MiddleLeft;
        lblInitialTemp.Click += label2_Click;
        // 
        // lblRoomName
        // 
        lblRoomName.AutoSize = true;
        lblRoomName.Dock = DockStyle.Fill;
        lblRoomName.Location = new Point(3, 26);
        lblRoomName.Name = "lblRoomName";
        lblRoomName.Size = new Size(131, 29);
        lblRoomName.TabIndex = 0;
        lblRoomName.Text = "Room Name";
        lblRoomName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtRoomName
        // 
        txtRoomName.Dock = DockStyle.Fill;
        txtRoomName.Location = new Point(140, 29);
        txtRoomName.Name = "txtRoomName";
        txtRoomName.Size = new Size(110, 23);
        txtRoomName.TabIndex = 1;
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
        btnReset.Location = new Point(140, 562);
        btnReset.Margin = new Padding(3, 20, 3, 3);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(110, 23);
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
        grpResults.Size = new Size(872, 832);
        grpResults.TabIndex = 0;
        grpResults.TabStop = false;
        grpResults.Text = "Results";
        // 
        // tblResults
        // 
        tblResults.ColumnCount = 1;
        tblResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tblResults.Controls.Add(groupBox3, 0, 0);
        tblResults.Controls.Add(groupBox2, 0, 2);
        tblResults.Controls.Add(tableLayoutPanel2, 0, 3);
        tblResults.Controls.Add(groupBox1, 0, 4);
        tblResults.Controls.Add(tableLayoutPanel1, 0, 1);
        tblResults.Dock = DockStyle.Fill;
        tblResults.Location = new Point(3, 19);
        tblResults.Name = "tblResults";
        tblResults.RowCount = 5;
        tblResults.RowStyles.Add(new RowStyle());
        tblResults.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblResults.RowStyles.Add(new RowStyle());
        tblResults.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tblResults.RowStyles.Add(new RowStyle());
        tblResults.Size = new Size(866, 810);
        tblResults.TabIndex = 0;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.Controls.Add(formsPlot2, 1, 0);
        tableLayoutPanel1.Controls.Add(formsPlotTemperature, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(3, 58);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.RowStyles.Add(new RowStyle());
        tableLayoutPanel1.Size = new Size(860, 311);
        tableLayoutPanel1.TabIndex = 1;
        // 
        // txtHeatingDelta
        // 
        txtHeatingDelta.Dock = DockStyle.Fill;
        txtHeatingDelta.Location = new Point(140, 203);
        txtHeatingDelta.Name = "txtHeatingDelta";
        txtHeatingDelta.Size = new Size(110, 23);
        txtHeatingDelta.TabIndex = 19;
        // 
        // lblTimeStep
        // 
        lblTimeStep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblTimeStep.Location = new Point(1, 171);
        lblTimeStep.Margin = new Padding(1, 0, 1, 0);
        lblTimeStep.Name = "lblTimeStep";
        lblTimeStep.Size = new Size(135, 29);
        lblTimeStep.TabIndex = 8;
        lblTimeStep.Text = "Time Step (min)";
        lblTimeStep.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtTimeStep
        // 
        txtTimeStep.Dock = DockStyle.Fill;
        txtTimeStep.Location = new Point(140, 174);
        txtTimeStep.Name = "txtTimeStep";
        txtTimeStep.Size = new Size(110, 23);
        txtTimeStep.TabIndex = 17;
        // 
        // lblKd
        // 
        lblKd.AutoSize = true;
        lblKd.Dock = DockStyle.Fill;
        lblKd.Location = new Point(3, 328);
        lblKd.Name = "lblKd";
        lblKd.Size = new Size(131, 29);
        lblKd.TabIndex = 7;
        lblKd.Text = "Kd";
        lblKd.TextAlign = ContentAlignment.MiddleLeft;
        lblKd.Click += label7_Click;
        // 
        // txtKd
        // 
        txtKd.Dock = DockStyle.Fill;
        txtKd.Location = new Point(140, 331);
        txtKd.Name = "txtKd";
        txtKd.Size = new Size(110, 23);
        txtKd.TabIndex = 16;
        // 
        // lblKi
        // 
        lblKi.AutoSize = true;
        lblKi.Dock = DockStyle.Fill;
        lblKi.Location = new Point(3, 299);
        lblKi.Name = "lblKi";
        lblKi.Size = new Size(131, 29);
        lblKi.TabIndex = 6;
        lblKi.Text = "Ki";
        lblKi.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtKi
        // 
        txtKi.Dock = DockStyle.Fill;
        txtKi.Location = new Point(140, 302);
        txtKi.Name = "txtKi";
        txtKi.Size = new Size(110, 23);
        txtKi.TabIndex = 15;
        // 
        // lblKp
        // 
        lblKp.AutoSize = true;
        lblKp.Dock = DockStyle.Fill;
        lblKp.Location = new Point(3, 270);
        lblKp.Name = "lblKp";
        lblKp.Size = new Size(131, 29);
        lblKp.TabIndex = 5;
        lblKp.Text = "Kp";
        lblKp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtKp
        // 
        txtKp.Dock = DockStyle.Fill;
        txtKp.Location = new Point(140, 273);
        txtKp.Name = "txtKp";
        txtKp.Size = new Size(110, 23);
        txtKp.TabIndex = 14;
        // 
        // lblSetpoint
        // 
        lblSetpoint.AutoSize = true;
        lblSetpoint.Dock = DockStyle.Fill;
        lblSetpoint.Location = new Point(3, 113);
        lblSetpoint.Name = "lblSetpoint";
        lblSetpoint.Size = new Size(131, 29);
        lblSetpoint.TabIndex = 4;
        lblSetpoint.Text = "Setpoint (ºC)";
        lblSetpoint.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtSetpoint
        // 
        txtSetpoint.Dock = DockStyle.Fill;
        txtSetpoint.Location = new Point(140, 116);
        txtSetpoint.Name = "txtSetpoint";
        txtSetpoint.Size = new Size(110, 23);
        txtSetpoint.TabIndex = 13;
        // 
        // lblOutdoorTemp
        // 
        lblOutdoorTemp.AutoSize = true;
        lblOutdoorTemp.Dock = DockStyle.Fill;
        lblOutdoorTemp.Location = new Point(3, 84);
        lblOutdoorTemp.Name = "lblOutdoorTemp";
        lblOutdoorTemp.Size = new Size(131, 29);
        lblOutdoorTemp.TabIndex = 3;
        lblOutdoorTemp.Text = "Outdoor Temp (ºC)";
        lblOutdoorTemp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtOutdoorTemp
        // 
        txtOutdoorTemp.Dock = DockStyle.Fill;
        txtOutdoorTemp.Location = new Point(140, 87);
        txtOutdoorTemp.Name = "txtOutdoorTemp";
        txtOutdoorTemp.Size = new Size(110, 23);
        txtOutdoorTemp.TabIndex = 12;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Dock = DockStyle.Fill;
        label1.Location = new Point(3, 142);
        label1.Name = "label1";
        label1.Size = new Size(131, 29);
        label1.TabIndex = 22;
        label1.Text = "Total Steps";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtSteps
        // 
        txtSteps.Dock = DockStyle.Fill;
        txtSteps.Location = new Point(140, 145);
        txtSteps.Name = "txtSteps";
        txtSteps.Size = new Size(110, 23);
        txtSteps.TabIndex = 23;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Fill;
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label2.Location = new Point(3, 0);
        label2.Name = "label2";
        label2.Padding = new Padding(0, 5, 0, 0);
        label2.Size = new Size(131, 26);
        label2.TabIndex = 27;
        label2.Text = "Common";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        label2.Click += label2_Click_1;
        // 
        // lblHeatingRate
        // 
        lblHeatingRate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblHeatingRate.Location = new Point(1, 200);
        lblHeatingRate.Margin = new Padding(1, 0, 1, 0);
        lblHeatingRate.Name = "lblHeatingRate";
        lblHeatingRate.Size = new Size(135, 29);
        lblHeatingRate.TabIndex = 28;
        lblHeatingRate.Text = "Heat Rate (ºC/min)";
        lblHeatingRate.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Cursor = Cursors.No;
        label3.Dock = DockStyle.Fill;
        label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        label3.Location = new Point(3, 229);
        label3.Name = "label3";
        label3.Padding = new Padding(0, 20, 0, 0);
        label3.Size = new Size(131, 41);
        label3.TabIndex = 29;
        label3.Text = "PID Control";
        label3.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Cursor = Cursors.No;
        label4.Dock = DockStyle.Fill;
        label4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        label4.Location = new Point(3, 357);
        label4.Name = "label4";
        label4.Padding = new Padding(0, 20, 0, 0);
        label4.Size = new Size(131, 40);
        label4.TabIndex = 30;
        label4.Text = "On/Off Control";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        label4.Click += label4_Click;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Dock = DockStyle.Fill;
        label5.Location = new Point(3, 397);
        label5.Name = "label5";
        label5.Size = new Size(131, 29);
        label5.TabIndex = 31;
        label5.Text = "Percentage When On";
        label5.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Dock = DockStyle.Fill;
        label6.Location = new Point(3, 426);
        label6.Name = "label6";
        label6.Size = new Size(131, 29);
        label6.TabIndex = 32;
        label6.Text = "Percentage When Off";
        label6.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Dock = DockStyle.Fill;
        label7.Location = new Point(3, 455);
        label7.Name = "label7";
        label7.Size = new Size(131, 29);
        label7.TabIndex = 33;
        label7.Text = "Hysteresis";
        label7.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Dock = DockStyle.Fill;
        label8.Location = new Point(3, 484);
        label8.Name = "label8";
        label8.Size = new Size(131, 29);
        label8.TabIndex = 34;
        label8.Text = "Min. On Time";
        label8.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Dock = DockStyle.Fill;
        label9.Location = new Point(3, 513);
        label9.Name = "label9";
        label9.Size = new Size(131, 29);
        label9.TabIndex = 35;
        label9.Text = "Min. Off Time";
        label9.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // textBox1
        // 
        textBox1.Dock = DockStyle.Fill;
        textBox1.Location = new Point(140, 400);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(110, 23);
        textBox1.TabIndex = 36;
        // 
        // textBox2
        // 
        textBox2.Dock = DockStyle.Fill;
        textBox2.Location = new Point(140, 429);
        textBox2.Name = "textBox2";
        textBox2.Size = new Size(110, 23);
        textBox2.TabIndex = 37;
        // 
        // textBox3
        // 
        textBox3.Dock = DockStyle.Fill;
        textBox3.Location = new Point(140, 458);
        textBox3.Name = "textBox3";
        textBox3.Size = new Size(110, 23);
        textBox3.TabIndex = 38;
        // 
        // textBox4
        // 
        textBox4.Dock = DockStyle.Fill;
        textBox4.Location = new Point(140, 487);
        textBox4.Name = "textBox4";
        textBox4.Size = new Size(110, 23);
        textBox4.TabIndex = 39;
        // 
        // textBox5
        // 
        textBox5.Dock = DockStyle.Fill;
        textBox5.Location = new Point(140, 516);
        textBox5.Name = "textBox5";
        textBox5.Size = new Size(110, 23);
        textBox5.TabIndex = 40;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(label10);
        groupBox1.Controls.Add(label11);
        groupBox1.Controls.Add(label12);
        groupBox1.Controls.Add(label13);
        groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        groupBox1.Location = new Point(3, 746);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(860, 61);
        groupBox1.TabIndex = 2;
        groupBox1.TabStop = false;
        groupBox1.Text = "Metrics";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Location = new Point(130, 23);
        label10.Name = "label10";
        label10.Size = new Size(62, 21);
        label10.TabIndex = 3;
        label10.Text = "Error: -";
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Location = new Point(246, 23);
        label11.Name = "label11";
        label11.Size = new Size(66, 21);
        label11.TabIndex = 2;
        label11.Text = "Valve: -";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Location = new Point(393, 23);
        label12.Name = "label12";
        label12.Size = new Size(65, 21);
        label12.TabIndex = 1;
        label12.Text = "Steps: -";
        // 
        // label13
        // 
        label13.AutoSize = true;
        label13.Location = new Point(6, 23);
        label13.Name = "label13";
        label13.Size = new Size(66, 21);
        label13.TabIndex = 0;
        label13.Text = "Temp: -";
        // 
        // formsPlotTemperature
        // 
        formsPlotTemperature.Location = new Point(3, 3);
        formsPlotTemperature.Name = "formsPlotTemperature";
        formsPlotTemperature.Size = new Size(421, 306);
        formsPlotTemperature.TabIndex = 0;
        // 
        // formsPlot2
        // 
        formsPlot2.Location = new Point(430, 3);
        formsPlot2.Name = "formsPlot2";
        formsPlot2.Size = new Size(421, 306);
        formsPlot2.TabIndex = 3;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel2.Controls.Add(formsPlot1, 1, 0);
        tableLayoutPanel2.Controls.Add(formsPlot3, 0, 0);
        tableLayoutPanel2.Dock = DockStyle.Fill;
        tableLayoutPanel2.Location = new Point(3, 429);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new RowStyle());
        tableLayoutPanel2.RowStyles.Add(new RowStyle());
        tableLayoutPanel2.Size = new Size(860, 311);
        tableLayoutPanel2.TabIndex = 3;
        // 
        // formsPlot1
        // 
        formsPlot1.Location = new Point(430, 3);
        formsPlot1.Name = "formsPlot1";
        formsPlot1.Size = new Size(421, 306);
        formsPlot1.TabIndex = 3;
        // 
        // formsPlot3
        // 
        formsPlot3.Location = new Point(3, 3);
        formsPlot3.Name = "formsPlot3";
        formsPlot3.Size = new Size(421, 306);
        formsPlot3.TabIndex = 0;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(label14);
        groupBox2.Controls.Add(label15);
        groupBox2.Controls.Add(label16);
        groupBox2.Controls.Add(label17);
        groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        groupBox2.Location = new Point(3, 375);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(860, 48);
        groupBox2.TabIndex = 4;
        groupBox2.TabStop = false;
        groupBox2.Text = "Summary On/Off";
        // 
        // label14
        // 
        label14.AutoSize = true;
        label14.Location = new Point(130, 23);
        label14.Name = "label14";
        label14.Size = new Size(46, 15);
        label14.TabIndex = 3;
        label14.Text = "Error: -";
        // 
        // label15
        // 
        label15.AutoSize = true;
        label15.Location = new Point(246, 23);
        label15.Name = "label15";
        label15.Size = new Size(48, 15);
        label15.TabIndex = 2;
        label15.Text = "Valve: -";
        // 
        // label16
        // 
        label16.AutoSize = true;
        label16.Location = new Point(393, 23);
        label16.Name = "label16";
        label16.Size = new Size(49, 15);
        label16.TabIndex = 1;
        label16.Text = "Steps: -";
        // 
        // label17
        // 
        label17.AutoSize = true;
        label17.Location = new Point(6, 23);
        label17.Name = "label17";
        label17.Size = new Size(49, 15);
        label17.TabIndex = 0;
        label17.Text = "Temp: -";
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(label18);
        groupBox3.Controls.Add(label19);
        groupBox3.Controls.Add(label20);
        groupBox3.Controls.Add(label21);
        groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        groupBox3.Location = new Point(3, 3);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(860, 49);
        groupBox3.TabIndex = 5;
        groupBox3.TabStop = false;
        groupBox3.Text = "Summary PID";
        // 
        // label18
        // 
        label18.AutoSize = true;
        label18.Location = new Point(130, 23);
        label18.Name = "label18";
        label18.Size = new Size(46, 15);
        label18.TabIndex = 3;
        label18.Text = "Error: -";
        // 
        // label19
        // 
        label19.AutoSize = true;
        label19.Location = new Point(246, 23);
        label19.Name = "label19";
        label19.Size = new Size(48, 15);
        label19.TabIndex = 2;
        label19.Text = "Valve: -";
        // 
        // label20
        // 
        label20.AutoSize = true;
        label20.Location = new Point(393, 23);
        label20.Name = "label20";
        label20.Size = new Size(49, 15);
        label20.TabIndex = 1;
        label20.Text = "Steps: -";
        // 
        // label21
        // 
        label21.AutoSize = true;
        label21.Location = new Point(6, 23);
        label21.Name = "label21";
        label21.Size = new Size(49, 15);
        label21.TabIndex = 0;
        label21.Text = "Temp: -";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1135, 832);
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
        tableLayoutPanel1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        tableLayoutPanel2.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
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
    private TableLayoutPanel tableLayoutPanel1;
    private TextBox txtHeatingDelta;
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
    private Label label1;
    private Label label2;
    private Label label4;
    private Label label3;
    private Label lblHeatingRate;
    private Label label9;
    private Label label8;
    private Label label7;
    private Label label6;
    private Label label5;
    private TextBox textBox5;
    private TextBox textBox4;
    private TextBox textBox3;
    private TextBox textBox2;
    private TextBox textBox1;
    private GroupBox groupBox1;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private ScottPlot.WinForms.FormsPlot formsPlot2;
    private ScottPlot.WinForms.FormsPlot formsPlotTemperature;
    private GroupBox groupBox2;
    private Label label14;
    private Label label15;
    private Label label16;
    private Label label17;
    private TableLayoutPanel tableLayoutPanel2;
    private ScottPlot.WinForms.FormsPlot formsPlot1;
    private ScottPlot.WinForms.FormsPlot formsPlot3;
    private GroupBox groupBox3;
    private Label label18;
    private Label label19;
    private Label label20;
    private Label label21;
}
