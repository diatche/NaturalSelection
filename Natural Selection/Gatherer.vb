'Imports System.Threading
Imports System.Math
Imports System

Public Class Gatherer
    '-----------------------------(Released at end)-----------------------------
    Private VisibleGatherers, VisibleFamily, VisibleEnemy, VisibleFamilyFighting, VisibleFamilyNormal, VisiblePlants As New Collection
    Public Family, MemorisedPlants, Attackers As New Collection
    Dim form As Environment
    Dim Random As Random
    Dim OldFriendlyTarget As Gatherer
    Public FightTarget, FriendlyTarget As Gatherer
    Dim TargetPlant As Plant
    Public x, y As Single

    'Point
    Public FightArena, FriendlyArena As Point
    Private MemorisedPlantsPosition(1), FleePoint, FocusPoint As Point

    'String
    Public Action, Info, Status, FamilyStr, MemorisedPlantsStr, AttackersStr As String
    Private NewStatus, OldPriority, OldStatus, DNA As String

    'Double
    Public DfX, DfY, DfPlusMinus, DfSpeed, DfAggressiveness, RnAggressiveness, AveTalkTime As Double
    Private YXRatio As Double

    'Integer  ++++++ sort
    Private DfParentLife, DfParentHealthMaxFinal, DfParentHealthPercentHigh, DfParentMultiplyTime, DfParentAggressiveness As Integer
    Private Seed, FieldOfVisionRadius, AgeCounter, TalkTime(1), RemovedPlantsId(3), NextRemovedPlantsIdIndex, MoveRandomTimer, RnDivideProcessTime, DfMultiplyTime, DfHealthPercentHigh, Attacker As Integer
    Private RnMoveWait, VisibleMemorisedPlantsNumber, RnLookAroundTime, OldMemorisedPlantsCount, SendInfoCount, NewMemorisedPlantsCount, Overlap, LookAroundTimer, LookAroundTime, HealthTimer, Speed, MultiplyTimer, AttackThrowTimer, RnAttackThrowTime, FightTargetCounter, RnPlant As Integer
    Private RnX, RnY, XSkip, YSkip, XNew, YNew, XSearch, YSearch, ClosestIndex, ClosestKnownPlantIndex, DeathTimer, FeedTimer, AggressivenessTimer As Integer
    Public Gen, Id, ColNum, Age, Life, HealthMax, HealthMaxFinal, Health, HealthPercent, HealthPercentHigh, DfLife, DfHealthMaxFinal, RnMultiplyTime, Wins, Fights, TargetFamilyIndex, SendInfoIndex As Integer
    Public PriorityFeed, PrioritySearch, PriorityFight, PriorityClaim, PriorityRemember, PriorityFlee, PriorityLookAround, PriorityHelpFight, PriorityCommunicate As Integer
    Public Width, Height As Integer

    'Boolean
    Private HealthHigh, HealthMaxed, Young, Moving, XMovementPossible, YMovementPossible, XAvoidingOverlap, YAvoidingOverlap, LookingAround As Boolean
    Public Alive, Dividing As Boolean

    '-----------------------------------------------------------------------------

    'Add new vars here -------
    Public Tails, Facing, Reaching, AnimationTimer, RnFeedTime As Integer
    Public TailPosition(12), ReachingTexLocation As Point
    Private OldFacing As Integer
    Public TickFinished, FamilySelected, Hit As Boolean
    Private Turning As Boolean
    Public VisibleGatherersStr, VisiblePlantsStr As String
    '-------------------------

    'Creation
    Public Sub New(ByVal _form As Environment, ByVal _x As Integer, ByVal _y As Integer, ByVal _id As Integer, ByVal ColNumber As Integer, ByVal Generation As Integer, ByVal ParentDNA As String)

        'Non visual
        Seed = DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond
        Dim _Random As New Random(Seed + _id)
        Random = _Random
        form = _form
        Speed = form.GlobalTimerInterval
        x = _x
        y = _y
        Id = _id
        AggressivenessTimer = 5
        Gen = Generation
        ColNum = ColNumber
        HealthPercent = 100
        LookAroundTime = 1000
        FieldOfVisionRadius = form.DefaultFieldOfVisionRadius
        AgeCounter = 1
        Alive = True
        FamilySelected = False
        TickFinished = False
        XMovementPossible = True
        YMovementPossible = True
        Moving = False
        Turning = False
        Dividing = False
        VisibleGatherersStr = ""
        VisiblePlantsStr = ""
        Width = 8
        Height = 8
        SendInfoIndex = 1
        Facing = 1
        XSearch = x
        YSearch = y

        NextRandom(RnLookAroundTime)
        NextRandom(RnMoveWait)
        NextRandom(RnMultiplyTime)
        NextRandom(RnDivideProcessTime)
        NextRandom(RnAttackThrowTime)
        NextRandom(RnFeedTime)

        InheritParentDNA(ParentDNA)
        SetDNA()

        'AddHandler Texture., AddressOf Timer_Tick
    End Sub

    'Fundamental Life Processes
    Public Sub MomentTick()
        Speed = form.GlobalTimerInterval
        Reaching = 0
        Hit = False

        If Alive Then
            'Age
            AgeCounter += 1
            If AgeCounter Mod Int(1000 / Speed) = 0 Then
                Age += 1
                AgeCounter = 0
            End If

            'Health
            HealthTimer += 1
            If HealthTimer Mod Int(form.GlobalGathererHealthDown / Speed) = 0 Then
                Health -= 1
                HealthTimer = 0
                HealthPercent = Round(Health / HealthMax * 100)
            End If
            If HealthPercent < HealthPercentHigh Then
                HealthHigh = False
                HealthMaxed = False
            Else
                HealthHigh = True
            End If

            'Grow (Simple, no inheritance)
            If Age = Int(Life / 10) Then
                If AgeCounter = 0 Then
                    HealthMax = HealthMaxFinal / 2.7
                    AddTail()
                    Young = False
                End If
            ElseIf Age = Int(Life / 5) Then
                If AgeCounter = 0 Then
                    HealthMax = HealthMaxFinal / 2
                    AddTail()
                End If
            ElseIf Age = Int(Life * (10 / 3)) Then
                If AgeCounter = 0 Then
                    HealthMax = HealthMaxFinal / 1.35
                    AddTail()
                End If
            ElseIf Age = Int(Life * (10 / 9)) Then
                If AgeCounter = 0 Then
                End If
            End If

            'Multiply
            MultiplyTimer += 1
            If Dividing = True Then
                If MultiplyTimer Mod Int(RnDivideProcessTime / Speed) = 0 Then
                    Divide(form.Gatherers, form.SetId(), form.Gatherers.Count + 1)
                    Status = ""
                    'label.Text = "o"
                    Dividing = False
                    NextRandom(RnDivideProcessTime)
                    MultiplyTimer = 0
                    'Debug.WriteLine(Id & ": Finished dividing. MultiplyTimer: " & MultiplyTimer)
                End If
            Else
                If MultiplyTimer Mod Int(RnMultiplyTime / Speed) = 0 Then
                    Status = "Dividing"
                    'Debug.WriteLine(Id & ": Dividing. MultiplyTimer: " & MultiplyTimer)
                    'label.Text = "8"
                    Dividing = True
                    NextRandom(RnMultiplyTime)
                    RnMultiplyTime += DfMultiplyTime
                    MultiplyTimer = 0
                End If
            End If

            If Dividing = False Then
                'DebugItem("Flee")
                AI()
            End If

            'Death
            If Age = Life Or Health <= 0 Then
                Alive = False
                'label.Text = "c"
                Status = "Dead"
                Death()
                'Debug.WriteLine(Id & ": Died with ColNum " & ColNum & "/" & form.Gatherers.Count)
            End If

        Else
            Death()
        End If

        'TickFinished = True
        'Debug.WriteLine(Id & ": Tick finished")
    End Sub

    '++++++ Make feed priority dependent on visible food not visible plants
    Sub AI()
        '==== Set priorities ====
        Dim PriorityList(8) As Double

        'PriorityFeed: health, visible plants?, visible gatherers?
        If HealthMaxed Then
            PriorityFeed = (100 - HealthPercent) * 2
            'Debug.WriteLine(Id & ": Health maxed")
        Else
            If Action = "Feeding" Or Action = "Going to feed" Then
                PriorityFeed = 150
            ElseIf HealthHigh = False Then
                PriorityFeed = 150
            End If
        End If
        PriorityFeed *= form.ZeroOne(VisiblePlants.Count + MemorisedPlants.Count)
        PriorityList(0) = PriorityFeed

        'PrioritySearch: visible gatherers
        PrioritySearch = 100 - ((VisiblePlants.Count + MemorisedPlants.Count) * 15) '100 / form.MinNum((VisiblePlants.Count + MemorisedPlants.Count) / 2, 1)
        PriorityList(1) = PrioritySearch

        'PriorityRemember: unknown plants
        PriorityRemember = form.ZeroOne(VisiblePlants.Count - VisibleMemorisedPlantsNumber) * 190
        PriorityList(2) = PriorityRemember

        'PriorityLookAround: timer
        PriorityLookAround = LookAroundTime * Speed / 15 'form.MaxNum((LookAroundTime * Speed) / 10, 150)
        PriorityList(3) = PriorityLookAround
        LookAroundTime += 1

        'PriorityFight: health, visible gatherers
        If (Action = "Fighting" Or Action = "Going to fight") And FightTarget Is Nothing = False Then
            PriorityFight = HealthPercent * 3 * form.MinNum(VisibleFamily.Count, 1) / form.MinNum(Attackers.Count, 1) + DfAggressiveness
        Else
            PriorityFight = 0
        End If
        PriorityList(4) = PriorityFight

        'PriorityFlee: health, visible gatherers
        'If Action = "Fighting" Or Action = "Going to fight" Or Action = "Fleeing" Then
        If Action = "Fleeing" And FightTarget Is Nothing = False Then
            PriorityFlee = 500
        Else
            PriorityFlee = (((100 - HealthPercent) * 3) / form.MinNum(VisibleFamily.Count, 1) - DfAggressiveness) * form.ZeroOne(Attackers.Count)  ' * form.ZeroOne(VisibleGatherers.Count)
        End If
        'Else
        'PriorityFlee = 0
        'End If
        PriorityList(5) = PriorityFlee

        'PriorityClaim
        PriorityClaim = ((HealthPercent * 2 / form.MinNum(VisibleEnemy.Count, 1)) + DfAggressiveness) * form.ZeroOne(VisiblePlants.Count) * form.ZeroOne(VisibleEnemy.Count)
        PriorityList(6) = PriorityClaim

        'PriorityHelpFight
        PriorityHelpFight = (HealthPercent * 2 * VisibleFamily.Count + DfAggressiveness) * form.ZeroOne(VisibleFamilyFighting.Count)
        PriorityList(7) = PriorityHelpFight

        'PriorityCommunicate
        Dim i As Integer
        If Family.Count <> 0 Then
            Dim TotalTalkTime As Integer = 0
            For i = 1 To TalkTime.Length - 1
                If TalkTime(i) <> 0 Then
                    TalkTime(i) += 1
                    TotalTalkTime += TalkTime(i)
                End If
            Next
            AveTalkTime = Round((TotalTalkTime / (TalkTime.Length - 1)) * Speed / 1000, 1)
        Else
            AveTalkTime = 0
        End If
        If Action <> "Fighting" And Action <> "Going to fight" Then
            If Action = "Communicating" Or Action = "Going to communicate" Then
                If FriendlyTarget.Action = "Communicating" Or FriendlyTarget.Action = "Going to communicate" Then
                    PriorityCommunicate = 140
                Else
                    PriorityCommunicate = form.MaxNum(MemorisedPlants.Count * form.ConditionNum(AveTalkTime, 0, 12) * 5 * form.ZeroOne(VisibleFamilyNormal.Count), 140)
                End If
            Else
                PriorityCommunicate = form.MaxNum(MemorisedPlants.Count * form.ConditionNum(AveTalkTime, 0, 12) * 5 * form.ZeroOne(VisibleFamilyNormal.Count), 140)
            End If
        Else
            PriorityCommunicate = 0
        End If
        PriorityList(8) = PriorityCommunicate


        '(process)
        PriorityList = form.UniqueValues(PriorityList, 1) 'Makes Array values unique
        PriorityList = form.SortedArray(PriorityList) 'Sort

        'If OldPriority <> PriorityList(0) Then
        '    Animation = 1
        '    OldPriority = PriorityList(0)
        'End If

        '==== Set action ====
        Select Case PriorityList(0)
            Case PriorityFeed
                Status = "Feeding"
                Feed()
            Case PrioritySearch
                Status = "Searching"
                search()
            Case PriorityRemember
                Status = "Remembering"
                Remember()
            Case PriorityLookAround
                Status = "Looking around"
                LookAround("AI: General")
            Case PriorityFight
                Status = "Fighting"
                Fight(FightTarget)
            Case PriorityFlee
                Status = "Fleeing"
                Flee()
            Case PriorityClaim
                Status = "Claiming"
                Claim()
            Case PriorityHelpFight
                Status = "Helping family fight"
                HelpFamilyFight()
            Case PriorityCommunicate
                Status = "Communicating"
                Communicate()
        End Select
    End Sub

    Sub LookAround(ByVal Reason As String)

        If LookingAround = False Then

            'Each second look for objects in the field of vision and place them into a list.
            'Use this list whenever interacting, to avoid having 2 chek ALL the objects.

            'Note: at speed = 50, 20 steps/s are possible so field of vision needs to be at least 40 steps (pixels) each way
            'Debug.WriteLine(Id & ": LookAroundTime: " & LookAroundTime)

            'Dim FieldOfVisionA, FieldofVisionB As Point

            'FieldOfVisionA = New Point(x - FieldOfVisionRadius, y - FieldOfVisionRadius)
            'FieldofVisionB = New Point(x + FieldOfVisionRadius, y + FieldOfVisionRadius)

            LookingAround = True

            Dim _TargetGatherer As Gatherer
            Dim _TargetPlant As Plant
            Dim Checking As Integer
            Dim GathererIdStr, PlantIdStr As String

            'Reset
            VisibleGatherers.Clear()
            VisibleFamily.Clear()
            VisibleEnemy.Clear()
            VisibleFamilyFighting.Clear()
            VisibleFamilyNormal.Clear()
            VisiblePlants.Clear()

            GathererIdStr = ""
            VisibleMemorisedPlantsNumber = 0

            'Gatherers
            If form.Gatherers.Count > 1 Then
                For Checking = 1 To form.Gatherers.Count
                    If Checking <> ColNum Then
                        _TargetGatherer = form.Gatherers.Item(Checking)
                        If _TargetGatherer.Alive Then
                            'Check if this object is inside field of vision
                            If form.DistanceBetween(x, y, _TargetGatherer.x, _TargetGatherer.y) <= FieldOfVisionRadius Then
                                VisibleGatherers.Add(_TargetGatherer)
                                GathererIdStr &= _TargetGatherer.Id

                                ''Check if gatherer is feeding or going to feed
                                'If _TargetGatherer.Action = "Feeding" Or _TargetGatherer.Action = "Going to feed" Then
                                '    VisibleFeedingGatherers.Add(_TargetGatherer)
                                '    'GathererIdStr &= "(e)"
                                'End If

                                'Check if gatherer is family
                                If InStr(FamilyStr, _TargetGatherer.Id & ":") <> 0 Then
                                    VisibleFamily.Add(_TargetGatherer)
                                    GathererIdStr &= "*"

                                    'Check if gatherer is fighting
                                    If _TargetGatherer.Action = "Fighting" Or _TargetGatherer.Action = "Going to fighting" Then
                                        VisibleFamilyFighting.Add(_TargetGatherer)
                                    Else
                                        VisibleFamilyNormal.Add(_TargetGatherer)
                                    End If
                                Else
                                    VisibleEnemy.Add(_TargetGatherer)

                                    'Check if gatherer is an attacker
                                    If InStr(AttackersStr, _TargetGatherer.Id & ".") <> 0 Then
                                        GathererIdStr &= "!"
                                    End If

                                    'Check if gatherer is fighttarget
                                    If FightTarget Is Nothing = False Then
                                        If _TargetGatherer.Id = FightTarget.Id Then
                                            GathererIdStr &= "@"
                                        End If
                                    End If

                                    End If
                                    GathererIdStr &= " "
                            End If
                        End If
                    End If
                Next
            End If
            PlantIdStr = ""

            'Plants
            If form.Plants.Count > 0 Then
                For Checking = 1 To form.Plants.Count
                    _TargetPlant = form.Plants.Item(Checking)
                    If _TargetPlant.Alive = True Then
                        'Check if this object is inside field of vision
                        If form.DistanceBetween(x, y, _TargetPlant.x, _TargetPlant.y) <= FieldOfVisionRadius Then
                            VisiblePlants.Add(_TargetPlant)
                            PlantIdStr &= _TargetPlant.id

                            'Check if plant is MemorisedPlants
                            If InStr(MemorisedPlantsStr, _TargetPlant.id & ".") <> 0 Then
                                'PlantIdStr &= "(m)"
                                VisibleMemorisedPlantsNumber += 1
                            End If
                            PlantIdStr &= " "
                        End If
                    End If
                Next
            End If
            VisibleGatherersStr = "Visible Gatherers: " & VisibleGatherers.Count & " (id: " & GathererIdStr & ")"
            VisiblePlantsStr = "Visible Plants: " & VisiblePlants.Count & " (id: " & PlantIdStr & ")"

            'Debug.WriteLine(Id & ": Looking around. Reason: " & Reason & ". Action: " & Action)
            'Debug.WriteLine("   Visible Gatherers: " & VisibleGatherers.Count)
            'Debug.WriteLine("   Visible Plants: " & VisiblePlants.Count & " (" & VisibleMemorisedPlantsNumber & " MemorisedPlants)")

            ''====== Set Aggressiveness =======================================

            ''If AggressivenessTimer Mod 5 = 0 Then
            'If VisiblePlants.Count > 0 Then
            '    RnAggressiveness = (((Random.NextDouble + 0.01) * 2) + ((VisibleGatherers.Count * 2) / VisiblePlants.Count)) + DfAggressiveness
            'Else
            '    '1 plant visible assumed
            '    RnAggressiveness = ((Random.NextDouble + 0.01) * 2) + (VisibleGatherers.Count * 2) + DfAggressiveness
            'End If
            ''AggressivenessTimer = 0
            ''End If
            ''AggressivenessTimer += 1

            LookAroundTimer = 0
        Else
            If Facing >= 8 Then
                Facing = 1
            Else
                Facing += 1
            End If
            'Debug.WriteLine(Id & ": Changed direction to " & Facing & ". Looking around")

            If LookAroundTimer Mod RnLookAroundTime = 0 Then
                LookingAround = False
                LookAroundTimer = 0
                LookAroundTime = 0
                NextRandom(RnLookAroundTime)
            End If
        End If
        LookAroundTimer += 1

    End Sub

    '++++++ needs improving
    Sub search()
        'Debug.WriteLine(Id & ": Searching. Action: " & Action)
        If Action <> "Searching" Then
            XSearch = IntRandomInclusive(0, form.Env.Width - Width)
            YSearch = IntRandomInclusive(0, form.Env.Height - Height)
            'Debug.WriteLine(Id & ": Random coords changed")
        End If
        Action = "Searching"
        MoveTo(XSearch, YSearch, 0)
        If x = XSearch And y = YSearch Then
            Action = ""
        End If
    End Sub

    '|||||| Disabled selection between multiple visible plants
    Sub Feed()
        Dim p As Plant
        If VisiblePlants.Count = 0 Then
            If MemorisedPlants.Count > 0 Then
                Dim Point As Point
                If Action <> "Going to remembered plant" Then
                    'See which known plant is closest
                    ClosestKnownPlantIndex = FindClosestMemorisedPlantsIndex()
                    NewMemorisedPlantsCount = MemorisedPlants.Count
                    'Debug.WriteLine(Id & ": Found closest remembered plant. Index: " & ClosestKnownPlantIndex & ". Action: " & Action)
                End If
                'Move to plant
                If ClosestKnownPlantIndex <= MemorisedPlantsPosition.Length - 1 Then
                    Point = MemorisedPlantsPosition(ClosestKnownPlantIndex)
                    Action = "Going to remembered plant"
                    MoveTo(Point.X, Point.Y, 0)
                    ReachTowards(Point.X - x + 8, Point.Y - y + 8)
                    'Debug.WriteLine(Id & ": Moving to " & Point.ToString)
                End If

                If form.DistanceBetween(x, y, Point.X, Point.Y) < (FieldOfVisionRadius / 2) And OldMemorisedPlantsCount = NewMemorisedPlantsCount Then
                    ChangeMemorisedPlants(False, Nothing, ClosestKnownPlantIndex)
                    Action = ""
                    TargetPlant = Nothing
                    'Debug.WriteLine(Id & ": Removed closest remembered plant. Index: " & ClosestKnownPlantIndex & ". Action: " & Action)
                End If
                OldMemorisedPlantsCount = NewMemorisedPlantsCount
            End If
        Else
            If Action = "Going to remembered plant" Or Action = "Searching" Then
                Action = ""
            End If

            'If there are plants visible
            If VisiblePlants.Count > 1 Then
                If Action <> "Feeding" And Action <> "Going to feed" Then
                    p = form.FindClosest(x, y, VisiblePlants, Nothing) '(IntRandomInclusive(1, VisiblePlants.Count))
                    TargetPlant = p
                End If
                FeedOn(TargetPlant)
            Else
                p = VisiblePlants.Item(1)
                TargetPlant = p
                'Debug.WriteLine(Id & ": Moving to one (id: " & p.id & ")")
                FeedOn(p)
            End If
            'Debug.WriteLine(Id & ": Feeding on (id: " & p.id & ")")

        End If
    End Sub

    Sub FeedOn(ByVal Target As Plant)
        If Target Is Nothing = False Then
            TargetPlant = Target
            'Debug.WriteLine(Id & ": About to go to feed. Action: " & Action)

            MoveTo(Target.x, Target.y, 6)
            ReachTowards(Target.x - x + 8, Target.y - y + 8)

            If form.DistanceBetween(x, y, Target.x, Target.y) < 7 Then
                If Health >= HealthMax Then
                    HealthMaxed = True
                End If

                If Target.Health <= 0 Or Health >= HealthMax Then
                    'Debug.WriteLine(Id & ": Reseting")
                    Status = ""
                    Action = ""
                    TargetPlant = Nothing
                    Moving = False
                    'LookAround("Finished feeding")
                Else
                    Action = "Feeding"
                    'Debug.WriteLine(Id & ": Feeding")
                    If FeedTimer Mod Int(RnFeedTime / Speed) = 0 Then
                        Target.Health -= 1
                        Health += 1
                        FeedTimer = 0

                        NextRandom(RnFeedTime)
                    End If
                    FeedTimer += 1
                End If
            Else
                'Debug.WriteLine(Id & ": Going to feed")
                Action = "Going to feed"
            End If
        Else
            Action = ""
            TargetPlant = Nothing
            Moving = False
            LookAround("FeedOn: Lost target plant")
        End If
    End Sub

    Sub Claim()
        Dim v As Integer
        Dim g As Gatherer
        Dim Found As Boolean = False
        For v = 1 To VisibleEnemy.Count
            If v <= VisibleEnemy.Count Then
                If Found = False Then
                    g = VisibleEnemy.Item(v)
                    'Keep attacking until there are no more
                    TargetPlant = form.FindClosest(x, y, VisiblePlants, Nothing)

                    If form.DistanceBetween(g.x, g.y, TargetPlant.x, TargetPlant.y) < FieldOfVisionRadius - 5 And g.Action <> "Fleeing" Then
                        'Debug.WriteLine(Id & ": Targeting gatherer id: " & g.Id)
                        Found = True
                        Fight(g)
                    End If
                End If
            Else
                FightTarget = Nothing
                Found = True
                Action = ""
                FightArena = Nothing
                'LookAround("Claim: Error in picking target")
            End If
        Next

        If Found = False Then
            FightTarget = Nothing
            Action = ""
            FightArena = Nothing
            'LookAround("Claim: No proper targets found")
        End If
    End Sub

    '||||| TurnTowards disabled
    Sub Remember()
        Dim v As Integer
        Dim p As Plant
        For v = 1 To VisiblePlants.Count
            p = VisiblePlants.Item(v)
            If InStr(MemorisedPlantsStr, p.id & ".") = 0 Then
                'turn towards plant
                Dim Dif As Point = form.XYDifference(x, y, p.x, p.y)
                'TurnTowards(Dif.X, Dif.Y)
                'If Turning = False Then

                'Add plant to MemorisedPlants
                ChangeMemorisedPlants(True, p, 0)

                'End If
            End If
        Next
        LookAround("Remember: remembered all plants")
    End Sub

    Sub Communicate()
        If VisibleFamilyNormal.Count <> 0 Then
            Dim i As Integer = 1

            If Action = "Communicating" Or Action = "Going to communicate" Then
                If AveTalkTime <> 0 Then
                    If OldFriendlyTarget.Id <> FriendlyTarget.Id Then
                        SendInfoIndex = 1
                        'Debug.WriteLine(Id & ": Index reset")
                    End If
                End If
                OldFriendlyTarget = FriendlyTarget
            Else
                'Pick target
                If VisibleFamilyNormal.Count > 1 Then
                    i = IntRandomInclusive(1, VisibleFamilyNormal.Count)
                End If
                FriendlyTarget = VisibleFamilyNormal.Item(i)
            End If

            CommunicateWith(FriendlyTarget)
        End If
    End Sub

    Sub CommunicateWith(ByVal Target As Gatherer)

        'Move to target
        If Action <> "Communicating" Then
            MoveTo(Target.x, Target.y, 0)
            Action = "Going to communicate"
        End If

        If Action = "Going to communicate" And form.DistanceBetween(x, y, Target.x, Target.y) <= FieldOfVisionRadius / 2 Then
            Action = "Communicating"
            FriendlyArena = New Point(Target.x, Target.y)

            'Determine which family gatherer the target is
            TargetFamilyIndex = FindFamilyIndex(Target)

            'Let the target know of the communication
            If Target.Action <> "Communicating" Or Target.Action <> "Going to communicate" Then
                Target.Action = "Communicating"
                Target.FriendlyTarget = Me
                Target.FriendlyArena = FriendlyArena
                Target.TargetFamilyIndex = Target.FindFamilyIndex(Me)
                'Debug.WriteLine(Id & ": Trying to communicate with family id: " & Target.Id)
            End If
        End If

        If SendInfoIndex <= MemorisedPlants.Count Then
            If Action = "Communicating" Then
                MoveTo(Target.x, Target.y, 7)

                'Send memorised plants to target
                If SendInfoCount Mod Int(1000 / Speed) = 0 Then
                    Dim p As Plant
                    p = MemorisedPlants.Item(SendInfoIndex)
                    'Debug.WriteLine(Id & ": Sending info about plant id: " & p.id & " to family id: " & Target.Id & ". " & SendInfoIndex & "/" & MemorisedPlants.Count)
                    If Target.RemovedPlant(p.id) = False Then
                        Target.ChangeMemorisedPlants(True, p, Nothing)
                        SendInfoIndex += 1
                    Else
                        'Debug.WriteLine(Id & ": Found out that plant id: " & p.id & " is gone. Old str: " & MemorisedPlantsStr)
                        ChangeMemorisedPlants(False, p, Nothing)
                    End If
                    SendInfoCount = 0
                End If
                SendInfoCount += 1
            End If
        Else
            If FriendlyTarget.SendInfoIndex <= FriendlyTarget.MemorisedPlants.Count Then
                Action = ""
                'Target.FriendlyTarget = Nothing
                'Target.FriendlyArena = Nothing
                SendInfoIndex = 1
                If TargetFamilyIndex <= TalkTime.Length - 1 Then
                    TalkTime(TargetFamilyIndex) = 1
                End If
                'Debug.WriteLine(Id & ": Finished communicating. SendInfoIndex: " & SendInfoIndex & "/" & MemorisedPlants.Count)
                LookAround("CommunicateWith: Finished communicating")
            Else
                MoveTo(FriendlyArena.X, FriendlyArena.Y, 7)
            End If
        End If
    End Sub

    Sub Fight(ByVal Target As Gatherer)
        If Target Is Nothing = False Then

            Dim FightEnd As Boolean = False
            Dim PerformAttack As Boolean = False
            Dim Distance As Double = form.DistanceBetween(Target.x, Target.y, FightArena.X, FightArena.Y)

            If Target.Health > 0 Then
                Dim FightArenaBounds As Integer = 7
                FightTarget = Target

                'Set a place to fight (to determine if the other one flees)
                If Action <> "Fighting" Then 'Or (Action = "Fighting" And form.DistanceBetween(x, y, Target.x, Target.y) >= FieldOfVisionRadius / 4) Then
                    If form.DistanceBetween(x, y, Target.x, Target.y) < FieldOfVisionRadius / 2 Then 'x = Target.x And y = Target.y Then '
                        FightArena = New Point(Target.x, Target.y)
                        'If Target.x >= FightArena.X - FightArenaBounds And Target.x <= FightArena.X + FightArenaBounds And Target.y >= FightArena.Y - FightArenaBounds And Target.y <= FightArena.Y + FightArenaBounds Then
                        '    Fighting = True
                        'End If
                        Action = "Fighting"
                    Else
                        'Move to target
                        Action = "Going to fight"
                        MoveTo(Target.x, Target.y, 0)
                        ReachTowards(Target.x - x + (Width / 2), Target.y - y + (Width / 2))
                    End If
                Else
                    'Debug.WriteLine(Id & ": Moving around")
                    MoveTo(FightArena.X, FightArena.Y, FightArenaBounds)
                    ReachTowards(Target.x - x + (Width / 2), Target.y - y + (Width / 2))
                End If
                If Action = "Fighting" Then
                    'Make the target know its being attacked
                    Target.ChangeAttackers(True, Me)
                    If Target.Action <> "Fighting" And Target.Action <> "Going to fight" Then 'Target.FightTarget Is Nothing Then ' 'Target.Attacker = 0 Then
                        Target.FightTarget = Me
                        Target.Action = "Fighting"
                        Target.FightArena = FightArena
                        'Debug.WriteLine(Id & ": Set fight area for gatherer id: " & Target.Id)
                    End If

                    'Check if target is inside FightArena
                    If Distance <= FieldOfVisionRadius / 4 Then
                        PerformAttack = True
                    Else
                        FightEnd = True
                    End If
                End If

            Else
                FightTarget = Nothing
                Action = ""
                FightArena = Nothing
                LookAround("Fight: Target dead")
            End If

            '-------------- ATTACK---------------
            If PerformAttack Then
                Action = "Fighting"

                'Throw attack
                If AttackThrowTimer Mod Int(RnAttackThrowTime / Speed) = 0 Then
                    'Debug.WriteLine(Id & ": Performing attack on gatherer id: " & Target.Id & ". Distance: " & Distance)
                    Target.Health -= 1
                    Target.Hit = True
                    NextRandom(RnAttackThrowTime)
                    AttackThrowTimer = 0
                End If
                AttackThrowTimer += 1

                If Target.Health <= 0 Then
                    ChangeAttackers(False, Target)
                    'Debug.WriteLine(Id & ": Killed gatherer (id: " & Target.Id & ")")
                    FightTarget = Nothing
                    Action = ""
                    FightArena = Nothing
                    LookAround("Fight: Target dead")
                End If
            End If

            '-------------- FINISH --------------
            If FightEnd Then
                'Debug.WriteLine(Id & ": Target (id: " & Target.Id & ") fleed. Distance: " & Round(Distance, 1) & "(x: " & x & ", y: " & y & ". FightArena.X: " & FightArena.X & ", FightArena.Y: " & FightArena.Y & ". Target.x: " & Target.x & ", Target.y: " & Target.y)
                FightTarget = Nothing
                ChangeAttackers(False, Target)
                Action = ""
                FightArena = Nothing
                LookAround("Fight: Target fleed")
            End If

        Else
            Action = ""
        End If
    End Sub

    Sub DebugItem(ByVal Item As String)
        Select Case Item
            Case "Fight"
                If form.Gatherers.Count > 1 Then
                    'Debug.WriteLine(Id & ": Preparing to fight. Action: " & Action)
                    If Action <> "Fighting" And Action <> "Going to fight" Then
                        Dim g As Gatherer
                        If form.Gatherers.Count = 2 Then
                            If ColNum = 1 Then
                                g = form.Gatherers.Item(2)
                            Else
                                g = form.Gatherers.Item(1)
                            End If
                        Else
                            g = form.Gatherers.Item(form.ExcludeNumberRandom(1, form.Gatherers.Count, ColNum))
                        End If
                        FightTarget = g
                    End If
                    Fight(FightTarget)
                End If
            Case "Flee"
                If form.Gatherers.Count > 1 Then
                    'Debug.WriteLine(Id & ": Preparing to fight. Action: " & Action)
                    If FightTarget Is Nothing = False Then
                        If form.DistanceBetween(x, y, FightTarget.x, FightTarget.y) <= FieldOfVisionRadius Then
                            Action = "Fleeing"
                        Else
                            Action = ""
                        End If
                    End If

                    If Action <> "Fleeing" Then
                        Dim g As Gatherer
                        If form.Gatherers.Count = 2 Then
                            If ColNum = 1 Then
                                g = form.Gatherers.Item(2)
                            Else
                                g = form.Gatherers.Item(1)
                            End If
                        Else
                            g = form.Gatherers.Item(form.ExcludeNumberRandom(1, form.Gatherers.Count, ColNum))
                        End If
                        FightTarget = g
                        MoveTo(g.x, g.y, 0)

                        If form.DistanceBetween(x, y, g.x, g.y) <= FieldOfVisionRadius Then
                            Action = "Fleeing"
                        End If
                    End If

                    If Action = "Fleeing" Then
                        Flee()
                    End If
                End If
        End Select
    End Sub

    Sub HelpFamilyFight()

        If VisibleFamilyFighting.Count <> 0 Then
            Dim g As Gatherer
            Dim Picked As Boolean = False
            If VisibleFamilyFighting.Count > 1 Then
                '    ''Determine which is most injured
                '    'Dim i As Integer
                '    'For i = 1 To VisibleFamilyFighting.Count

                '    'Next

                'Pick a random family to help
                g = VisibleFamilyFighting.Item(IntRandomInclusive(1, VisibleFamilyFighting.Count))
                If g.Action = "Fighting" Or g.Action = "Going to fight" Then
                    FightTarget = g.FightTarget
                    Picked = True
                Else
                    Dim v As Integer
                    For v = 1 To VisibleFamilyFighting.Count
                        If Picked = False Then
                            g = VisibleFamilyFighting.Item(v)
                            If g.Action = "Fighting" Or g.Action = "Going to fight" Then
                                FightTarget = g.FightTarget
                                Picked = True
                            End If
                        End If
                    Next
                End If
            Else
                g = VisibleFamilyFighting.Item(1)
                If g.Action = "Fighting" Or g.Action = "Going to fight" Then
                    FightTarget = g.FightTarget
                    Picked = True
                End If
            End If
            'Debug.WriteLine(Id & ": FamilyStr: " & FamilyStr & "Helping family id: " & g.Id & " fight gatherer id: " & FightTarget.Id)
            If Picked Then
                Fight(FightTarget)
            Else
                LookAround("HelpFamilyFight: Target action changed")
            End If
        Else
            FightTarget = Nothing
            Action = ""
            FightArena = Nothing
            LookAround("HelpFamilyFight: Target(s) lost")
        End If

    End Sub

    Sub Flee()

        If FightTarget Is Nothing = False Then
            If FightTarget.Action <> "Fleeing" Then
                If Action <> "Fleeing" Or FleePoint = New Point(0, 0) Then

                    'Determine flee point
                    Dim XDiff, YDiff, XRan, YRan As Integer
                    Dim Distance, Multiplier As Double

                    XDiff = FightTarget.x - x
                    YDiff = FightTarget.y - y

                    XRan = IntRandomInclusive(-10, 10)
                    YRan = IntRandomInclusive(-10, 10)

                    If XDiff = 0 And YDiff = 0 Then
                        XDiff = IntRandomInclusive(-FieldOfVisionRadius, FieldOfVisionRadius)
                        YDiff = IntRandomInclusive(-FieldOfVisionRadius, FieldOfVisionRadius)

                        Distance = form.DistanceBetween(x, y, x + XDiff, y + YDiff)
                        Multiplier = (FieldOfVisionRadius + 5) / Distance

                        FleePoint = New Point(x + (XDiff * Multiplier) + XDiff, y + (YDiff * Multiplier) + YDiff)
                    Else
                        Distance = form.DistanceBetween(x, y, FightTarget.x, FightTarget.y)
                        Multiplier = (FieldOfVisionRadius + 5) / Distance

                        FleePoint = New Point(x - (XDiff * Multiplier) + XRan, y - (YDiff * Multiplier) + YRan)
                    End If
                End If

                'Move away from fight arena
                Action = "Fleeing"
                MoveTo(FleePoint.X, FleePoint.Y, 0)
                'Debug.WriteLine(Id & ": Moving to " & FleePoint.ToString & ". Action: " & Action)

                If form.DistanceBetween(x, y, FleePoint.X, FleePoint.Y) = 0 Then
                    BattleLost()

                    Action = ""
                    FightTarget = Nothing
                    FightArena = Nothing
                    LookAround("Fight: Target fleed")

                End If
            Else
                Action = ""
                FightTarget = Nothing
                FightArena = Nothing
                LookAround("Fight: Target fleed")
            End If
        End If
    End Sub

    Sub MoveTo(ByVal XTarget As Integer, ByVal YTarget As Integer, ByVal Randomness As Integer)
        'Debug.WriteLine(Id & ": Move called to (" & XTarget & "," & YTarget & "). Action: " & Action)
        '===================== Random =======================
        If Randomness > 0 Then
            If Moving = False Then
                If MoveRandomTimer Mod RnMoveWait * 2 = 0 Then
                    'Debug.WriteLine("Randomising direction target...")
                    RnX = IntRandomInclusive(-Randomness, Randomness)
                    RnY = IntRandomInclusive(-Randomness, Randomness)
                    MoveRandomTimer = 0
                End If
                MoveRandomTimer += 1
            End If
        Else
            RnX = 0
            RnY = 0
        End If

        Dim XDiff, YDiff, XPM, YPM As Integer
        Dim XMove, YMove As Boolean
        XMove = False
        YMove = False

        XDiff = XTarget - x + RnX
        YDiff = YTarget - y + RnY

        '=========== Determine direction of movement ====================================
        If XDiff <> 0 Or YDiff <> 0 Then
            Moving = True

            If XDiff = 0 Then
                XPM = 0
            ElseIf XDiff > 0 Then
                XPM = 1
            Else
                XPM = -1
            End If
            If YDiff = 0 Then
                YPM = 0
            ElseIf YDiff > 0 Then
                YPM = 1
            Else
                YPM = -1
            End If
            'Status = "Attempting to move by (" & XDiff & "," & YDiff & ")..."

            'If Abs(XDiff) = Abs(YDiff) Then 'Diagonal Movement

            If Abs(XDiff) = 0 Then 'Vertical Movement
                'Debug.WriteLine(Id & ": My coords: (" & x & "," & y & "), moving by (" & XDiff & "," & YDiff & "): Vertical")
                YMove = True
            ElseIf Abs(YDiff) = 0 Then 'Horizontal Movement
                'Debug.WriteLine(Id & ": My coords: (" & x & "," & y & "), moving by (" & XDiff & "," & YDiff & "): Horizontal")
                XMove = True
            Else 'Diagonal Movement
                'If DiagonalSkip <> 4 And DiagonalSkip <> 7 Then
                'Debug.WriteLine(Id & ": My coords: (" & x & "," & y & "), moving by (" & XDiff & "," & YDiff & "): Diagonal")
                XMove = True
                YMove = True
            End If

            'Check Environment Bounds
            If x + XPM < 0 Or x + XPM + Width > form.Env.Width Then
                XMove = False
            End If
            If y + YPM < 0 Or y + YPM + Height > form.Env.Height Then
                YMove = False
            End If
            If (x + XPM < 0 Or x + XPM + Width > form.Env.Width) And (y + YPM < 0 Or y + YPM + Height > form.Env.Height) Then
                Moving = False
            End If

            '======= Move if possible ==========================================
            If XMove Then
                XNew = x + XPM
            End If
            If YMove Then
                YNew = y + YPM
            End If

            '' ''======= Check collision ==========================================
            ' ''Collision(XNew, YNew)
            ' ''If XMovementPossible Then
            ' ''    x = XNew
            ' ''End If
            ' ''If YMovementPossible Then
            ' ''    y = YNew
            ' ''End If
            ' ''label.Location = New Point(x, y)
            'label.Location = New Point(XNew, YNew)

            If XMove = False And YMove = False Then
                Moving = False
                Action = ""
                LookAround("MoveTo: Reached Env corner")
            Else
                TurnTowards(XNew - x, YNew - y)
            End If


            '============== Change position ===================
            If Turning = False Then
                'Change tail position
                If Tails <> 0 Then
                    Dim t As Integer
                    'Dim OldValues(0) As Point
                    'OldValues = TailPosition
                    For t = Tails To 1 Step -1
                        If t = 1 Then
                            TailPosition(t) = New Point(x, y)
                        Else
                            TailPosition(t) = TailPosition(t - 1)
                        End If
                        'Debug.WriteLine(Id & ": New position: (" & XNew & "," & YNew & "). Set tail " & t & " position to " & TailPosition(t).ToString)
                    Next
                    'Debug.WriteLine(Id & ": Tails: " & Tails)
                End If

                'Change head position
                x = XNew
                y = YNew
            End If
        Else
            Moving = False
            'Debug.WriteLine(Id & ": Finished moving")
        End If

    End Sub

    Function FindClosestMemorisedPlantsIndex() As Integer
        Dim v, _ClosestIndex As Integer
        Dim ClosestDistance, Distance As Double
        _ClosestIndex = 0
        ClosestDistance = form.Env.Width * form.Env.Height

        Dim Point As Point

        For v = 1 To MemorisedPlantsPosition.Length - 1
            Point = MemorisedPlantsPosition(v)
            Distance = form.DistanceBetween(x, y, Point.X, Point.Y)
            If Distance < ClosestDistance Then
                ClosestDistance = Distance
                _ClosestIndex = v
            End If
        Next

        FindClosestMemorisedPlantsIndex = _ClosestIndex
    End Function

    '++++++ Add a queue to divide if limit is reached
    Public Sub Divide(ByVal col As Collection, ByVal _id As Integer, ByVal ColNumber As Integer)
        If form.Gatherers.Count < 40 Then
            Dim NewGatherer As New Gatherer(form, x, y, _id, ColNumber, Gen + 1, DNA)
            col.Add(NewGatherer, NewGatherer.Id)
            form.GathererCountLabel.Text = form.Gatherers.Count - 1 & "/" & form.Limit

            'Add gatherer to family
            ChangeFamily(True, NewGatherer)
            Dim v As Integer
            Dim g As Gatherer
            For v = 1 To Family.Count
                g = Family.Item(v)
                If g.Alive Then
                    g.ChangeFamily(True, NewGatherer)
                End If
            Next

            If MemorisedPlants.Count > 0 Then
                'Communicate with new family
                FriendlyTarget = Family.Item(Family.Count)
                CommunicateWith(NewGatherer)
            End If
        End If
    End Sub

    Sub SetDNA()

        'Life
        DfLife = IntRandomInclusive(-15, 15) + (DfParentLife * form.InheritAmount)
        Life = form.DefaultLife + DfLife

        'Health
        DfHealthMaxFinal = IntRandomInclusive(-5, 5) + (DfParentHealthMaxFinal * form.InheritAmount)
        HealthMaxFinal = form.DefaultHealthMaxFinal + DfHealthMaxFinal
        HealthMax = HealthMaxFinal / 2.7
        Health = HealthMax

        'HealthPercentHigh
        DfHealthPercentHigh = IntRandomInclusive(-5, 5) + (DfParentHealthPercentHigh * form.InheritAmount)
        HealthPercentHigh = form.DefaultHealthPercentHigh + DfHealthPercentHigh

        'DivideTime
        DfMultiplyTime = IntRandomInclusive(-10, 10) + (DfParentMultiplyTime * form.InheritAmount)
        RnMultiplyTime += DfMultiplyTime

        'Aggressiveness
        DfAggressiveness = IntRandomInclusive(-20, 20) + (DfParentAggressiveness * form.InheritAmount)

        DNA = "Family = " & Id & ":" & FamilyStr & " " & _
            "DfLife = " & DfLife & " " & _
            "DfHealthMaxFinal = " & DfHealthMaxFinal & " " & _
            "DfHealthPercentHigh = " & DfHealthPercentHigh & " " & _
            "DfMultiplyTime = " & DfMultiplyTime & " " & _
            "DfAggressiveness = " & DfAggressiveness & " "

        'Debug.WriteLine(Id & ": My DNA: " & DNA)
    End Sub

    Sub InheritParentDNA(ByVal ParentDNA As String)
        If ParentDNA <> "" Then
            Dim v, c, k, FamilyCount, Index, EndIndex, Length As Integer
            Dim g As Gatherer

            'Family
            Index = InStr(ParentDNA, "Family = ") + 9
            EndIndex = InStr(Index, ParentDNA, " ") + 1
            Length = EndIndex - Index
            FamilyStr = Mid(ParentDNA, Index, Length)
            k = 1
            While InStr(k, FamilyStr, ":") > 0
                FamilyCount += 1
                k = InStr(k, FamilyStr, ":") + 1
            End While
            Dim FamilyId(FamilyCount) As Integer
            EndIndex = 0
            For v = 1 To FamilyCount
                Index = EndIndex + 1
                EndIndex = InStr(Index, FamilyStr, ":")
                Length = EndIndex - Index
                FamilyId(v) = Mid(FamilyStr, Index, Length)
            Next
            'Debug.Write(Id & ": Does " & FamilyStr & "= ")
            For v = 1 To form.Gatherers.Count
                g = form.Gatherers.Item(v)
                If g.Id <> Id Then
                    'Debug.Write("(" & g.Id & "?)")
                    For c = 1 To FamilyCount
                        If g.Id = FamilyId(c) Then
                            Family.Add(g)
                            FamilyId(c) = 0
                            'Debug.Write(g.Id & ":")
                        End If
                    Next
                End If
            Next
            'Debug.WriteLine(" ?")
            ReDim TalkTime(Family.Count)

            'dfParentLife
            Index = InStr(ParentDNA, "DfLife = ") + 9
            EndIndex = InStr(Index, ParentDNA, " ")
            Length = EndIndex - Index
            DfParentLife = Mid(ParentDNA, Index, Length)

            'dfParentHealthMaxFinal
            Index = InStr(ParentDNA, "DfHealthMaxFinal = ") + 19
            EndIndex = InStr(Index, ParentDNA, " ")
            Length = EndIndex - Index
            DfParentHealthMaxFinal = Mid(ParentDNA, Index, Length)

            'DfHealthPercentHigh
            Index = InStr(ParentDNA, "DfHealthPercentHigh = ") + 22
            EndIndex = InStr(Index, ParentDNA, " ")
            Length = EndIndex - Index
            DfHealthPercentHigh = Mid(ParentDNA, Index, Length)

            'DfParentMultiplyTime
            Index = InStr(ParentDNA, "DfMultiplyTime = ") + 17
            EndIndex = InStr(Index, ParentDNA, " ")
            Length = EndIndex - Index
            DfParentMultiplyTime = Mid(ParentDNA, Index, Length)

            'DfAggressiveness
            Index = InStr(ParentDNA, "DfAggressiveness = ") + 19
            EndIndex = InStr(Index, ParentDNA, " ")
            Length = EndIndex - Index
            DfParentAggressiveness = Mid(ParentDNA, Index, Length)

        End If
        'Debug.WriteLine(Id & ": Parent DNA: " & ParentDNA)
    End Sub

    Sub Death()

        Dim DeathTime As Integer = (Life / 4) * 1000 / Speed

        If DeathTimer = 0 Then
            BattleLost()
        End If

        form.Soil.ChangeCellFertility(True, x + (Width / 2), y + (Height / 2), HealthMax / DeathTime * form.GlobalFurtiliseAmount)

        If DeathTimer >= DeathTime Then
            If form.ShowingInfoId = Id Then
                form.ShowingInfoId = 0
            End If
            Remove()
        End If
        DeathTimer += 1
    End Sub

    Sub TurnTowards(ByVal Xd As Integer, ByVal Yd As Integer)
        Dim CurrentDirection, NewDirection As Integer

        CurrentDirection = Facing
        NewDirection = GetDirectionIndex(Xd, Yd)

        If CurrentDirection = NewDirection Then
            Turning = False
            'Debug.WriteLine(Id & ": Direction is the same to " & Facing & " Recieved: Xd: " & Xd & ", Yd: " & Yd & ". Moving")
        Else
            Turning = True
            'determine which way to turn
            If Abs(NewDirection - CurrentDirection) > 4 Then
                If NewDirection - CurrentDirection > 0 Then
                    If Facing <= 1 Then
                        Facing = 8
                    Else
                        Facing -= 1
                    End If
                Else
                    If Facing >= 8 Then
                        Facing = 1
                    Else
                        Facing += 1
                    End If
                End If
            Else
                If NewDirection - CurrentDirection < 4 Then
                    If NewDirection - CurrentDirection < 0 Then
                        If Facing <= 1 Then
                            Facing = 8
                        Else
                            Facing -= 1
                        End If
                    Else
                        If Facing >= 8 Then
                            Facing = 1
                        Else
                            Facing += 1
                        End If
                    End If
                ElseIf Abs(NewDirection - CurrentDirection) = 4 Then
                    Dim r As Integer
                    r = form.RandomOf2(-1, 1)
                    If r = 1 Then
                        If Facing >= 8 Then
                            Facing = 1
                        Else
                            Facing += 1
                        End If
                    Else
                        If Facing <= 1 Then
                            Facing = 8
                        Else
                            Facing -= 1
                        End If
                    End If
                Else
                    If Facing <= 1 Then
                        Facing = 8
                    Else
                        Facing -= 1
                    End If
                End If
            End If
            'Debug.WriteLine(Id & ": Changed direction from " & CurrentDirection & " to " & Facing & ". Target: " & NewDirection & "(Xd: " & Xd & ", Yd: " & Yd & "). Moving")

        End If

    End Sub

    Sub ReachTowards(ByVal Xd As Integer, ByVal Yd As Integer)

        Dim XDir, YDir As Integer
        Dim m As Double

        XDir = 0
        YDir = 0

        If Xd <> 0 Then
            m = Yd / Xd
            If m >= -0.5 And m < 0.5 Then
                If Xd > 0 Then
                    XDir = 1
                Else
                    XDir = -1
                End If
            ElseIf m >= -2 And m < -0.5 Then
                If Yd < 0 Then
                    XDir = 1
                    YDir = -1
                Else
                    XDir = -1
                    YDir = 1
                End If
            ElseIf m >= 0.5 And m < 2 Then
                If Yd < 0 Then
                    YDir = -1
                    XDir = -1
                Else
                    YDir = 1
                    XDir = 1
                End If
            Else
                If Yd < 0 Then
                    YDir = -1
                Else
                    YDir = 1
                End If
            End If
        Else
            If Yd < 0 Then
                YDir = -1
            Else
                YDir = 1
            End If
        End If

        Reaching = GetDirectionIndex(XDir, YDir)

        'ReachingTexLocation
        Select Case Reaching
            Case 1
                ReachingTexLocation = New Point(x + (Width / 2) - 2, y - 3)
            Case 2
                ReachingTexLocation = New Point(x + ((3 * Width) / 4), y - 2)
            Case 3
                ReachingTexLocation = New Point(x + Width - 1, y + (Height / 2) - 2)
            Case 4
                ReachingTexLocation = New Point(x + ((3 * Width) / 4), y + ((3 * Height) / 4))
            Case 5
                ReachingTexLocation = New Point(x + (Width / 2) - 2, y + Height - 1)
            Case 6
                ReachingTexLocation = New Point(x - 2, y + ((3 * Height) / 4))
            Case 7
                ReachingTexLocation = New Point(x - 3, y + (Height / 2) - 2)
            Case 8
                ReachingTexLocation = New Point(x - 2, y - 2)
        End Select
    End Sub

    Public Sub Remove()

        form.GreyOutGathererButtons()

        'Remove self from family
        'ChangeFamily(False, Me)
        Dim v As Integer
        Dim g As Gatherer
        For v = 1 To Family.Count
            g = Family.Item(v)
            g.ChangeFamily(False, Me)
        Next

        'Remove self from FightTargets
        For v = 1 To VisibleGatherers.Count
            g = VisibleGatherers.Item(v)
            RemoveVisibleGatherer(Me)
        Next

        Dispose()

        form.SetCurrent()

        'Debug.WriteLine(Id & ": Releasing...")
        'Release
        VisibleGatherers = Nothing : VisibleFamily = Nothing : VisibleEnemy = Nothing : VisibleFamilyFighting = Nothing : VisibleFamilyNormal = Nothing : VisiblePlants = Nothing
        Family = Nothing : MemorisedPlants = Nothing : Attackers = Nothing
        form = Nothing
        Random = Nothing
        OldFriendlyTarget = Nothing
        FightTarget = Nothing : FriendlyTarget = Nothing
        TargetPlant = Nothing
        'Point
        FightArena = Nothing : FriendlyArena = Nothing
        FleePoint = Nothing : FocusPoint = Nothing
        MemorisedPlantsPosition = Nothing
        'String
        Action = Nothing : Info = Nothing : Status = Nothing : FamilyStr = Nothing : MemorisedPlantsStr = Nothing : AttackersStr = Nothing
        NewStatus = Nothing : OldStatus = Nothing : DNA = Nothing
        'Double
        DfX = Nothing : DfY = Nothing : DfPlusMinus = Nothing : DfSpeed = Nothing : DfAggressiveness = Nothing : RnAggressiveness = Nothing : AveTalkTime = Nothing
        YXRatio = Nothing
        'Integer  ++++++ sort
        DfParentLife = Nothing : DfParentHealthMaxFinal = Nothing : DfParentHealthPercentHigh = Nothing : DfParentMultiplyTime = Nothing : DfParentAggressiveness = Nothing
        Seed = Nothing : FieldOfVisionRadius = Nothing : AgeCounter = Nothing : NextRemovedPlantsIdIndex = Nothing : MoveRandomTimer = Nothing : RnDivideProcessTime = Nothing : DfMultiplyTime = Nothing : DfHealthPercentHigh = Nothing : Speed = Nothing : Attacker = Nothing
        RnMoveWait = Nothing : VisibleMemorisedPlantsNumber = Nothing : RnLookAroundTime = Nothing : OldMemorisedPlantsCount = Nothing : SendInfoCount = Nothing : NewMemorisedPlantsCount = Nothing : Overlap = Nothing : LookAroundTimer = Nothing : LookAroundTime = Nothing : HealthTimer = Nothing : MultiplyTimer = Nothing : AttackThrowTimer = Nothing : RnAttackThrowTime = Nothing : FightTargetCounter = Nothing : RnPlant = Nothing
        RnX = Nothing : RnY = Nothing : XSkip = Nothing : YSkip = Nothing : XNew = Nothing : YNew = Nothing : XSearch = Nothing : YSearch = Nothing : ClosestIndex = Nothing : ClosestKnownPlantIndex = Nothing : DeathTimer = Nothing : FeedTimer = Nothing : AggressivenessTimer = Nothing
        Gen = Nothing : Id = Nothing : ColNum = Nothing : Age = Nothing : Life = Nothing : HealthMax = Nothing : HealthMaxFinal = Nothing : Health = Nothing : HealthPercent = Nothing : HealthPercentHigh = Nothing : DfLife = Nothing : DfHealthMaxFinal = Nothing : RnMultiplyTime = Nothing : Wins = Nothing : Fights = Nothing : TargetFamilyIndex = Nothing : SendInfoIndex = Nothing
        PriorityFeed = Nothing : PrioritySearch = Nothing : PriorityFight = Nothing : PriorityClaim = Nothing : PriorityRemember = Nothing : PriorityFlee = Nothing : PriorityLookAround = Nothing : PriorityHelpFight = Nothing : PriorityCommunicate = Nothing
        x = Nothing : y = Nothing : Width = Nothing : Height = Nothing
        TalkTime = Nothing
        'Boolean
        HealthHigh = Nothing : HealthMaxed = Nothing : Alive = Nothing : Young = Nothing : Moving = Nothing : Dividing = Nothing : XMovementPossible = Nothing : YMovementPossible = Nothing : XAvoidingOverlap = Nothing : YAvoidingOverlap = Nothing : LookingAround = Nothing
    End Sub

    Public Sub Dispose()
        form.ChangeColNumberGatherer(ColNum)
        form.Gatherers.Remove(ColNum)
    End Sub

    Sub AddTail()
        Dim v, AddAmount As Integer
        AddAmount = (TailPosition.Length - 1) / 3
        Tails += AddAmount

        If Tails - AddAmount <> 0 Then
            For v = Tails To Tails - AddAmount + 1 Step -1
                TailPosition(v) = TailPosition(Tails - AddAmount)
            Next
        Else
            For v = 1 To Tails
                TailPosition(v) = New Point(x, y)
            Next
        End If
    End Sub

    Sub BattleLost()
        If Attackers Is Nothing = False Then
            'Reset attackers
            If Attackers.Count <> 0 Then
                Dim g As Gatherer
                'Lost fight

                Fights += 1
                'Debug.WriteLine(Id & ": " & Wins & "/" & Fights & " (Lost fight)")

                While Attackers.Count <> 0
                    g = Attackers.Item(1)
                    If g.Id <> 0 Then
                        g.Wins += 1
                        g.Fights += 1
                        'Debug.WriteLine("   Adding a win to gatherer id: " & g.Id & ". Result: " & g.Wins & "/" & g.Fights)
                        ChangeAttackers(False, g)

                        'Remove self from attacker
                        g.ChangeAttackers(False, Me)
                    Else
                        Attackers.Clear()
                    End If
                End While
            End If
        End If
    End Sub

    'Note: atm cousins are not family, only the parent and offspring are
    Public Sub ChangeFamily(ByVal Add As Boolean, ByVal Target As Gatherer)
        Dim g As Gatherer
        Dim v As Integer
        Dim Changed As Boolean = False

        If Add Then
            If InStr(FamilyStr, Target.Id & ":") = 0 And Target.Id <> Id Then
                Family.Add(Target)
                Changed = True
            End If
            'FamilyStr &= (TargetGatherer.Id & ":")
        Else
            If InStr(FamilyStr, Target.Id & ":") > 0 And Target.Id <> Id Then
                v = 1
                g = Family.Item(v)
                'Debug.WriteLine(Id & ": " & TargetGatherer.Id & " = " & g.Id & "?")
                While g.Id <> Target.Id And v < Family.Count
                    v += 1
                    g = Family.Item(v)
                    'Debug.WriteLine(Id & ": " & TargetGatherer.Id & " = " & g.Id & "?")
                End While
                'Debug.WriteLine(Id & ": Removing gatherer from family (id: " & g.Id & ")")
                Family.Remove(v)
                Changed = True
                'FamilyStr = Replace(FamilyStr, g.Id & ":", "")
            End If
        End If

        If Changed Then
            Dim i As Integer
            FamilyStr = ""
            For i = 1 To Family.Count
                g = Family.Item(i)
                FamilyStr &= g.Id & ":"
            Next
            FamilyStr &= " "

            'Communication update
            Dim OldValues(TalkTime.Length) As Integer
            OldValues = TalkTime
            Dim c As Integer
            ReDim TalkTime(Family.Count)
            If Add Then
                'If v = Family.Count Then
                For c = 1 To Family.Count - 1
                    TalkTime(c) = OldValues(c)
                Next
                'Else
                '    For c = 1 To Family.Count
                '        If c < v Then
                '            TalkTime(c) = OldValues(c)
                '        ElseIf c > v Then
                '            TalkTime(c) = OldValues(c - 1)
                '        End If
                '    Next
                'End If
            Else
                'Remove
                If v = Family.Count Then
                    For c = 1 To Family.Count
                        TalkTime(c) = OldValues(c)
                    Next
                Else
                    For c = 1 To Family.Count
                        If c < v Then
                            TalkTime(c) = OldValues(c)
                        Else
                            TalkTime(c) = OldValues(c + 1)
                        End If
                    Next
                End If
            End If
            'Debug.WriteLine(Id & ": Added family?: " & Add & ". Family: " & Family.Count & ". OldValues: " & form.ArrayValues(OldValues) & "  NewValues: " & form.ArrayValues(TalkTime))
        End If

    End Sub

    Public Sub ChangeMemorisedPlants(ByVal Add As Boolean, ByVal _TargetPlant As Plant, ByVal CollectionIndex As Integer)
        Dim p As Plant
        Dim v As Integer
        Dim Changed As Boolean = False
        TargetPlant = _TargetPlant

        If CollectionIndex = 0 Then
            If Add Then
                If InStr(MemorisedPlantsStr, TargetPlant.id & ".") = 0 Then
                    MemorisedPlants.Add(TargetPlant)
                    Changed = True
                End If
            Else
                'Debug.WriteLine(Id & ": Removing from MemorisedPlants by target (id: " & TargetPlant.id & ")")
                If InStr(MemorisedPlantsStr, TargetPlant.id & ".") > 0 Then
                    v = 1
                    p = MemorisedPlants.Item(v)
                    While p.id <> TargetPlant.id And v < MemorisedPlants.Count
                        v += 1
                        p = MemorisedPlants.Item(v)
                    End While
                    MemorisedPlants.Remove(v)
                    AddRemovedPlantId(p.id)

                    Changed = True
                End If
            End If
        Else
            'Debug.WriteLine(Id & ": Removing from MemorisedPlants by index: " & CollectionIndex & "/" & MemorisedPlants.Count)
            MemorisedPlants.Remove(CollectionIndex)
            Changed = True
        End If

        ReDim MemorisedPlantsPosition(MemorisedPlants.Count)

        If Changed Then
            MemorisedPlantsStr = ""
            For v = 1 To MemorisedPlants.Count
                p = MemorisedPlants.Item(v)
                MemorisedPlantsStr &= p.id & "."
                MemorisedPlantsPosition(v) = New Point(p.x, p.y)
            Next
            If Len(MemorisedPlantsStr) > 0 Then
                MemorisedPlantsStr &= " "
            End If
            'Debug.WriteLine(Id & ": MemorisedPlants: " & MemorisedPlantsStr & "Action: " & Action)
        End If
        TargetPlant = Nothing
    End Sub

    Public Function FindFamilyIndex(ByVal Target As Gatherer) As Integer
        Dim v, Index As Integer
        Dim Found As Boolean = False
        Dim g As Gatherer
        For v = 1 To Family.Count
            If Found = False Then
                g = Family.Item(v)
                If g.Id = Target.Id Then
                    Index = v
                    Found = True
                End If
            End If
        Next
        FindFamilyIndex = Index
    End Function

    Public Sub ChangeAttackers(ByVal Add As Boolean, ByVal Target As Gatherer)
        If Target.Id <> 0 Then

            Dim v As Integer
            Dim g As Gatherer
            Dim Changed As Boolean = False

            If Add Then
                If InStr(AttackersStr, Target.Id & ".") = 0 Then
                    Attackers.Add(Target)
                    Changed = True
                    'Debug.WriteLine(Id & ": Added gatherer id " & Target.Id & " to Attackers")
                End If
            Else
                'Remove
                If AttackersStr Is Nothing = False Then
                    If InStr(AttackersStr, Target.Id & ".") <> 0 Then
                        If Attackers.Count <> 0 Then
                            For v = 1 To Attackers.Count
                                If Changed = False Then
                                    g = Attackers.Item(v)
                                    If Target.Id = g.Id Then
                                        Attackers.Remove(v)
                                        Changed = True
                                        'Debug.WriteLine(Id & ": Removed gatherer id " & Target.Id & " from Attackers")
                                    End If
                                End If
                            Next
                        Else
                            Changed = True
                        End If
                    End If
                End If
            End If

            If Changed Then
                AttackersStr = ""

                If Attackers.Count <> 0 Then
                    For v = 1 To Attackers.Count
                        g = Attackers.Item(v)
                        AttackersStr &= g.Id & "."
                    Next
                    AttackersStr &= " "
                End If
                'Debug.WriteLine(Id & ": ColNum: " & ColNum & ". AttackersStr: " & AttackersStr & "Action: " & Action)
            End If
        End If
    End Sub

    Public Sub RemoveVisibleGatherer(ByVal Target As Gatherer)
        Dim g As Gatherer
        Dim v As Integer = 0
        Dim Done As Boolean = False

        For v = 1 To VisibleGatherers.Count
            If Done = False Then
                g = VisibleGatherers.Item(v)
                If g.Id = Target.Id Then
                    VisibleGatherers.Remove(v)
                    Done = True
                End If
            End If
        Next
    End Sub

    Public Sub RemoveVisiblePlant(ByVal Target As Plant)
        Dim p As Plant
        Dim v As Integer = 0
        Dim Done As Boolean = False

        For v = 1 To VisiblePlants.Count
            If Done = False Then
                p = VisiblePlants.Item(v)
                If p.id = Target.id Then
                    VisiblePlants.Remove(v)
                    Done = True
                End If
            End If
        Next

    End Sub

    Function GetDirectionIndex(ByVal Xd As Integer, ByVal Yd As Integer) As Integer
        Select Case Xd
            Case 0
                Select Case Yd
                    Case -1
                        GetDirectionIndex = 1 'N
                    Case 0
                        GetDirectionIndex = Facing
                    Case 1
                        GetDirectionIndex = 5 'S
                End Select
            Case 1 'E
                Select Case Yd
                    Case -1
                        GetDirectionIndex = 2 'N
                    Case 0
                        GetDirectionIndex = 3
                    Case 1
                        GetDirectionIndex = 4 'S
                End Select
            Case -1 'W
                Select Case Yd
                    Case 1
                        GetDirectionIndex = 6 'S
                    Case 0
                        GetDirectionIndex = 7
                    Case -1
                        GetDirectionIndex = 8 'N
                End Select
        End Select
    End Function

    Sub AddRemovedPlantId(ByVal TargetId As Integer)
        'determine whether memory exists
        Dim i As Integer
        Dim NewMemory As Boolean = True
        For i = 0 To RemovedPlantsId.Length - 1
            If TargetId = RemovedPlantsId(i) And NewMemory = True Then
                NewMemory = False
            End If
        Next

        If NewMemory Then
            'Add new entry at NextRemovedPlantsIdIndex
            RemovedPlantsId(NextRemovedPlantsIdIndex) = TargetId
            If NextRemovedPlantsIdIndex = RemovedPlantsId.Length - 1 Then
                NextRemovedPlantsIdIndex = 0
            Else
                NextRemovedPlantsIdIndex += 1
            End If
        End If
        'Debug.WriteLine(Id & ": Str: " & form.ArrayValues(RemovedPlantsId) & ". Id: " & TargetId & " removed?: " & RemovedPlant(TargetId))
        'Debug.WriteLine(Id & ": Added plant id: " & TargetId & " to RemovedPlantsId. New list: " & form.ArrayValues(RemovedPlantsId))
    End Sub

    Function IntRandomInclusive(ByVal LowestNum, ByVal HighestNum) As Integer
        IntRandomInclusive = Random.Next(LowestNum, HighestNum + 1)
    End Function

    Public Function RemovedPlant(ByVal TargetId As Integer) As Boolean
        Dim v As Integer
        Dim Removed As Boolean = False
        RemovedPlant = False
        For v = 0 To RemovedPlantsId.Length - 1
            If TargetId = RemovedPlantsId(v) And Removed = False Then
                Removed = True
                RemovedPlant = True
            End If
        Next
    End Function

    Sub NextRandom(ByVal Name)
        Select Case Name
            Case RnLookAroundTime
                RnLookAroundTime = IntRandomInclusive(4, 8)
            Case RnMoveWait
                RnMoveWait = IntRandomInclusive(1, 10)
            Case RnMultiplyTime
                RnMultiplyTime = IntRandomInclusive(20000, 35000)
            Case RnDivideProcessTime
                RnDivideProcessTime = IntRandomInclusive(1000, 1500)
            Case RnAttackThrowTime
                Dim Low, High, Exp As Integer
                Low = 500
                High = 800
                Exp = Wins * (Low / 25)
                If Exp > Low / 1.25 Then
                    Exp = Low / 1.25
                End If

                RnAttackThrowTime = IntRandomInclusive(500 - (Wins * 20), 800 - (Wins * 20))

            Case RnFeedTime
                RnFeedTime = IntRandomInclusive(200, 400)
        End Select
    End Sub

End Class
