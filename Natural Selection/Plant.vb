Imports System.Math

Public Class Plant
    '-----------------------------(Released at end)-----------------------------
    Private VisibleGatherers As New Collection
    Dim form As Environment
    Private Seed As Integer = DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond
    Public x, y, id, ColNum, HealthMax, Width, Height As Integer
    Public Health As Double
    Private AgeCounter, Speed As Integer
    Private Random As Random
    Public TickFinished, Alive As Boolean
    '-----------------------------------------------------------------------------

    'Add new vars here -------
    Public Leaves, AllLeaves As New Collection
    Public Parent, Stem As Plant
    Public HealthPercent, DeathTimer, Grow, Spread, Growth, AgeTimer, Age, LocalId, LocalColNum, LeavesMax, LocalIdCounter, LocalColNumCounter As Integer
    Dim Fed, FeedTimer As Integer
    Public FeedAmount, FeedTargetFertility, HealthSurplus As Double
    Dim FeedTarget, FeedTargetArrayPos As Point
    Public Status, LeavesStr As String
    Public StemState, Transformed, GetNewLocalColNum As Boolean
    '-------------------------

    'Creation
    Public Sub New(ByVal _form As Environment, ByVal _Parent As Plant, ByVal _x As Integer, ByVal _y As Integer, ByVal _id As Integer, ByVal ColNumber As Integer)
        Dim _Random As New Random(Seed + _id)
        Random = _Random
        form = _form
        Parent = _Parent
        Speed = form.GlobalTimerInterval
        Parent = _Parent
        form.Plants.Add(Me)
        id = _id
        'If Parent Is Nothing Then
        'form.PlantStems.Add(Me)
        'id = _id
        'LocalId = SetLocalId()
        ColNum = ColNumber
        'LocalColNum = SetLocalColNum(Me)
        StemState = True
        Stem = Me

        'Else
        '    form.PlantLeaves.Add(Me)
        '    'LocalId = _id
        '    LocalColNum = ColNumber
        '    StemState = False
        '    Stem = Parent.Stem
        'End If
        x = _x
        y = _y
        AgeCounter = 1
        Alive = True
        Transformed = True
        GetNewLocalColNum = False
        TickFinished = False
        FeedTarget = New Point(form.MaxNum(form.MinNum(x + (Width / 2), 0), form.Env.Width - 1), form.MaxNum(form.MinNum(y + (Height / 2), 0), form.Env.Height - 1))
        Width = 16
        Height = 16

        'Health
        HealthMax = 10 + IntRandomInclusive(-4, 4)
        Health = HealthMax

        ''Leaves
        'If IntRandomInclusive(0, 8) = 0 Then
        '    LeavesMax = 2
        'Else
        '    LeavesMax = 1
        'End If
        LeavesMax = 1

        'Set Defects?
    End Sub

    'Fundamental Life Processes
    Public Sub MomentTick()
        If form Is Nothing = False Then
            Speed = form.GlobalTimerInterval

            ''Transform
            'If Parent Is Nothing Then
            '    If Transformed = False Then
            '        id = form.SetId()
            '        'form.Plants.Add(Me)
            '        Stem = Me
            '        LocalColNumCounter = 0
            '        GetNewLocalColNum = True
            '        Transformed = True
            '        StemState = True
            '    End If
            'Else
            '    ' Stem = Parent.Stem
            'End If

            'If GetNewLocalColNum Then
            '    'ColNum = Stem.SetLocalColNum(Me)

            '    'Change all leaves LocalColNum
            '    If Leaves.Count <> 0 Then
            '        Dim i As Integer
            '        Dim p As Plant
            '        For i = 1 To Leaves.Count
            '            p = Leaves.Item(i)
            '            p.Stem = Stem
            '            p.GetNewLocalColNum = True
            '        Next
            '    End If

            '    GetNewLocalColNum = False
            'End If

            If Alive Then
                HealthPercent = Round(Health / HealthMax * 100)

                'Age
                If AgeTimer Mod Int(1000 / Speed) = 0 Then
                    Age += 1
                    AgeTimer = 0
                End If
                AgeTimer += 1

                'Feed
                FeedAmount = form.GlobalPlantFeedAmount * 0.0005

                FeedTargetArrayPos = form.Soil.FertilityArrayPosition(FeedTarget.X, FeedTarget.Y)
                FeedTargetFertility = form.Soil.Fertility(FeedTargetArrayPos.X, FeedTargetArrayPos.Y)

                If FeedTargetFertility <> 0 Then
                    If FeedTargetFertility < FeedAmount Then
                        FeedAmount = FeedTargetFertility
                    End If
                    form.Soil.ChangeCellFertility(False, FeedTarget.X, FeedTarget.Y, FeedAmount)
                    Health += FeedAmount
                    Status = "Growing"
                Else
                    Health -= FeedAmount
                    Status = "Starving"
                End If

                'Grow
                If Health >= HealthMax Then
                    Grow += 1
                Else
                    Grow -= 1
                End If
                If Grow >= form.GlobalPlantGrowDelay Then
                    HealthMax += 1
                    Growth += 1
                    Spread += 1
                    Grow = 0
                End If
                If Grow <= -form.GlobalPlantGrowDelay Then
                    HealthMax -= 1
                    Growth -= 1
                    Grow = 0
                End If

                'Spread
                If Spread >= 2 And Leaves.Count < LeavesMax Then
                    'NewLeaf()
                    Spread = 0
                End If

                'Health
                If Health > HealthMax Then
                    HealthSurplus += Health - HealthMax
                    Health = HealthMax
                Else
                    HealthSurplus = 0
                End If

                ''Feed neighbours
                'If HealthSurplus <> 0 Then
                '    FeedNeighbours()
                'End If

                If Health <= 0 Then
                    Alive = False
                    Death()
                End If

            Else
                Death()
            End If
        End If
    End Sub

    'Sub NewLeaf()
    '    '-----------
    '    'Make a new leaf a random distance away from self in the direction
    '    'where there is most fertility
    '    '-----------

    '    'Work out where to put new leaf - Compare fertility of adjacent soil cells

    '    'Determine which cell has the biggest value
    '    Dim r As Integer
    '    Dim AdjCellsFertility(3), Sorted(3) As Double
    '    Dim AdjCells(3), OwnCell, TargetCell As Point
    '    OwnCell = FeedTargetArrayPos

    '    'Upper cell
    '    If OwnCell.Y <> 0 Then
    '        AdjCells(0) = New Point(OwnCell.X, OwnCell.Y - 1)
    '        AdjCellsFertility(0) = form.Soil.Fertility(OwnCell.X, OwnCell.Y - 1)
    '    Else
    '        AdjCells(0) = Nothing
    '    End If

    '    'Right cell
    '    If OwnCell.X <> form.Soil.Columns Then
    '        AdjCells(1) = New Point(OwnCell.X + 1, OwnCell.Y)
    '        AdjCellsFertility(1) = form.Soil.Fertility(OwnCell.X + 1, OwnCell.Y)
    '    Else
    '        AdjCells(1) = Nothing
    '    End If

    '    'Lower cell
    '    If OwnCell.Y <> form.Soil.Rows Then
    '        AdjCells(2) = New Point(OwnCell.X, OwnCell.Y + 1)
    '        AdjCellsFertility(2) = form.Soil.Fertility(OwnCell.X, OwnCell.Y + 1)
    '    Else
    '        AdjCells(2) = Nothing
    '    End If

    '    'Left cell
    '    If OwnCell.X <> 0 Then
    '        AdjCells(3) = New Point(OwnCell.X - 1, OwnCell.Y)
    '        AdjCellsFertility(3) = form.Soil.Fertility(OwnCell.X - 1, OwnCell.Y)
    '    Else
    '        AdjCells(3) = Nothing
    '    End If

    '    If AdjCellsFertility(0) + AdjCellsFertility(1) + AdjCellsFertility(2) + AdjCellsFertility(3) <> 0 Then

    '        AdjCellsFertility = form.UniqueValues(AdjCellsFertility, 0.000001)
    '        Sorted = AdjCellsFertility
    '        Sorted = form.SortedArray(Sorted)

    '        Select Case Sorted(0)
    '            Case AdjCellsFertility(0)
    '                r = 0
    '            Case AdjCellsFertility(1)
    '                r = 1
    '            Case AdjCellsFertility(2)
    '                r = 2
    '            Case AdjCellsFertility(3)
    '                r = 3
    '        End Select

    '        TargetCell = AdjCells(r)

    '        'Randomise point
    '        Dim Point As Point = New Point(form.Soil.FertilityRealPosition(TargetCell.X, TargetCell.Y).X, form.Soil.FertilityRealPosition(TargetCell.X, TargetCell.Y).Y)
    '        Dim RnPoint As Point = New Point(form.MaxNum(form.MinNum(Point.X + IntRandomInclusive(-Width / 4, Width / 4), 0), form.Env.Width - 1), form.MaxNum(form.MinNum(Point.Y + IntRandomInclusive(-Height / 4, Height / 4), 0), form.Env.Height - 1))
    '        Dim CreationPoint As Point = RnPoint

    '        'limit closest distance between new leaf

    '        'Determine point
    '        Dim XDiff, YDiff, RnDistance As Integer
    '        Dim Distance, Multiplier As Double

    '        XDiff = FeedTarget.X - RnPoint.X
    '        YDiff = FeedTarget.Y - RnPoint.Y
    '        RnDistance = IntRandomInclusive(Width / 2, Width)

    '        If XDiff = 0 And YDiff = 0 Then
    '            XDiff = IntRandomInclusive(-Width, Height)
    '            YDiff = IntRandomInclusive(-Width, Height)

    '            Distance = form.DistanceBetween(x, y, x + XDiff, y + YDiff)
    '            Multiplier = RnDistance / Distance

    '            CreationPoint = New Point(x + (XDiff * Multiplier) + XDiff, y + (YDiff * Multiplier) + YDiff)
    '        Else
    '            Distance = form.DistanceBetween(x, y, RnPoint.X, RnPoint.Y)
    '            Multiplier = RnDistance / Distance

    '            CreationPoint = New Point(x - (XDiff * Multiplier), y - (YDiff * Multiplier))
    '        End If

    '        'Create new leaf
    '        Dim p As New Plant(form, Me, CreationPoint.X, CreationPoint.Y, form.SetId(), Stem.SetLocalColNum(Nothing))
    '        ChangeLeaves(True, p)
    '        Stem.AllLeaves.Add(p)
    '        'form.Plants.Add(p)

    '    End If

    'End Sub

    ''||||||| Unfinished
    'Sub FeedNeighbours()

    'End Sub

    Sub LookAround()

        Dim Radius As Integer = 42 'distance from center to left or top edge (square)
        Dim FieldOfVisionA As Point
        Dim FieldofVisionB As Point

        FieldOfVisionA = New Point(x - Radius, y - Radius)
        FieldofVisionB = New Point(x + Radius, y + Radius)

        Dim TargetGatherer As Gatherer
        Dim Checking As Integer

        'If Moving Then
        'Debug.WriteLine(counter & " steps in " & LookAroundTimer / 20 & "s")
        'End If

        'Reset
        While VisibleGatherers.Count > 0
            VisibleGatherers.Remove(1)
        End While

        'Gatherers
        'VisibleListStr = "Visible Gatherers (id): " & vbNewLine
        If form.Gatherers.Count > 1 Then
            For Checking = 1 To form.Gatherers.Count
                TargetGatherer = form.Gatherers.Item(Checking)
                'Check if this object is inside field of vision
                If TargetGatherer.x >= FieldOfVisionA.X And TargetGatherer.x <= FieldofVisionB.X And TargetGatherer.y >= FieldOfVisionA.Y And TargetGatherer.y <= FieldofVisionB.Y Then
                    VisibleGatherers.Add(TargetGatherer)
                    'VisibleListStr &= TargetGatherer.Id
                End If
            Next
        End If

    End Sub

    Sub Death()
        Dim DeathTime As Integer = 25000 / Speed
        Status = "Dead"

        If DeathTimer = 0 Then
            LookAround()
            'Remove self from visible gatherers' MemorisedPlants
            Dim v As Integer
            Dim g As Gatherer
            For v = 1 To VisibleGatherers.Count
                g = VisibleGatherers.Item(v)
                'Debug.WriteLine(id & ": Removing self from gatherer " & g.Id)
                g.ChangeMemorisedPlants(False, Me, 0)
                g.RemoveVisiblePlant(Me)
                g.AddRemovedPlantId(id)
            Next

            ''Remove self from leaves
            'Dim p As Plant
            'For v = 1 To Leaves.Count
            '    p = Leaves.Item(v)
            '    p.Parent = Nothing
            '    p.Transformed = False
            '    'Set LocalIdCounter
            '    p.LocalIdCounter = LocalIdCounter
            'Next
            'If Parent Is Nothing = False Then
            '    Parent.ChangeLeaves(False, Me)
            'End If
            'Stem.AllLeaves.Remove(LocalColNum)

        End If

        form.Soil.ChangeCellFertility(True, x + (Width / 2), y + (Height / 2), HealthMax / DeathTime * form.GlobalFurtiliseAmount)

        If DeathTimer >= DeathTime Then
            Remove()
        End If
        DeathTimer += 1
    End Sub

    Public Sub Remove()

        'Debug.WriteLine(id & ": Dead (" & x & "," & y & ")")
        Dispose()

        'Release
        VisibleGatherers = Nothing
        form = Nothing
        Seed = Nothing
        x = Nothing : y = Nothing : id = Nothing : ColNum = Nothing : Health = Nothing
        AgeCounter = Nothing : Width = Nothing : Height = Nothing : Speed = Nothing : HealthMax = Nothing
        Random = Nothing
        Alive = Nothing
        TickFinished = Nothing

    End Sub

    Public Sub Dispose()
        'Stem.AllLeaves.Remove(LocalColNum)

        'If StemState Then
        If ColNum <> 0 Then
            form.ChangeColNumberPlant(ColNum)
            form.Plants.Remove(ColNum)
        End If
        'End If

    End Sub

    'Public Sub ChangeLeaves(ByVal Add As Boolean, ByVal Target As Plant)
    '    If Target Is Nothing = False Then

    '        Dim Changed As Boolean = False
    '        Dim v As Integer
    '        Dim p As Plant
    '        If Add Then
    '            If InStr(LeavesStr, Target.LocalId & ":") = 0 Then
    '                Leaves.Add(Target)
    '                Changed = True
    '            End If
    '        Else
    '            If InStr(LeavesStr, Target.LocalId & ":") <> 0 Then
    '                Dim RemoveIndex As Integer
    '                For v = 1 To Leaves.Count
    '                    If Changed = False Then
    '                        p = Leaves.Item(v)
    '                        If p.LocalId = Target.LocalId Then
    '                            RemoveIndex = v
    '                        End If
    '                    End If
    '                Next
    '                Leaves.Remove(RemoveIndex)
    '                Changed = True
    '            End If
    '        End If

    '        If Changed Then
    '            LeavesStr = ""
    '            If Leaves.Count <> 0 Then
    '                For v = 1 To Leaves.Count
    '                    p = Leaves.Item(v)
    '                    LeavesStr &= p.LocalId & ":"
    '                Next
    '                LeavesStr &= " "
    '            End If
    '        End If

    '    End If
    'End Sub

    'Public Function SetLocalId() As Integer
    '    LocalIdCounter += 1
    '    SetLocalId = LocalIdCounter
    'End Function

    'Public Function SetLocalColNum(ByVal sender As Plant) As Integer
    '    If sender Is Nothing = False Then
    '        AllLeaves.Add(sender)
    '    End If
    '    LocalColNumCounter += 1
    '    SetLocalColNum = LocalColNumCounter
    'End Function

    Function IntRandomInclusive(ByVal LowestNum, ByVal HighestNum) As Integer
        IntRandomInclusive = Random.Next(LowestNum, HighestNum + 1)
    End Function

End Class