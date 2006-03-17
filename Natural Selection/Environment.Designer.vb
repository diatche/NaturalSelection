<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Environment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Env = New System.Windows.Forms.Panel
        Me.InfoTabs = New System.Windows.Forms.TabControl
        Me.WorldInfoTab = New System.Windows.Forms.TabPage
        Me.GathererAveragesLabel = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GathererPlantRatioAveLabel = New System.Windows.Forms.Label
        Me.PlantCountAveLabel = New System.Windows.Forms.Label
        Me.GathererCountAveLabel = New System.Windows.Forms.Label
        Me.GathererPlantRatioLabel = New System.Windows.Forms.Label
        Me.PlantCountLabel = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GathererCountLabel = New System.Windows.Forms.Label
        Me.AgeLabel = New System.Windows.Forms.Label
        Me.IndInfoTab = New System.Windows.Forms.TabPage
        Me.IndInfoBox = New System.Windows.Forms.ListBox
        Me.GoButton = New System.Windows.Forms.Button
        Me.ResetButton = New System.Windows.Forms.Button
        Me.AddGathererButton = New System.Windows.Forms.Button
        Me.AddPlantButton = New System.Windows.Forms.Button
        Me.DivideButton = New System.Windows.Forms.Button
        Me.ControlerTabs = New System.Windows.Forms.TabControl
        Me.Object1 = New System.Windows.Forms.TabPage
        Me.FertiliseAmountControler = New System.Windows.Forms.TrackBar
        Me.FertiliseAmountControlerLabel = New System.Windows.Forms.Label
        Me.ClickAddFurtilityAmountBox = New System.Windows.Forms.NumericUpDown
        Me.ClickAddFurtility = New System.Windows.Forms.CheckBox
        Me.FertilitySpreadControler = New System.Windows.Forms.TrackBar
        Me.FertilitySpreadControlerLabel = New System.Windows.Forms.Label
        Me.FertilityDelayControler = New System.Windows.Forms.TrackBar
        Me.ClickAddGatherer = New System.Windows.Forms.CheckBox
        Me.FertilityDelayControlerLabel = New System.Windows.Forms.Label
        Me.ClickAddPlant = New System.Windows.Forms.CheckBox
        Me.AddPlantDelayControler = New System.Windows.Forms.TrackBar
        Me.AddPlantDelayControlerLabel = New System.Windows.Forms.Label
        Me.InheritAmountControler = New System.Windows.Forms.TrackBar
        Me.InheritAmountControlerLabel = New System.Windows.Forms.Label
        Me.GlobalTimerIntervalControler = New System.Windows.Forms.TrackBar
        Me.GlobalTimerIntervalControlerLabel = New System.Windows.Forms.Label
        Me.GathererControlsTab = New System.Windows.Forms.TabPage
        Me.GathererHealthDownControler = New System.Windows.Forms.TrackBar
        Me.GathererHealthDownControlerLabel = New System.Windows.Forms.Label
        Me.PlantControlsTab = New System.Windows.Forms.TabPage
        Me.PlantFeedDelayControler = New System.Windows.Forms.TrackBar
        Me.PlantFeedDelayControlerLabel = New System.Windows.Forms.Label
        Me.PlantGrowResponsivenesControl = New System.Windows.Forms.TrackBar
        Me.PlantGrowResponsivenesControlLabel = New System.Windows.Forms.Label
        Me.PlantFeedAmountControler = New System.Windows.Forms.TrackBar
        Me.PlantFeedAmountControlerLabel = New System.Windows.Forms.Label
        Me.SetDefaultsButton = New System.Windows.Forms.Button
        Me.NotesBox = New System.Windows.Forms.TextBox
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Info = New System.Windows.Forms.ToolTip(Me.components)
        Me.PauseButton = New System.Windows.Forms.Button
        Me.RenderButton = New System.Windows.Forms.Button
        Me.InfoTabs.SuspendLayout()
        Me.WorldInfoTab.SuspendLayout()
        Me.IndInfoTab.SuspendLayout()
        Me.ControlerTabs.SuspendLayout()
        Me.Object1.SuspendLayout()
        CType(Me.FertiliseAmountControler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ClickAddFurtilityAmountBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FertilitySpreadControler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FertilityDelayControler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AddPlantDelayControler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InheritAmountControler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GlobalTimerIntervalControler, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GathererControlsTab.SuspendLayout()
        CType(Me.GathererHealthDownControler, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PlantControlsTab.SuspendLayout()
        CType(Me.PlantFeedDelayControler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PlantGrowResponsivenesControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PlantFeedAmountControler, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Env
        '
        Me.Env.BackColor = System.Drawing.Color.Tan
        Me.Env.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Env.Location = New System.Drawing.Point(2, 2)
        Me.Env.Name = "Env"
        Me.Env.Size = New System.Drawing.Size(528, 480)
        Me.Env.TabIndex = 0
        '
        'InfoTabs
        '
        Me.InfoTabs.Controls.Add(Me.WorldInfoTab)
        Me.InfoTabs.Controls.Add(Me.IndInfoTab)
        Me.InfoTabs.Location = New System.Drawing.Point(535, 2)
        Me.InfoTabs.Name = "InfoTabs"
        Me.InfoTabs.SelectedIndex = 0
        Me.InfoTabs.Size = New System.Drawing.Size(265, 486)
        Me.InfoTabs.TabIndex = 1
        '
        'WorldInfoTab
        '
        Me.WorldInfoTab.BackColor = System.Drawing.Color.Transparent
        Me.WorldInfoTab.Controls.Add(Me.GathererAveragesLabel)
        Me.WorldInfoTab.Controls.Add(Me.Label3)
        Me.WorldInfoTab.Controls.Add(Me.Label6)
        Me.WorldInfoTab.Controls.Add(Me.Label7)
        Me.WorldInfoTab.Controls.Add(Me.Label8)
        Me.WorldInfoTab.Controls.Add(Me.GathererPlantRatioAveLabel)
        Me.WorldInfoTab.Controls.Add(Me.PlantCountAveLabel)
        Me.WorldInfoTab.Controls.Add(Me.GathererCountAveLabel)
        Me.WorldInfoTab.Controls.Add(Me.GathererPlantRatioLabel)
        Me.WorldInfoTab.Controls.Add(Me.PlantCountLabel)
        Me.WorldInfoTab.Controls.Add(Me.Label2)
        Me.WorldInfoTab.Controls.Add(Me.Label1)
        Me.WorldInfoTab.Controls.Add(Me.GathererCountLabel)
        Me.WorldInfoTab.Controls.Add(Me.AgeLabel)
        Me.WorldInfoTab.Location = New System.Drawing.Point(4, 22)
        Me.WorldInfoTab.Name = "WorldInfoTab"
        Me.WorldInfoTab.Padding = New System.Windows.Forms.Padding(3)
        Me.WorldInfoTab.Size = New System.Drawing.Size(257, 460)
        Me.WorldInfoTab.TabIndex = 0
        Me.WorldInfoTab.Text = "World Info"
        Me.WorldInfoTab.UseVisualStyleBackColor = True
        '
        'GathererAveragesLabel
        '
        Me.GathererAveragesLabel.Location = New System.Drawing.Point(6, 124)
        Me.GathererAveragesLabel.Name = "GathererAveragesLabel"
        Me.GathererAveragesLabel.Size = New System.Drawing.Size(248, 333)
        Me.GathererAveragesLabel.TabIndex = 13
        Me.GathererAveragesLabel.Text = "Nothing"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Gatherer Averages:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Ratio:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Plants:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Gatherers:"
        '
        'GathererPlantRatioAveLabel
        '
        Me.GathererPlantRatioAveLabel.AutoSize = True
        Me.GathererPlantRatioAveLabel.Location = New System.Drawing.Point(181, 66)
        Me.GathererPlantRatioAveLabel.Name = "GathererPlantRatioAveLabel"
        Me.GathererPlantRatioAveLabel.Size = New System.Drawing.Size(44, 13)
        Me.GathererPlantRatioAveLabel.TabIndex = 8
        Me.GathererPlantRatioAveLabel.Text = "Nothing"
        Me.GathererPlantRatioAveLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PlantCountAveLabel
        '
        Me.PlantCountAveLabel.AutoSize = True
        Me.PlantCountAveLabel.Location = New System.Drawing.Point(181, 53)
        Me.PlantCountAveLabel.Name = "PlantCountAveLabel"
        Me.PlantCountAveLabel.Size = New System.Drawing.Size(44, 13)
        Me.PlantCountAveLabel.TabIndex = 7
        Me.PlantCountAveLabel.Text = "Nothing"
        Me.PlantCountAveLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GathererCountAveLabel
        '
        Me.GathererCountAveLabel.AutoSize = True
        Me.GathererCountAveLabel.Location = New System.Drawing.Point(181, 40)
        Me.GathererCountAveLabel.Name = "GathererCountAveLabel"
        Me.GathererCountAveLabel.Size = New System.Drawing.Size(44, 13)
        Me.GathererCountAveLabel.TabIndex = 6
        Me.GathererCountAveLabel.Text = "Nothing"
        Me.GathererCountAveLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GathererPlantRatioLabel
        '
        Me.GathererPlantRatioLabel.AutoSize = True
        Me.GathererPlantRatioLabel.Location = New System.Drawing.Point(101, 66)
        Me.GathererPlantRatioLabel.Name = "GathererPlantRatioLabel"
        Me.GathererPlantRatioLabel.Size = New System.Drawing.Size(44, 13)
        Me.GathererPlantRatioLabel.TabIndex = 5
        Me.GathererPlantRatioLabel.Text = "Nothing"
        Me.GathererPlantRatioLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PlantCountLabel
        '
        Me.PlantCountLabel.AutoSize = True
        Me.PlantCountLabel.Location = New System.Drawing.Point(101, 53)
        Me.PlantCountLabel.Name = "PlantCountLabel"
        Me.PlantCountLabel.Size = New System.Drawing.Size(44, 13)
        Me.PlantCountLabel.TabIndex = 4
        Me.PlantCountLabel.Text = "Nothing"
        Me.PlantCountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(181, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Average"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(101, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Current"
        '
        'GathererCountLabel
        '
        Me.GathererCountLabel.AutoSize = True
        Me.GathererCountLabel.Location = New System.Drawing.Point(101, 40)
        Me.GathererCountLabel.Name = "GathererCountLabel"
        Me.GathererCountLabel.Size = New System.Drawing.Size(44, 13)
        Me.GathererCountLabel.TabIndex = 1
        Me.GathererCountLabel.Text = "Nothing"
        Me.GathererCountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'AgeLabel
        '
        Me.AgeLabel.AutoSize = True
        Me.AgeLabel.Location = New System.Drawing.Point(6, 3)
        Me.AgeLabel.Name = "AgeLabel"
        Me.AgeLabel.Size = New System.Drawing.Size(59, 13)
        Me.AgeLabel.TabIndex = 0
        Me.AgeLabel.Text = "World age:"
        '
        'IndInfoTab
        '
        Me.IndInfoTab.BackColor = System.Drawing.Color.Transparent
        Me.IndInfoTab.Controls.Add(Me.IndInfoBox)
        Me.IndInfoTab.Location = New System.Drawing.Point(4, 22)
        Me.IndInfoTab.Name = "IndInfoTab"
        Me.IndInfoTab.Padding = New System.Windows.Forms.Padding(3)
        Me.IndInfoTab.Size = New System.Drawing.Size(257, 460)
        Me.IndInfoTab.TabIndex = 1
        Me.IndInfoTab.Text = "Individual Info"
        Me.IndInfoTab.UseVisualStyleBackColor = True
        '
        'IndInfoBox
        '
        Me.IndInfoBox.BackColor = System.Drawing.SystemColors.Control
        Me.IndInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.IndInfoBox.FormattingEnabled = True
        Me.IndInfoBox.Location = New System.Drawing.Point(1, 1)
        Me.IndInfoBox.Name = "IndInfoBox"
        Me.IndInfoBox.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.IndInfoBox.Size = New System.Drawing.Size(255, 455)
        Me.IndInfoBox.TabIndex = 0
        '
        'GoButton
        '
        Me.GoButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.GoButton.Location = New System.Drawing.Point(7, 493)
        Me.GoButton.Name = "GoButton"
        Me.GoButton.Size = New System.Drawing.Size(41, 23)
        Me.GoButton.TabIndex = 3
        Me.GoButton.Text = "Go"
        Me.GoButton.UseVisualStyleBackColor = True
        '
        'ResetButton
        '
        Me.ResetButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ResetButton.Location = New System.Drawing.Point(114, 493)
        Me.ResetButton.Name = "ResetButton"
        Me.ResetButton.Size = New System.Drawing.Size(49, 23)
        Me.ResetButton.TabIndex = 4
        Me.ResetButton.Text = "Reset"
        Me.ResetButton.UseVisualStyleBackColor = True
        '
        'AddGathererButton
        '
        Me.AddGathererButton.Location = New System.Drawing.Point(7, 522)
        Me.AddGathererButton.Name = "AddGathererButton"
        Me.AddGathererButton.Size = New System.Drawing.Size(101, 23)
        Me.AddGathererButton.TabIndex = 5
        Me.AddGathererButton.Text = "Add Gatherer"
        Me.AddGathererButton.UseVisualStyleBackColor = True
        '
        'AddPlantButton
        '
        Me.AddPlantButton.Location = New System.Drawing.Point(7, 551)
        Me.AddPlantButton.Name = "AddPlantButton"
        Me.AddPlantButton.Size = New System.Drawing.Size(156, 23)
        Me.AddPlantButton.TabIndex = 6
        Me.AddPlantButton.Text = "Add Plant"
        Me.AddPlantButton.UseVisualStyleBackColor = True
        '
        'DivideButton
        '
        Me.DivideButton.Location = New System.Drawing.Point(114, 522)
        Me.DivideButton.Name = "DivideButton"
        Me.DivideButton.Size = New System.Drawing.Size(49, 23)
        Me.DivideButton.TabIndex = 7
        Me.DivideButton.Text = "Divide"
        Me.DivideButton.UseVisualStyleBackColor = True
        '
        'ControlerTabs
        '
        Me.ControlerTabs.Controls.Add(Me.Object1)
        Me.ControlerTabs.Controls.Add(Me.GathererControlsTab)
        Me.ControlerTabs.Controls.Add(Me.PlantControlsTab)
        Me.ControlerTabs.Location = New System.Drawing.Point(169, 495)
        Me.ControlerTabs.Name = "ControlerTabs"
        Me.ControlerTabs.SelectedIndex = 0
        Me.ControlerTabs.Size = New System.Drawing.Size(631, 178)
        Me.ControlerTabs.TabIndex = 9
        '
        'Object1
        '
        Me.Object1.BackColor = System.Drawing.SystemColors.Control
        Me.Object1.Controls.Add(Me.FertiliseAmountControler)
        Me.Object1.Controls.Add(Me.FertiliseAmountControlerLabel)
        Me.Object1.Controls.Add(Me.ClickAddFurtilityAmountBox)
        Me.Object1.Controls.Add(Me.ClickAddFurtility)
        Me.Object1.Controls.Add(Me.FertilitySpreadControler)
        Me.Object1.Controls.Add(Me.FertilitySpreadControlerLabel)
        Me.Object1.Controls.Add(Me.FertilityDelayControler)
        Me.Object1.Controls.Add(Me.ClickAddGatherer)
        Me.Object1.Controls.Add(Me.FertilityDelayControlerLabel)
        Me.Object1.Controls.Add(Me.ClickAddPlant)
        Me.Object1.Controls.Add(Me.AddPlantDelayControler)
        Me.Object1.Controls.Add(Me.AddPlantDelayControlerLabel)
        Me.Object1.Controls.Add(Me.InheritAmountControler)
        Me.Object1.Controls.Add(Me.InheritAmountControlerLabel)
        Me.Object1.Controls.Add(Me.GlobalTimerIntervalControler)
        Me.Object1.Controls.Add(Me.GlobalTimerIntervalControlerLabel)
        Me.Object1.Location = New System.Drawing.Point(4, 22)
        Me.Object1.Name = "Object1"
        Me.Object1.Padding = New System.Windows.Forms.Padding(3)
        Me.Object1.Size = New System.Drawing.Size(623, 152)
        Me.Object1.TabIndex = 0
        Me.Object1.Text = "World Controls"
        '
        'FertiliseAmountControler
        '
        Me.FertiliseAmountControler.Location = New System.Drawing.Point(327, 108)
        Me.FertiliseAmountControler.Maximum = 20
        Me.FertiliseAmountControler.Name = "FertiliseAmountControler"
        Me.FertiliseAmountControler.Size = New System.Drawing.Size(104, 45)
        Me.FertiliseAmountControler.TabIndex = 16
        Me.FertiliseAmountControler.TickFrequency = 2
        Me.FertiliseAmountControler.Value = 10
        '
        'FertiliseAmountControlerLabel
        '
        Me.FertiliseAmountControlerLabel.AutoSize = True
        Me.FertiliseAmountControlerLabel.Location = New System.Drawing.Point(224, 112)
        Me.FertiliseAmountControlerLabel.Name = "FertiliseAmountControlerLabel"
        Me.FertiliseAmountControlerLabel.Size = New System.Drawing.Size(70, 13)
        Me.FertiliseAmountControlerLabel.TabIndex = 15
        Me.FertiliseAmountControlerLabel.Text = "Fert. Amount:"
        '
        'ClickAddFurtilityAmountBox
        '
        Me.ClickAddFurtilityAmountBox.Location = New System.Drawing.Point(577, 56)
        Me.ClickAddFurtilityAmountBox.Name = "ClickAddFurtilityAmountBox"
        Me.ClickAddFurtilityAmountBox.Size = New System.Drawing.Size(40, 20)
        Me.ClickAddFurtilityAmountBox.TabIndex = 14
        Me.ClickAddFurtilityAmountBox.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'ClickAddFurtility
        '
        Me.ClickAddFurtility.AutoSize = True
        Me.ClickAddFurtility.Location = New System.Drawing.Point(470, 57)
        Me.ClickAddFurtility.Name = "ClickAddFurtility"
        Me.ClickAddFurtility.Size = New System.Drawing.Size(105, 17)
        Me.ClickAddFurtility.TabIndex = 13
        Me.ClickAddFurtility.Text = "Click-add Furtility"
        Me.ClickAddFurtility.UseVisualStyleBackColor = True
        '
        'FertilitySpreadControler
        '
        Me.FertilitySpreadControler.Location = New System.Drawing.Point(327, 57)
        Me.FertilitySpreadControler.Maximum = 20
        Me.FertilitySpreadControler.Name = "FertilitySpreadControler"
        Me.FertilitySpreadControler.Size = New System.Drawing.Size(104, 45)
        Me.FertilitySpreadControler.TabIndex = 9
        Me.FertilitySpreadControler.TickFrequency = 2
        Me.FertilitySpreadControler.Value = 4
        '
        'FertilitySpreadControlerLabel
        '
        Me.FertilitySpreadControlerLabel.AutoSize = True
        Me.FertilitySpreadControlerLabel.Location = New System.Drawing.Point(224, 61)
        Me.FertilitySpreadControlerLabel.Name = "FertilitySpreadControlerLabel"
        Me.FertilitySpreadControlerLabel.Size = New System.Drawing.Size(68, 13)
        Me.FertilitySpreadControlerLabel.TabIndex = 8
        Me.FertilitySpreadControlerLabel.Text = "Fert. Spread:"
        '
        'FertilityDelayControler
        '
        Me.FertilityDelayControler.Location = New System.Drawing.Point(327, 6)
        Me.FertilityDelayControler.Maximum = 20
        Me.FertilityDelayControler.Name = "FertilityDelayControler"
        Me.FertilityDelayControler.Size = New System.Drawing.Size(104, 45)
        Me.FertilityDelayControler.TabIndex = 7
        Me.FertilityDelayControler.TickFrequency = 2
        Me.FertilityDelayControler.Value = 10
        '
        'ClickAddGatherer
        '
        Me.ClickAddGatherer.AutoSize = True
        Me.ClickAddGatherer.Location = New System.Drawing.Point(470, 11)
        Me.ClickAddGatherer.Name = "ClickAddGatherer"
        Me.ClickAddGatherer.Size = New System.Drawing.Size(114, 17)
        Me.ClickAddGatherer.TabIndex = 12
        Me.ClickAddGatherer.Text = "Click-add Gatherer"
        Me.ClickAddGatherer.UseVisualStyleBackColor = True
        '
        'FertilityDelayControlerLabel
        '
        Me.FertilityDelayControlerLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.FertilityDelayControlerLabel.AutoSize = True
        Me.FertilityDelayControlerLabel.Location = New System.Drawing.Point(224, 10)
        Me.FertilityDelayControlerLabel.Name = "FertilityDelayControlerLabel"
        Me.FertilityDelayControlerLabel.Size = New System.Drawing.Size(61, 13)
        Me.FertilityDelayControlerLabel.TabIndex = 6
        Me.FertilityDelayControlerLabel.Text = "Fert. Delay:"
        '
        'ClickAddPlant
        '
        Me.ClickAddPlant.AutoSize = True
        Me.ClickAddPlant.Location = New System.Drawing.Point(470, 34)
        Me.ClickAddPlant.Name = "ClickAddPlant"
        Me.ClickAddPlant.Size = New System.Drawing.Size(97, 17)
        Me.ClickAddPlant.TabIndex = 11
        Me.ClickAddPlant.Text = "Click-add Plant"
        Me.ClickAddPlant.UseVisualStyleBackColor = True
        '
        'AddPlantDelayControler
        '
        Me.AddPlantDelayControler.Location = New System.Drawing.Point(109, 108)
        Me.AddPlantDelayControler.Minimum = 2
        Me.AddPlantDelayControler.Name = "AddPlantDelayControler"
        Me.AddPlantDelayControler.Size = New System.Drawing.Size(104, 45)
        Me.AddPlantDelayControler.TabIndex = 5
        Me.AddPlantDelayControler.Value = 3
        '
        'AddPlantDelayControlerLabel
        '
        Me.AddPlantDelayControlerLabel.AutoSize = True
        Me.AddPlantDelayControlerLabel.Location = New System.Drawing.Point(6, 112)
        Me.AddPlantDelayControlerLabel.Name = "AddPlantDelayControlerLabel"
        Me.AddPlantDelayControlerLabel.Size = New System.Drawing.Size(84, 13)
        Me.AddPlantDelayControlerLabel.TabIndex = 4
        Me.AddPlantDelayControlerLabel.Text = "Add Plant delay:"
        '
        'InheritAmountControler
        '
        Me.InheritAmountControler.Location = New System.Drawing.Point(109, 57)
        Me.InheritAmountControler.Maximum = 20
        Me.InheritAmountControler.Name = "InheritAmountControler"
        Me.InheritAmountControler.Size = New System.Drawing.Size(104, 45)
        Me.InheritAmountControler.TabIndex = 3
        Me.InheritAmountControler.TickFrequency = 2
        Me.InheritAmountControler.Value = 15
        '
        'InheritAmountControlerLabel
        '
        Me.InheritAmountControlerLabel.AutoSize = True
        Me.InheritAmountControlerLabel.Location = New System.Drawing.Point(6, 61)
        Me.InheritAmountControlerLabel.Name = "InheritAmountControlerLabel"
        Me.InheritAmountControlerLabel.Size = New System.Drawing.Size(77, 13)
        Me.InheritAmountControlerLabel.TabIndex = 2
        Me.InheritAmountControlerLabel.Text = "Inherit amount:"
        '
        'GlobalTimerIntervalControler
        '
        Me.GlobalTimerIntervalControler.Location = New System.Drawing.Point(109, 6)
        Me.GlobalTimerIntervalControler.Name = "GlobalTimerIntervalControler"
        Me.GlobalTimerIntervalControler.Size = New System.Drawing.Size(104, 45)
        Me.GlobalTimerIntervalControler.TabIndex = 1
        Me.Info.SetToolTip(Me.GlobalTimerIntervalControler, "Bugged")
        Me.GlobalTimerIntervalControler.Value = 5
        '
        'GlobalTimerIntervalControlerLabel
        '
        Me.GlobalTimerIntervalControlerLabel.AutoSize = True
        Me.GlobalTimerIntervalControlerLabel.Location = New System.Drawing.Point(6, 10)
        Me.GlobalTimerIntervalControlerLabel.Name = "GlobalTimerIntervalControlerLabel"
        Me.GlobalTimerIntervalControlerLabel.Size = New System.Drawing.Size(73, 13)
        Me.GlobalTimerIntervalControlerLabel.TabIndex = 0
        Me.GlobalTimerIntervalControlerLabel.Text = "Timer interval:"
        '
        'GathererControlsTab
        '
        Me.GathererControlsTab.BackColor = System.Drawing.SystemColors.Control
        Me.GathererControlsTab.Controls.Add(Me.GathererHealthDownControler)
        Me.GathererControlsTab.Controls.Add(Me.GathererHealthDownControlerLabel)
        Me.GathererControlsTab.Location = New System.Drawing.Point(4, 22)
        Me.GathererControlsTab.Name = "GathererControlsTab"
        Me.GathererControlsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.GathererControlsTab.Size = New System.Drawing.Size(623, 152)
        Me.GathererControlsTab.TabIndex = 1
        Me.GathererControlsTab.Text = "Gatherer Controls"
        '
        'GathererHealthDownControler
        '
        Me.GathererHealthDownControler.Location = New System.Drawing.Point(109, 6)
        Me.GathererHealthDownControler.Maximum = 50
        Me.GathererHealthDownControler.Minimum = 1
        Me.GathererHealthDownControler.Name = "GathererHealthDownControler"
        Me.GathererHealthDownControler.Size = New System.Drawing.Size(104, 45)
        Me.GathererHealthDownControler.TabIndex = 5
        Me.GathererHealthDownControler.TickFrequency = 5
        Me.GathererHealthDownControler.Value = 25
        '
        'GathererHealthDownControlerLabel
        '
        Me.GathererHealthDownControlerLabel.AutoSize = True
        Me.GathererHealthDownControlerLabel.Location = New System.Drawing.Point(6, 10)
        Me.GathererHealthDownControlerLabel.Name = "GathererHealthDownControlerLabel"
        Me.GathererHealthDownControlerLabel.Size = New System.Drawing.Size(75, 13)
        Me.GathererHealthDownControlerLabel.TabIndex = 4
        Me.GathererHealthDownControlerLabel.Text = "Health Decay:"
        '
        'PlantControlsTab
        '
        Me.PlantControlsTab.BackColor = System.Drawing.SystemColors.Control
        Me.PlantControlsTab.Controls.Add(Me.PlantFeedDelayControler)
        Me.PlantControlsTab.Controls.Add(Me.PlantFeedDelayControlerLabel)
        Me.PlantControlsTab.Controls.Add(Me.PlantGrowResponsivenesControl)
        Me.PlantControlsTab.Controls.Add(Me.PlantGrowResponsivenesControlLabel)
        Me.PlantControlsTab.Controls.Add(Me.PlantFeedAmountControler)
        Me.PlantControlsTab.Controls.Add(Me.PlantFeedAmountControlerLabel)
        Me.PlantControlsTab.Location = New System.Drawing.Point(4, 22)
        Me.PlantControlsTab.Name = "PlantControlsTab"
        Me.PlantControlsTab.Size = New System.Drawing.Size(623, 152)
        Me.PlantControlsTab.TabIndex = 2
        Me.PlantControlsTab.Text = "Plant Controls"
        '
        'PlantFeedDelayControler
        '
        Me.PlantFeedDelayControler.Location = New System.Drawing.Point(109, 108)
        Me.PlantFeedDelayControler.Name = "PlantFeedDelayControler"
        Me.PlantFeedDelayControler.Size = New System.Drawing.Size(104, 45)
        Me.PlantFeedDelayControler.TabIndex = 7
        Me.PlantFeedDelayControler.Value = 4
        '
        'PlantFeedDelayControlerLabel
        '
        Me.PlantFeedDelayControlerLabel.AutoSize = True
        Me.PlantFeedDelayControlerLabel.Location = New System.Drawing.Point(6, 112)
        Me.PlantFeedDelayControlerLabel.Name = "PlantFeedDelayControlerLabel"
        Me.PlantFeedDelayControlerLabel.Size = New System.Drawing.Size(62, 13)
        Me.PlantFeedDelayControlerLabel.TabIndex = 6
        Me.PlantFeedDelayControlerLabel.Text = "Feed delay:"
        '
        'PlantGrowResponsivenesControl
        '
        Me.PlantGrowResponsivenesControl.Location = New System.Drawing.Point(109, 57)
        Me.PlantGrowResponsivenesControl.Maximum = 20
        Me.PlantGrowResponsivenesControl.Name = "PlantGrowResponsivenesControl"
        Me.PlantGrowResponsivenesControl.Size = New System.Drawing.Size(104, 45)
        Me.PlantGrowResponsivenesControl.TabIndex = 5
        Me.PlantGrowResponsivenesControl.TickFrequency = 2
        Me.PlantGrowResponsivenesControl.Value = 8
        Me.PlantGrowResponsivenesControl.Visible = False
        '
        'PlantGrowResponsivenesControlLabel
        '
        Me.PlantGrowResponsivenesControlLabel.AutoSize = True
        Me.PlantGrowResponsivenesControlLabel.Location = New System.Drawing.Point(6, 61)
        Me.PlantGrowResponsivenesControlLabel.Name = "PlantGrowResponsivenesControlLabel"
        Me.PlantGrowResponsivenesControlLabel.Size = New System.Drawing.Size(63, 13)
        Me.PlantGrowResponsivenesControlLabel.TabIndex = 4
        Me.PlantGrowResponsivenesControlLabel.Text = "Grow delay:"
        Me.PlantGrowResponsivenesControlLabel.Visible = False
        '
        'PlantFeedAmountControler
        '
        Me.PlantFeedAmountControler.Location = New System.Drawing.Point(109, 6)
        Me.PlantFeedAmountControler.Maximum = 20
        Me.PlantFeedAmountControler.Name = "PlantFeedAmountControler"
        Me.PlantFeedAmountControler.Size = New System.Drawing.Size(104, 45)
        Me.PlantFeedAmountControler.TabIndex = 3
        Me.PlantFeedAmountControler.TickFrequency = 2
        Me.PlantFeedAmountControler.Value = 10
        '
        'PlantFeedAmountControlerLabel
        '
        Me.PlantFeedAmountControlerLabel.AutoSize = True
        Me.PlantFeedAmountControlerLabel.Location = New System.Drawing.Point(6, 10)
        Me.PlantFeedAmountControlerLabel.Name = "PlantFeedAmountControlerLabel"
        Me.PlantFeedAmountControlerLabel.Size = New System.Drawing.Size(53, 13)
        Me.PlantFeedAmountControlerLabel.TabIndex = 2
        Me.PlantFeedAmountControlerLabel.Text = "Soil subt.:"
        '
        'SetDefaultsButton
        '
        Me.SetDefaultsButton.Location = New System.Drawing.Point(723, 490)
        Me.SetDefaultsButton.Name = "SetDefaultsButton"
        Me.SetDefaultsButton.Size = New System.Drawing.Size(75, 23)
        Me.SetDefaultsButton.TabIndex = 10
        Me.SetDefaultsButton.Text = "Defaults"
        Me.SetDefaultsButton.UseVisualStyleBackColor = True
        '
        'NotesBox
        '
        Me.NotesBox.Location = New System.Drawing.Point(7, 580)
        Me.NotesBox.Name = "NotesBox"
        Me.NotesBox.Size = New System.Drawing.Size(156, 20)
        Me.NotesBox.TabIndex = 13
        Me.NotesBox.Text = "Notes"
        Me.NotesBox.Visible = False
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 50
        '
        'Info
        '
        Me.Info.AutomaticDelay = 1
        Me.Info.AutoPopDelay = 100000
        Me.Info.InitialDelay = 1
        Me.Info.ReshowDelay = 0
        '
        'PauseButton
        '
        Me.PauseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.PauseButton.Location = New System.Drawing.Point(54, 493)
        Me.PauseButton.Name = "PauseButton"
        Me.PauseButton.Size = New System.Drawing.Size(54, 23)
        Me.PauseButton.TabIndex = 14
        Me.PauseButton.Text = "Pause"
        Me.PauseButton.UseVisualStyleBackColor = True
        '
        'RenderButton
        '
        Me.RenderButton.Location = New System.Drawing.Point(7, 647)
        Me.RenderButton.Name = "RenderButton"
        Me.RenderButton.Size = New System.Drawing.Size(54, 23)
        Me.RenderButton.TabIndex = 15
        Me.RenderButton.Text = "Render"
        Me.RenderButton.UseVisualStyleBackColor = True
        Me.RenderButton.Visible = False
        '
        'Environment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(803, 676)
        Me.Controls.Add(Me.RenderButton)
        Me.Controls.Add(Me.PauseButton)
        Me.Controls.Add(Me.NotesBox)
        Me.Controls.Add(Me.SetDefaultsButton)
        Me.Controls.Add(Me.ControlerTabs)
        Me.Controls.Add(Me.DivideButton)
        Me.Controls.Add(Me.AddPlantButton)
        Me.Controls.Add(Me.AddGathererButton)
        Me.Controls.Add(Me.ResetButton)
        Me.Controls.Add(Me.GoButton)
        Me.Controls.Add(Me.InfoTabs)
        Me.Controls.Add(Me.Env)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Environment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Natural Selection"
        Me.InfoTabs.ResumeLayout(False)
        Me.WorldInfoTab.ResumeLayout(False)
        Me.WorldInfoTab.PerformLayout()
        Me.IndInfoTab.ResumeLayout(False)
        Me.ControlerTabs.ResumeLayout(False)
        Me.Object1.ResumeLayout(False)
        Me.Object1.PerformLayout()
        CType(Me.FertiliseAmountControler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ClickAddFurtilityAmountBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FertilitySpreadControler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FertilityDelayControler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AddPlantDelayControler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InheritAmountControler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GlobalTimerIntervalControler, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GathererControlsTab.ResumeLayout(False)
        Me.GathererControlsTab.PerformLayout()
        CType(Me.GathererHealthDownControler, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PlantControlsTab.ResumeLayout(False)
        Me.PlantControlsTab.PerformLayout()
        CType(Me.PlantFeedDelayControler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PlantGrowResponsivenesControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PlantFeedAmountControler, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Env As System.Windows.Forms.Panel
    Friend WithEvents InfoTabs As System.Windows.Forms.TabControl
    Friend WithEvents WorldInfoTab As System.Windows.Forms.TabPage
    Friend WithEvents IndInfoTab As System.Windows.Forms.TabPage
    Friend WithEvents IndInfoBox As System.Windows.Forms.ListBox
    Friend WithEvents GoButton As System.Windows.Forms.Button
    Friend WithEvents ResetButton As System.Windows.Forms.Button
    Friend WithEvents AddGathererButton As System.Windows.Forms.Button
    Friend WithEvents AddPlantButton As System.Windows.Forms.Button
    Friend WithEvents DivideButton As System.Windows.Forms.Button
    Friend WithEvents ControlerTabs As System.Windows.Forms.TabControl
    Friend WithEvents Object1 As System.Windows.Forms.TabPage
    Friend WithEvents GathererControlsTab As System.Windows.Forms.TabPage
    Friend WithEvents PlantControlsTab As System.Windows.Forms.TabPage
    Friend WithEvents GlobalTimerIntervalControlerLabel As System.Windows.Forms.Label
    Friend WithEvents SetDefaultsButton As System.Windows.Forms.Button
    Friend WithEvents ClickAddPlant As System.Windows.Forms.CheckBox
    Friend WithEvents ClickAddGatherer As System.Windows.Forms.CheckBox
    Friend WithEvents GlobalTimerIntervalControler As System.Windows.Forms.TrackBar
    Friend WithEvents AddPlantDelayControler As System.Windows.Forms.TrackBar
    Friend WithEvents AddPlantDelayControlerLabel As System.Windows.Forms.Label
    Friend WithEvents InheritAmountControler As System.Windows.Forms.TrackBar
    Friend WithEvents InheritAmountControlerLabel As System.Windows.Forms.Label
    Friend WithEvents GathererHealthDownControler As System.Windows.Forms.TrackBar
    Friend WithEvents GathererHealthDownControlerLabel As System.Windows.Forms.Label
    Friend WithEvents NotesBox As System.Windows.Forms.TextBox
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Public WithEvents Info As System.Windows.Forms.ToolTip
    Friend WithEvents GathererCountLabel As System.Windows.Forms.Label
    Friend WithEvents AgeLabel As System.Windows.Forms.Label
    Friend WithEvents PlantCountLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GathererAveragesLabel As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GathererPlantRatioAveLabel As System.Windows.Forms.Label
    Friend WithEvents PlantCountAveLabel As System.Windows.Forms.Label
    Friend WithEvents GathererCountAveLabel As System.Windows.Forms.Label
    Friend WithEvents GathererPlantRatioLabel As System.Windows.Forms.Label
    Friend WithEvents PauseButton As System.Windows.Forms.Button
    Friend WithEvents RenderButton As System.Windows.Forms.Button
    Friend WithEvents FertilitySpreadControler As System.Windows.Forms.TrackBar
    Friend WithEvents FertilitySpreadControlerLabel As System.Windows.Forms.Label
    Friend WithEvents FertilityDelayControler As System.Windows.Forms.TrackBar
    Friend WithEvents FertilityDelayControlerLabel As System.Windows.Forms.Label
    Friend WithEvents ClickAddFurtilityAmountBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents ClickAddFurtility As System.Windows.Forms.CheckBox
    Friend WithEvents PlantFeedAmountControler As System.Windows.Forms.TrackBar
    Friend WithEvents PlantFeedAmountControlerLabel As System.Windows.Forms.Label
    Friend WithEvents PlantGrowResponsivenesControl As System.Windows.Forms.TrackBar
    Friend WithEvents PlantGrowResponsivenesControlLabel As System.Windows.Forms.Label
    Friend WithEvents PlantFeedDelayControler As System.Windows.Forms.TrackBar
    Friend WithEvents PlantFeedDelayControlerLabel As System.Windows.Forms.Label
    Friend WithEvents FertiliseAmountControler As System.Windows.Forms.TrackBar
    Friend WithEvents FertiliseAmountControlerLabel As System.Windows.Forms.Label

End Class
