Imports System
Imports System.Math

Public Class Environment
    Inherits System.Windows.Forms.Form

    Public Gatherers, Plants, PlantStems, PlantLeaves, OldSelectedList, NewSelectedList As New Collection
    Private SelectedPrev As Object
    Public Selected As Object
    Public Soil As Soil
    Private Visual As Visual
    Private DropGatherer As New Label
    Public Random As New Random(DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond)
    Dim alive, GathererCountChanged, PlantCountChanged As Boolean
    Public Limit, GlobalTimerInterval, GlobalGathererHealthDown, GlobalPlantGrowDelay, GlobalPlantFeedDelay As Integer
    Public DefaultLife, DefaultFieldOfVisionRadius, DefaultHealthMaxFinal, DefaultHealthPercentHigh, ShowingInfoId As Integer
    Private WorldTime, IdCounter, CreationNumber, AddPlantCounter, AddPlantDelay, RnPlantDelay, WorldTimeTimer As Integer
    Private TotalGatherers, TotalPlants, OldIndInfoIndex As Integer
    Private Ratio, TotalRatio As Double
    Public InheritAmount, GlobalPlantFeedAmount, GlobalFurtiliseAmount As Double
    Dim Notes As String


    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        MomentTick()
    End Sub

    Private Sub Environment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim s As New Soil(Me)
        Soil = s

        Dim t As New Visual(Me)
        Visual = t

        Limit = 40
        alive = False
        RnPlantDelay = Random.Next(-1, 2)
        'PrevGatherer = Nothing
        Notes = vbNewLine & "Notes:" & vbNewLine

        SetDefaults()

        'Set labels
        PlantCountLabel.Text = ""
        IndInfoBox.Visible = False
        GathererPlantRatioLabel.Text = ""
        GathererCountAveLabel.Text = ""
        PlantCountAveLabel.Text = ""
        GathererPlantRatioAveLabel.Text = ""

        SetGathererAverages()

        Visual.SetDisplay()
        Visual.LoadTextures()

        'Debug.WriteLine(Application.StartupPath)
        'AddHandler Sprite., AddressOf Timer_Tick

    End Sub

    Private Sub WorldTimeTick()
        If alive Then
            Dim XRn, YRn As Integer
            'alive = True

            If WorldTime = 0 Then
                TotalGatherers = 0
                TotalPlants = 0
                TotalRatio = 0
            End If

            FadeOutControls()

            'If alive Then
            '    If AddPlantCounter Mod AddPlantDelay = 0 Then
            '        XRn = IntRandomInclusive(1, Env.Width - 15)
            '        YRn = IntRandomInclusive(1, Env.Height - 15)
            '        AddPlant(XRn, YRn)
            '        AddPlantDelay = IntRandomInclusive(AddPlantDelayControler.Value - 1, AddPlantDelayControler.Value + 1)
            '        AddPlantCounter = 0
            '    End If
            '    AddPlantCounter += 1
            'End If

            WorldTime += 1
            AgeLabel.Text = "World age: " & WorldTime & "s"

        End If

        SetCurrent()
        SetAve()
        SetGathererAverages()
        ShowIndInfo(Selected)
    End Sub

    Sub MomentTick()
        Dim g As Gatherer
        Dim p As Plant
        Dim v, OldCount As Integer

        'Reset ticks
        For v = 1 To Gatherers.Count
            g = Gatherers.Item(v)
            g.TickFinished = False
        Next
        For v = 1 To Plants.Count
            p = Plants.Item(v)
            p.TickFinished = False
        Next

        ' --- Next moment ---
        Soil.SpreadFurtility()

        'Add plant
        If alive Then
            If AddPlantCounter Mod (AddPlantDelay + RnPlantDelay) * (1000 / Timer.Interval) = 0 Then
                AddPlant(Random.Next(0, Env.Width - 16), Random.Next(0, Env.Height - 16))
                RnPlantDelay = Random.Next(-1, 2)
                AddPlantCounter = 0
            End If
            AddPlantCounter += 1
        End If

        'Gatherers
        GathererCountChanged = False
        OldCount = Gatherers.Count
        For v = 1 To Gatherers.Count
            If OldCount = Gatherers.Count Then
                g = Gatherers.Item(v)
                If g.TickFinished = False Then
                    g.MomentTick()
                    g.TickFinished = True
                End If
            Else
                GathererCountChanged = True
            End If
        Next
        While GathererCountChanged
            GathererCountChanged = False
            OldCount = Gatherers.Count
            For v = 1 To Gatherers.Count
                If OldCount = Gatherers.Count Then
                    g = Gatherers.Item(v)
                    If g.TickFinished = False Then
                        g.MomentTick()
                        g.TickFinished = True
                    End If
                Else
                    GathererCountChanged = True
                End If
            Next
        End While

        'Plants
        PlantCountChanged = False
        OldCount = Plants.Count
        For v = 1 To Plants.Count
            If OldCount = Plants.Count Then
                p = Plants.Item(v)
                If p.TickFinished = False Then
                    p.MomentTick()
                    p.TickFinished = True
                End If
            Else
                PlantCountChanged = True
            End If
        Next
        While PlantCountChanged
            PlantCountChanged = False
            OldCount = Plants.Count
            For v = 1 To Plants.Count
                If OldCount = Plants.Count Then
                    p = Plants.Item(v)
                    If p.TickFinished = False Then
                        p.MomentTick()
                        p.TickFinished = True
                    End If
                Else
                    PlantCountChanged = True
                End If
            Next
        End While

        Visual.Render()


        If WorldTimeTimer Mod Int(1000 / GlobalTimerInterval) = 0 Then
            WorldTimeTick()
            WorldTimeTimer = 0
        End If
        WorldTimeTimer += 1

    End Sub

    Sub AddGatherer(ByVal x As Integer, ByVal y As Integer)
        If Gatherers.Count + 1 <= Limit Then
            Dim g As New Gatherer(Me, x, y, SetId(), Gatherers.Count + 1, 1, "")
            'Debug.WriteLine("Created Gatherer (id: " & IdCounter & ")")
            Gatherers.Add(g, g.Id)
            'Dim t As New Thread(AddressOf ThreadProc)

            't.Start()
            SetCurrent()
        End If
        GreyOutGathererButtons()

        'WorldTimer.Start()
        'alive = True
        AgeLabel.Text = "World age: " & WorldTime & "s"
    End Sub

    Sub AddPlant(ByVal x, ByVal y)
        Dim s As New Plant(Me, Nothing, x, y, SetId(), Plants.Count + 1)
        'Debug.WriteLine("Created a plant (id: " & IdCounter & ")")
        Plants.Add(s, s.id)

        PlantCountLabel.Text = Plants.Count

    End Sub

    Sub Divide()
        If Gatherers.Count <> 0 Then
            If Gatherers.Count * 2 <= Limit Then
                Dim i As Integer
                Dim s As Gatherer
                For i = 1 To Gatherers.Count
                    s = Gatherers.Item(i)
                    s.Divide(Gatherers, SetId(), Gatherers.Count + 1)
                Next
                Debug.WriteLine("Divided " & i - 1 & " gatherers")
                SetCurrent()
            End If
            GreyOutGathererButtons()
        End If
    End Sub

    Public Function SetId() As Integer
        IdCounter += 1
        SetId = IdCounter
    End Function

    Private Sub AddGathererButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddGathererButton.Click
        Dim RnX, RnY As Integer
        RnX = IntRandomInclusive(1, Env.Width - 15)
        RnY = IntRandomInclusive(1, Env.Height - 15)
        AddGatherer(RnX, RnY)
    End Sub

    Private Sub DivideButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DivideButton.Click
        Divide()
    End Sub

    Private Sub ResetButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetButton.Click
        Dim g As Gatherer
        While Gatherers.Count > 0
            g = Gatherers.Item(1)
            g.Remove()
        End While

        Dim p As Plant
        While Plants.Count > 0
            p = Plants.Item(1)
            'Debug.WriteLine("Deleting plant (id: " & p.id & ")")
            p.Remove()
        End While

        SetCurrent()

        DivideButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        DivideButton.ForeColor = System.Drawing.SystemColors.ControlText
        AddGathererButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        AddGathererButton.ForeColor = System.Drawing.SystemColors.ControlText
        DivideButton.Enabled = True
        AddGathererButton.Enabled = True
        'WorldTimer.Stop()
        WorldTime = 0
        ShowingInfoId = 0
        AgeLabel.Text = "World age:"
        IndInfoBox.Visible = False
        alive = False
        IdCounter = 0

        Soil.Reset()
    End Sub

    Private Sub Environment_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Dispose
        Dim g As Gatherer
        Dim p As Plant
        While Gatherers.Count <> 0
            g = Gatherers.Item(1)
            g.Dispose()
        End While
        'While Plants.Count <> 0
        '    p = Plants.Item(1)
        '    p.Dispose()
        'End While
        Visual._device.Dispose()
        Visual._sprite.Dispose()
    End Sub

    Public Sub ChangeColNumberGatherer(ByVal ColNumber As Integer)
        If ColNumber < Gatherers.Count Then

            Dim v As Integer = Gatherers.Count
            Dim g As Gatherer
            While v > ColNumber
                g = Gatherers.Item(v)
                'Debug.WriteLine("Changing ColNum of Gatherer (id: " & g.Id & ") from " & v & "/" & Gatherers.Count & " to " & v - 1 & "/" & Gatherers.Count - 1)
                v -= 1
                g.ColNum = v
            End While
        Else
            'Debug.WriteLine("Deleted. My ColNum was " & ColNumber & "/" & Gatherers.Count)
        End If
    End Sub

    Public Sub ChangeColNumberPlant(ByVal ColNumber As Integer)
        If ColNumber < Plants.Count Then
            Dim v As Integer = Plants.Count
            Dim p As Plant
            While v > ColNumber
                p = Plants.Item(v)
                'Debug.WriteLine("Changing ColNum of Plant (id: " & p.id & ") from " & v & "/" & Plants.Count & " to " & v - 1 & "/" & Plants.Count - 1)
                p.ColNum = v - 1
                v -= 1
            End While
        End If
        PlantCountLabel.Text = Plants.Count
    End Sub

    Sub ShowIndInfo(ByVal Target As Object)
        If Target Is Nothing = False Then

            Selected = Target
            IndInfoBox.Items.Clear()

            'Gatherer
            If TypeOf Target Is Gatherer Then
                ShowIndInfoGatherer(Target)
            End If

            'Plant
            If TypeOf Target Is Plant Then
                ShowIndInfoPlant(Target)
            End If

            'Soil
            If TypeOf Target Is Point Then
                ShowIndInfoSoil(Target)
            End If

        Else
            IndInfoBox.Visible = False
        End If

    End Sub

    Sub ShowIndInfoGatherer(ByVal Target As Gatherer)

        Dim i As Integer
        Dim g As Gatherer
        Selected = Target

        If TypeOf SelectedPrev Is Gatherer Then
            If SelectedPrev.Equals(Selected) = False Then
                For i = 1 To OldSelectedList.Count
                    g = OldSelectedList.Item(i)
                    g.FamilySelected = False
                Next
            End If
        Else
            SelectedPrev = Nothing
            OldSelectedList = Nothing
        End If

        'Get family
        If Target.Family Is Nothing = False Then
            For i = 1 To Target.Family.Count
                g = Target.Family.Item(i)
                If g.Alive Then
                    NewSelectedList.Add(g)
                    g.FamilySelected = True
                End If
            Next
        End If

        With Target
            IndInfoBox.Items.Add("id: " & .Id)
            IndInfoBox.Items.Add("Age: " & .Age & "s")
            IndInfoBox.Items.Add("Health: " & .Health & "/" & .HealthMax & "(" & .HealthPercent & "%)")
            IndInfoBox.Items.Add("Status: " & .Status)
            IndInfoBox.Items.Add("")
            IndInfoBox.Items.Add(.VisibleGatherersStr)
            IndInfoBox.Items.Add(.VisiblePlantsStr)
            IndInfoBox.Items.Add("")
            IndInfoBox.Items.Add("Feed: " & .PriorityFeed)
            IndInfoBox.Items.Add("Search: " & .PrioritySearch)
            IndInfoBox.Items.Add("Remember: " & .PriorityRemember)
            IndInfoBox.Items.Add("Look around: " & .PriorityLookAround)
            IndInfoBox.Items.Add("Fight: " & .PriorityFight)
            IndInfoBox.Items.Add("Flee: " & .PriorityFlee)
            IndInfoBox.Items.Add("Claim: " & .PriorityClaim)
            IndInfoBox.Items.Add("Help family fight: " & .PriorityHelpFight)
            IndInfoBox.Items.Add("Communicate: " & .PriorityCommunicate)
            IndInfoBox.Items.Add("")
            IndInfoBox.Items.Add("Gatherer " & .ColNum & " of " & Gatherers.Count)
            IndInfoBox.Items.Add("Location: (" & .x & "," & .y & ")")
            IndInfoBox.Items.Add("Generation: " & .Gen)
            If .Family Is Nothing = False Then
                IndInfoBox.Items.Add("Family: " & .Family.Count & " (id: " & Replace(.FamilyStr, ": ", "") & ")")
            End If
            IndInfoBox.Items.Add("Ave. Talk Time: " & .AveTalkTime & "s")
            If .MemorisedPlants Is Nothing = False Then
                IndInfoBox.Items.Add("Memorised Plants: " & .MemorisedPlants.Count & " (id: " & Replace(.MemorisedPlantsStr, ". ", "") & ")")
            End If
            IndInfoBox.Items.Add("Life: " & .Life & "s")
            IndInfoBox.Items.Add("Health Max Final: " & .HealthMaxFinal)
            IndInfoBox.Items.Add("Health High: " & .HealthPercentHigh & "%")
            IndInfoBox.Items.Add("Fight wins: " & .Wins & "/" & .Fights & ": " & Round((.Wins / MinNum(.Fights, 1)) * 100) & "%")
            IndInfoBox.Items.Add("Aggressiveness: " & Round(.DfAggressiveness, 1))
            IndInfoBox.Items.Add("Multiply Time: " & Round(.RnMultiplyTime / 1000, 1) & "s")
        End With

        IndInfoBox.Visible = True

        SelectedPrev = Selected
        OldSelectedList = NewSelectedList

    End Sub

    Sub ShowIndInfoPlant(ByVal Target As Plant)

        Dim i As Integer
        Dim o As Object

        If TypeOf SelectedPrev Is Gatherer Then
            For i = 1 To OldSelectedList.Count
                o = OldSelectedList.Item(i)
                o.FamilySelected = False
            Next
            SelectedPrev = Nothing
            OldSelectedList = Nothing
        End If

        With Target
            IndInfoBox.Items.Add("id: " & .id)
            IndInfoBox.Items.Add("Local id: " & .LocalId)
            IndInfoBox.Items.Add("Stem: " & .StemState)
            IndInfoBox.Items.Add("Stem id: " & .Stem.id)
            IndInfoBox.Items.Add("Age: " & .Age & "s")
            IndInfoBox.Items.Add("Health: " & Round(.Health) & "/" & .HealthMax & "(" & .HealthPercent & "%)")
            IndInfoBox.Items.Add("Status: " & .Status)
            IndInfoBox.Items.Add("")
            IndInfoBox.Items.Add("Plant " & .ColNum & " of " & Plants.Count)
            IndInfoBox.Items.Add("Cell " & .LocalColNum & " of " & .Stem.AllLeaves.Count)
            IndInfoBox.Items.Add("Location: (" & .x & "," & .y & ")")
            IndInfoBox.Items.Add("Feed Amount: " & .FeedAmount)
            IndInfoBox.Items.Add("Soil fertility: " & .FeedTargetFertility)
            IndInfoBox.Items.Add("Health Max: " & .HealthMax)
            IndInfoBox.Items.Add("Growth: " & .Growth & " (" & Round(.Grow / GlobalPlantGrowDelay * 100) & "%)")
            IndInfoBox.Items.Add("Leaves: " & .Leaves.Count)
        End With

        IndInfoBox.Visible = True
        SelectedPrev = Selected

    End Sub

    Sub ShowIndInfoSoil(ByVal Target As Point)

        Dim i As Integer
        Dim o As Object

        If TypeOf SelectedPrev Is Gatherer Then
            For i = 1 To OldSelectedList.Count
                o = OldSelectedList.Item(i)
                o.FamilySelected = False
            Next
            SelectedPrev = Nothing
            OldSelectedList = Nothing
        End If

        Dim LocArray As Point

        LocArray = Soil.FertilityArrayPosition(Target.X, Target.Y)
        Selected = Soil.FertilityRealPosition(LocArray.X, LocArray.Y)

        IndInfoBox.Items.Add("Location: (" & Selected.X & "," & Selected.Y & ")")
        IndInfoBox.Items.Add("Array Location: (" & LocArray.X & "," & LocArray.Y & ")")
        IndInfoBox.Items.Add("Fertility: " & Round(Soil.Fertility(LocArray.X, LocArray.Y), 3))

        IndInfoBox.Visible = True
        SelectedPrev = Selected
    End Sub

    '+++++ Make averages for plants and soil (have a choice of which averages to see with tabs)
    Sub SetGathererAverages()
        If alive Then
            Dim TotalGen, TotalLife, TotalHealthMaxFinal, TotalAggressiveness, TotalMultiplyTime, TotalHealthPercent, TotalHealthPercentHigh, TotalWins, TotalFights, TotalWinPercentage, TotalFamilySize, TotalMemorisedPlants As Integer

            TotalGen = 0
            TotalLife = 0
            TotalHealthMaxFinal = 0
            TotalAggressiveness = 0
            TotalMultiplyTime = 0
            TotalHealthPercent = 0
            TotalHealthPercentHigh = 0
            TotalWins = 0
            TotalFights = 0
            TotalFamilySize = 0
            TotalMemorisedPlants = 0
            TotalWinPercentage = 0

            Dim g As Gatherer
            Dim v, Count As Integer
            Dim PercentChange(2) As String
            Count = Gatherers.Count
            For v = 1 To Count
                g = Gatherers.Item(v)

                If g.Alive Then
                    TotalGen += g.Gen
                    TotalLife += g.Life
                    TotalHealthMaxFinal += g.HealthMaxFinal
                    TotalAggressiveness += g.DfAggressiveness
                    TotalMultiplyTime += g.RnMultiplyTime
                    TotalHealthPercent += g.HealthPercent
                    TotalHealthPercentHigh += g.HealthPercentHigh
                    TotalWins += g.Wins
                    TotalFights += g.Fights
                    If g.Fights <> 0 Then
                        TotalWinPercentage += (g.Wins / MinNum(g.Fights, 1)) * 100
                    End If
                    TotalFamilySize += g.Family.Count
                    TotalMemorisedPlants += g.MemorisedPlants.Count
                End If
            Next

            PercentChange(0) = Round((((TotalLife / Count) - DefaultLife) / DefaultLife) * 100, 1) & "%"
            PercentChange(1) = Round((((TotalHealthMaxFinal / Count) - DefaultHealthMaxFinal) / DefaultHealthMaxFinal) * 100, 1) & "%"
            PercentChange(2) = Round((((TotalHealthPercentHigh / Count) - DefaultHealthPercentHigh) / DefaultHealthPercentHigh) * 100, 1) & "%"

            GathererAveragesLabel.Text = "Health: " & Round(TotalHealthPercent / Count) & "%" & vbNewLine & _
                                        "Generation: " & Round(TotalGen / Count, 1) & vbNewLine & _
                                        "Family size: " & Round(TotalFamilySize / Count, 1) & vbNewLine & _
                                        "Memorised Plants: " & Round(TotalMemorisedPlants / Count, 1) & vbNewLine & _
                                        "Life: " & Round(TotalLife / Count, 1) & " (" & PercentChange(0) & ")" & vbNewLine & _
                                        "Health Max Final: " & Round(TotalHealthMaxFinal / Count, 1) & " (" & PercentChange(1) & ")" & vbNewLine & _
                                        "Health High: " & Round(TotalHealthPercentHigh / Count, 1) & "%" & " (" & PercentChange(2) & ")" & vbNewLine & _
                                        "Fight wins: " & Round(TotalWins / Count, 1) & "/" & Round(TotalFights / Count, 1) & ": " & Round(TotalWinPercentage / Count) & "%" & vbNewLine & _
                                        "Aggressiveness: " & Round(TotalAggressiveness / Count, 1) & vbNewLine & _
                                        "Multiply Time: " & Round((TotalMultiplyTime / Count) / 1000, 1) & "s"
        Else

            GathererAveragesLabel.Text = "Health: " & vbNewLine & _
                                        "Generation: " & vbNewLine & _
                                        "Family size: " & vbNewLine & _
                                        "Life: " & vbNewLine & _
                                        "Memorised Plants: " & vbNewLine & _
                                        "Health Max Final: " & vbNewLine & _
                                        "Health High: " & vbNewLine & _
                                        "Fight wins: " & vbNewLine & _
                                        "Aggressiveness: " & vbNewLine & _
                                        "Multiply Time: "
        End If
    End Sub

    Sub SetAve()
        If WorldTime > 0 Then

            TotalGatherers += Gatherers.Count
            TotalPlants += Plants.Count
            TotalRatio += Ratio

            GathererCountAveLabel.Text = Round(TotalGatherers / WorldTime, 1)
            PlantCountAveLabel.Text = Round(TotalPlants / WorldTime, 1)
            GathererPlantRatioAveLabel.Text = Round(TotalRatio / WorldTime, 1)
        Else
            GathererCountAveLabel.Text = ""
            PlantCountAveLabel.Text = ""
            GathererPlantRatioAveLabel.Text = ""
        End If
    End Sub

    Sub SetCurrent()
        'Gatherers
        GathererCountLabel.Text = Gatherers.Count & "/" & Limit

        'Plants
        PlantCountLabel.Text = Plants.Count

        'Ratio
        If Plants.Count > 0 And Gatherers.Count > 0 Then
            Ratio = Round(Gatherers.Count / Plants.Count, 1)
            GathererPlantRatioLabel.Text = Ratio
        ElseIf Plants.Count = 0 Then
            GathererPlantRatioLabel.Text = "Infinity"
        ElseIf Gatherers.Count = 0 Then
            Ratio = 0
            GathererPlantRatioLabel.Text = "0"
        Else
            GathererPlantRatioLabel.Text = ""
        End If
    End Sub

    Sub SetDefaults()

        DefaultLife = 100
        DefaultHealthMaxFinal = 27
        DefaultHealthPercentHigh = 80
        DefaultFieldOfVisionRadius = 40

        'World controls
        GlobalTimerIntervalControler.Value = 5
        AddPlantDelayControler.Value = 4
        InheritAmountControler.Value = 15
        FertilityDelayControler.Value = 10
        FertilitySpreadControler.Value = 4
        FertiliseAmountControler.Value = 10

        'Gatherer controls
        GathererHealthDownControler.Value = 25

        'Plant controls
        PlantFeedAmountControler.Value = 10
        PlantGrowResponsivenesControl.Value = 8
        PlantFeedDelayControler.Value = 4

        ChangeControls()
    End Sub

    Sub ChangeControls()
        'World controls
        GlobalTimerInterval = MinNum(GlobalTimerIntervalControler.Value, 1) * 10
        Timer.Interval = GlobalTimerInterval
        GlobalTimerIntervalControlerLabel.Text = "Timer Interal: " & GlobalTimerInterval

        AddPlantDelay = IntRandomInclusive(AddPlantDelayControler.Value - 1, AddPlantDelayControler.Value + 1)
        AddPlantDelayControlerLabel.Text = "Add Plant: " & AddPlantDelayControler.Value - 1 & "s to " & AddPlantDelayControler.Value + 1 & "s"

        InheritAmount = InheritAmountControler.Value / 20
        InheritAmountControlerLabel.Text = "Inherit Amount: " & InheritAmount

        Soil.SpreadDelay = FertilityDelayControler.Value * GlobalTimerInterval
        FertilityDelayControlerLabel.Text = "Fert. Delay: " & Soil.SpreadDelay

        Soil.Spread = FertilitySpreadControler.Value / 20
        FertilitySpreadControlerLabel.Text = "Fert. Spread: " & Soil.Spread

        GlobalFurtiliseAmount = FertiliseAmountControler.Value / 10
        FertiliseAmountControlerLabel.Text = "Fert. Amount: " & GlobalFurtiliseAmount

        'Gatherer controls
        GlobalGathererHealthDown = GathererHealthDownControler.Value * 100
        GathererHealthDownControlerLabel.Text = "Health Det.: " & GathererHealthDownControler.Value / 10 & "s"

        'Plant controls
        GlobalPlantFeedAmount = PlantFeedAmountControler.Value / 10
        PlantFeedAmountControlerLabel.Text = "Soil subt.: " & GlobalPlantFeedAmount

        GlobalPlantGrowDelay = PlantGrowResponsivenesControl.Value * 10
        PlantGrowResponsivenesControlLabel.Text = "Grow delay: " & GlobalPlantGrowDelay

        GlobalPlantFeedDelay = PlantFeedDelayControler.Value * 10
        PlantFeedDelayControlerLabel.Text = "Feed delay: " & GlobalPlantFeedDelay

    End Sub

    Sub FadeOutControls()
        If Gatherers.Count * 2 > Limit Then
            DivideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            DivideButton.ForeColor = System.Drawing.SystemColors.ControlDark
            DivideButton.Enabled = False
        End If
        If Gatherers.Count = Limit Then
            AddGathererButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            AddGathererButton.ForeColor = System.Drawing.SystemColors.ControlDark
            AddGathererButton.Enabled = False
        End If
    End Sub

    Public Function DistanceBetween(ByVal x1, ByVal y1, ByVal x2, ByVal y2) As Double
        DistanceBetween = Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1))
    End Function

    Public Function XYDifference(ByVal x1, ByVal y1, ByVal x2, ByVal y2) As Point
        XYDifference.X = x2 - x1
        XYDifference.Y = y2 - y1
    End Function

    Public Function FindClosest(ByVal x As Integer, ByVal y As Integer, ByVal Collection As Collection, ByVal ExcludeCollection As Collection) As Object
        Dim ClosestIndex As Integer = 0

        If Collection.Count > 1 Then
            Dim v As Integer
            Dim ClosestDistance, Distance As Double
            ClosestIndex = 0
            ClosestDistance = 0

            Dim target As Object

            If ExcludeCollection Is Nothing = False Then
                Dim n As Integer
                Dim ExcludeTarget As Object

                For v = 1 To Collection.Count
                    target = Collection.Item(v)
                    'Check if exluded
                    For n = 1 To ExcludeCollection.Count
                        ExcludeTarget = ExcludeCollection.Item(n)
                        If target.id <> ExcludeTarget.id Then
                            'Check if closer
                            Distance = DistanceBetween(x, y, target.x, target.y)
                            If Distance < ClosestDistance Or ClosestDistance = 0 Then
                                ClosestDistance = Distance
                                ClosestIndex = v
                            End If
                        End If
                    Next
                Next
            Else
                For v = 1 To Collection.Count
                    target = Collection.Item(v)
                    'Check if closer
                    Distance = DistanceBetween(x, y, target.x, target.y)
                    If Distance < ClosestDistance Or ClosestDistance = 0 Then
                        ClosestDistance = Distance
                        ClosestIndex = v
                    End If
                Next
            End If

        ElseIf Collection.Count = 1 Then
            ClosestIndex = 1
        Else
            ClosestIndex = 0
        End If

        If ClosestIndex <> 0 Then
            FindClosest = Collection.Item(ClosestIndex)
            'Debug.WriteLine("Picked closest: " & FindClosest.id & " from Col: " & ColIds(Collection))
        Else
            FindClosest = Nothing
        End If

    End Function

    Public Function ZeroOne(ByVal Number)
        If Number > 0 Then
            ZeroOne = 1
        Else
            ZeroOne = 0
        End If
    End Function

    Public Function MinNum(ByVal number, ByVal Min)
        If number > Min Then
            MinNum = number
        Else
            MinNum = Min
        End If
    End Function

    Public Function MaxNum(ByVal number, ByVal Max)
        If number < Max Then
            MaxNum = number
        Else
            MaxNum = Max
        End If
    End Function

    Public Function ConditionNum(ByVal Number, ByVal Condition, ByVal ValueOnCondition)
        If Number = Condition Then
            ConditionNum = ValueOnCondition
        Else
            ConditionNum = Number
        End If
    End Function

    Public Function RandomOf2(ByVal First As Double, ByVal Second As Double) As Double
        Dim r As Integer = IntRandomInclusive(0, 1)

        If r = 0 Then
            RandomOf2 = First
        Else
            RandomOf2 = Second
        End If

        'Debug.WriteLine("Returned " & RandomOf2 & ". Chose between " & First & " and " & Second)
    End Function

    Public Function UniqueValues(ByVal Array As Array, ByVal ChangeAmount As Double) As Array
        Dim v, c As Integer
        'Dim Change1, Change2 As Integer
        Dim NewArray = Array
        Dim ChangeAmountType As Boolean

        If InStr(ChangeAmount.ToString, ".") <> 0 Then
            ChangeAmountType = False
        End If

        For v = 0 To NewArray.Length - 1
            For c = 0 To NewArray.Length - 1
                If v <> c And NewArray(v) = NewArray(c) Then
                    ''Debug.WriteLine(v & " & " & c & ": " & NewArray(v) & " = " & NewArray(c))
                    'Change1 = IntRandomInclusive(-2, 2)
                    'Change2 = IntRandomInclusive(-2, 2)
                    'While Change1 = Change2
                    '    Change1 = IntRandomInclusive(-2, 2)
                    '    Change2 = IntRandomInclusive(-2, 2)
                    'End While
                    'NewArray(v) += Change1
                    'NewArray(c) += Change2
                    NewArray(v) += RandomOf2(-ChangeAmount, ChangeAmount)
                End If
            Next
        Next

        UniqueValues = NewArray
        'Debug.WriteLine("Changed " & ArrayValues(Array) & "to " & ArrayValues(UniqueValues))

    End Function

    Public Function SortedArray(ByVal Array As Array) As Array
        Dim i, v, index As Integer
        Dim NewArray(Array.Length - 1) As Double

        For i = 0 To Array.Length - 1
            index = Array.Length - 1
            For v = 0 To Array.Length - 1
                If v <> i And Array(i) > Array(v) Then
                    index = MinNum(index - 1, 0)
                End If
            Next
            NewArray(index) = Array(i)
        Next

        SortedArray = NewArray
        'Debug.WriteLine("Sorted " & ArrayValues(Array) & "to " & ArrayValues(SortedArray))

    End Function

    Public Function ArrayValues(ByVal Array As Array) As String
        Dim v As Integer
        ArrayValues = ""

        For v = 0 To Array.Length - 1
            'ArrayValues &= v & ": " & Array(v) & " "
            ArrayValues &= Array(v) & " "
        Next
    End Function

    Public Function ColIds(ByVal Col As Collection) As String
        Dim v As Integer
        Dim o As Object

        For v = 1 To Col.Count
            o = Col.Item(v)
            ColIds &= o.id & " "
        Next

    End Function

    Public Function ConvertRound(ByVal Number As Double, ByVal DecimalPlaces As Integer) As Double
        Dim DNum As Double = Number
        ConvertRound = System.Math.Round(DNum, DecimalPlaces)
    End Function

    Public Function ExcludeNumberRandom(ByVal LowestNum As Integer, ByVal HighestNum As Integer, ByVal Exclude As Integer) As Integer
        Dim n As Integer
        n = IntRandomInclusive(LowestNum, HighestNum)
        While n = Exclude
            n = IntRandomInclusive(LowestNum, HighestNum)
        End While

        ExcludeNumberRandom = n
    End Function

    Public Function ExcludeNumbersRandom(ByVal LowestNum As Integer, ByVal HighestNum As Integer, ByVal Exclude As Array) As Integer
        Dim i, n As Integer
        Dim Found As Boolean = True

        n = IntRandomInclusive(LowestNum, HighestNum)
        For i = 0 To Exclude.Length - 1
            If n = Exclude(i) Then
                Found = False
            End If
        Next
        While Found = False
            Found = True
            n = IntRandomInclusive(LowestNum, HighestNum)
            For i = 0 To Exclude.Length - 1
                If Found = True Then
                    If n = Exclude(i) Then
                        Found = False
                    End If
                End If
            Next
            'Debug.WriteLine("Checked " & n)
        End While
        'Debug.WriteLine("Set " & n)
        ExcludeNumbersRandom = n
    End Function

    Public Function IntRandomInclusive(ByVal LowestNum, ByVal HighestNum) As Integer
        IntRandomInclusive = Random.Next(LowestNum, HighestNum + 1)
    End Function

    'Collision - not used
    'Note: collision takes up too much memory atm    
    '++++++ Collision works, but since they r on a path, 
    '       they keep wanting 2 go to the same place so they keep colliding
    'Sub Collision(ByVal XTarget, ByVal YTarget)
    '    Dim MaxCollision As Integer = 50

    '    '======= Find total current overlap ==========================================
    '    Dim XTargetOverlap, YTargetOverlap As Integer

    '    XTargetOverlap = FindOverlap(XTarget, y)
    '    YTargetOverlap = FindOverlap(x, YTarget)

    '    If XTargetOverlap <= MaxCollision Then
    '        XMovementPossible = True
    '    Else
    '        XMovementPossible = False
    '    End If
    '    If YTargetOverlap <= MaxCollision Then
    '        YMovementPossible = True
    '    Else
    '        YMovementPossible = False
    '    End If
    '    If YMovementPossible = False Or XMovementPossible = False Then
    '        label.Text = "s"
    '        Status = "Overlapping... XTargetOverlap: " & XTargetOverlap & " YTargetOverlap: " & YTargetOverlap
    '    Else
    '        label.Text = "o"
    '    End If
    '    'If Overlap <= MaxCollision Then
    '    '    XMovementPossible = True
    '    '    YMovementPossible = True
    '    '    label.Text = "o"
    '    'Else
    '    '    XMovementPossible = False
    '    '    YMovementPossible = False
    '    '    ''Debug.WriteLine(Id & ": Overlapping by " & Overlap & ". Attempting to reduce...")

    '    '    label.Text = "s"

    '    '    ''======= Attempt to reduce overlap ==========================================

    '    'End If

    'End Sub

    'Note: diagonal movement is faster than others (by 0.4)


    'Function FindOverlap(ByVal XTarget, ByVal Ytarget)
    '    Dim TargetGatherer As Gatherer
    '    Dim CheckingGatherer As Integer
    '    Dim IndividualOverlap, XDiffOverlap, YDiffOverlap As Integer
    '    FindOverlap = 0
    '    IndividualOverlap = 0
    '    Overlap = 0
    '    If VisibleGatherers.Count > 1 Then
    '        For CheckingGatherer = 1 To VisibleGatherers.Count
    '            'Debug.WriteLine(Id & ": Checking on Gatherer (ColNum: " & CheckingGatherer & ")")
    '            TargetGatherer = VisibleGatherers.Item(CheckingGatherer)
    '            If TargetGatherer.Age > 0 Then
    '                XDiffOverlap = Abs(TargetGatherer.x - XTarget)
    '                YDiffOverlap = Abs(TargetGatherer.y - Ytarget)

    '                If XDiffOverlap < Width Or YDiffOverlap < Height Then
    '                    IndividualOverlap = (Width - XDiffOverlap) * (Height - YDiffOverlap)
    '                Else
    '                    IndividualOverlap = 0
    '                End If
    '                'Debug.WriteLine("     IndividualOverlap: " & IndividualOverlap)
    '                If IndividualOverlap > 0 Then
    '                    FindOverlap += IndividualOverlap
    '                    'Debug.WriteLine(Id & ": Overlapping")
    '                End If
    '                'Debug.WriteLine("     Overlaping by " & Overlap)
    '            End If
    '        Next
    '    End If
    'End Function

    'Public Sub Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    GathererLabel = sender
    '    ShowingInfoId = GathererLabel.Gatherer.Id
    '    ShowIndInfo(GathererLabel)
    'End Sub

    Sub GreyOutGathererButtons()
        If Gatherers.Count > Limit Then
            AddGathererButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            AddGathererButton.ForeColor = System.Drawing.SystemColors.ControlDark
            AddGathererButton.Enabled = False
        Else
            AddGathererButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard
            AddGathererButton.ForeColor = System.Drawing.SystemColors.ControlText
            AddGathererButton.Enabled = True
        End If

        If Gatherers.Count * 2 > Limit Then
            DivideButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            DivideButton.ForeColor = System.Drawing.SystemColors.ControlDark
            DivideButton.Enabled = False
        Else
            DivideButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard
            DivideButton.ForeColor = System.Drawing.SystemColors.ControlText
            DivideButton.Enabled = True
        End If
    End Sub

    Private Sub AddPlantButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPlantButton.Click
        Dim RnX, RnY As Integer
        RnX = IntRandomInclusive(1, Env.Width - 15)
        RnY = IntRandomInclusive(1, Env.Height - 15)
        AddPlant(RnX, RnY)
    End Sub

    Private Sub GoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoButton.Click
        'WorldTimer.Start()
        alive = True

        ClickAddGatherer.Checked = False
        ClickAddPlant.Checked = False
        ClickAddFurtility.Checked = False

        Soil.GenerateLand(10, 1000, 30, 0.4)

        If Gatherers.Count = 0 And Plants.Count = 0 Then
            Dim XRn, YRn As Integer
            For CreationNumber = 1 To 25
                XRn = IntRandomInclusive(1, Env.Width - 16)
                YRn = IntRandomInclusive(1, Env.Height - 16)
                AddPlant(XRn, YRn)
            Next
            For CreationNumber = 1 To 15
                XRn = IntRandomInclusive(1, Env.Width - 16)
                YRn = IntRandomInclusive(1, Env.Height - 16)
                AddGatherer(XRn, YRn)
            Next
        End If
    End Sub

    Private Sub SpeedControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GlobalTimerIntervalControler.Scroll
        ChangeControls()
    End Sub

    Private Sub AddPlantDelayControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPlantDelayControler.Scroll
        ChangeControls()
    End Sub

    Private Sub GathererHealthDownControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GathererHealthDownControler.Scroll
        ChangeControls()
    End Sub

    Private Sub NotesBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles NotesBox.KeyPress

        ' The keypressed method uses the KeyChar property to check 
        ' whether the ENTER key is pressed. 

        If Replace(NotesBox.Text, " ", "") <> "" Then
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
                Notes += "'" & NotesBox.Text & vbNewLine
                NotesBox.Clear()
            End If
        End If

    End Sub

    Private Sub Environment_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        If Notes <> vbNewLine & "Notes:" & vbNewLine Then
            Debug.WriteLine(Notes)
            MsgBox("Review notes.")
        End If
    End Sub

    Private Sub SetDefaultsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetDefaultsButton.Click
        SetDefaults()
    End Sub

    Private Sub InheritAmountControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InheritAmountControler.Scroll
        ChangeControls()
    End Sub

    Private Sub Env_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Env.MouseDown
        Dim g As Gatherer
        Dim p As Plant
        Dim v As Integer
        Dim FoundGatherer As Boolean = False
        Dim FoundPlant As Boolean = False

        'Gatherers
        For v = 1 To Gatherers.Count
            If FoundGatherer = False Then
                g = Gatherers.Item(v)
                If e.X >= g.x And e.X <= g.x + g.Width And e.Y >= g.y And e.Y <= g.y + g.Height Then
                    FoundGatherer = True
                End If
            End If
        Next

        'Plants
        For v = 1 To Plants.Count
            If FoundPlant = False Then
                p = Plants.Item(v)
                If e.X >= p.x And e.X <= p.x + p.Width And e.Y >= p.y And e.Y <= p.y + p.Height Then
                    FoundPlant = True
                End If
            End If
        Next


        If FoundGatherer Then
            'Gatherer
            ShowIndInfo(g)
        Else
            If FoundPlant Then
                'Plant
                ShowIndInfo(p)
            Else
                'Soil
                ShowIndInfo(New Point(e.X, e.Y))
            End If
        End If
        InfoTabs.SelectedTab = IndInfoTab

        Visual.Render()

    End Sub

    'Private Sub Environment_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    '    WorldControlsGroup.Location = New Point(4, Me.Height - 254)
    '    WorldControlsGroup.Width = Me.Width - 18

    '    IndInfoGroup.Location = New Point(Me.Width - 230, 4)
    '    IndInfoGroup.Height = Me.Height - 262

    '    WorldInfoGroup.Location = New Point(Me.Width - 450, 4)
    '    WorldInfoGroup.Height = Me.Height - 262

    '    Env.Size = New Size(New Point(Me.Width - 462, Me.Height - 258))

    '    'If Rendering = False Then
    '    '    Dim presentParams As New PresentParameters
    '    '    Dim frmHandle As IntPtr
    '    '    frmHandle = Env.Handle

    '    '    presentParams.Windowed = True
    '    '    presentParams.SwapEffect = SwapEffect.Discard
    '    '    Device = New Device(0, DeviceType.Hardware, frmHandle, CreateFlags.SoftwareVertexProcessing, presentParams)
    '    '    Sprite = New Sprite(Device)
    '    'End If
    'End Sub

    Private Sub Env_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Env.MouseUp
        If ClickAddGatherer.Checked Then
            AddGatherer(e.X - 4, e.Y - 4)
        End If
        If ClickAddPlant.Checked Then
            AddPlant(e.X - 8, e.Y - 8)
        End If
        If ClickAddFurtility.Checked Then
            Soil.ChangeCellFertility(True, e.X - 4, e.Y - 4, ClickAddFurtilityAmountBox.Value)
        End If
    End Sub

    Private Sub NotesBox_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotesBox.MouseClick
        If NotesBox.Text = "Notes" Then
            NotesBox.Text = ""
        End If
    End Sub

    Private Sub PauseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseButton.Click
        If Timer.Enabled Then
            Timer.Stop()
        Else
            Timer.Start()
        End If

        If alive Then
            alive = False
        Else
            alive = True
        End If
    End Sub

    Private Sub RenderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderButton.Click
        Visual.Render()
    End Sub

    Private Sub IndInfoIdBox_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub FertilityDelayControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FertilityDelayControler.Scroll
        ChangeControls()
    End Sub

    Private Sub FertilityDecayControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FertilitySpreadControler.Scroll
        ChangeControls()
    End Sub

    Private Sub PlantFeedAmountControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlantFeedAmountControler.Scroll
        ChangeControls()
    End Sub

    Private Sub PlantGrowResponsivenesControl_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlantGrowResponsivenesControl.Scroll
        ChangeControls()
    End Sub

    Private Sub Environment_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        If Timer.Enabled = False Then
            Visual.Render()
        End If
    End Sub

    Private Sub PlantFeedDelayControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlantFeedDelayControler.Scroll
        ChangeControls()
    End Sub

    Private Sub FertiliseAmountControler_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FertiliseAmountControler.Scroll
        ChangeControls()
    End Sub
End Class