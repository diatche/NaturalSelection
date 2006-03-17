Imports System.math

Public Class Soil
    Dim form As Environment
    Public Fertility(0, 0) As Double
    'Public FertilityChanged(0, 0) As Boolean
    Public CellLength As Integer = 8
    Public BaseAdd As Integer = 1
    Public Spread As Double = 0.4 'FertilityParent / FertilityLength
    Private SpreadDelayTimer As Integer
    Public FertilityRound, Columns, Rows, Area, SpreadDelay As Integer

    Public Sub New(ByVal _form As Environment)
        form = _form

        Columns = form.Env.Width / CellLength
        Rows = form.Env.Height / CellLength

        Area = Columns * Rows

        ReDim Fertility(Columns, Rows)
        'ReDim FertilityChanged(Columns, Rows)
    End Sub

    Public Sub ChangeCellFertility(ByVal Add As Boolean, ByVal XPos As Integer, ByVal YPos As Integer, ByVal Amount As Double)

        Dim CellFertilityChange As Double = BaseAdd * Amount
        Dim Cell As Point = New Point(form.MinNum(Floor(XPos / CellLength), 0), form.MinNum(Floor(YPos / CellLength), 0))

        If Add Then
            Fertility(Cell.X, Cell.Y) += CellFertilityChange
        Else
            If Fertility(Cell.X, Cell.Y) - CellFertilityChange > 0 Then
                Fertility(Cell.X, Cell.Y) -= CellFertilityChange
            Else
                Fertility(Cell.X, Cell.Y) = 0
            End If
        End If

    End Sub

    'Note: Spreading does not happen diagonaly
    Public Sub SpreadFurtility()
        If Spread <> 0 Then
            SpreadDelayTimer += 1
            If SpreadDelayTimer Mod Int(SpreadDelay / form.GlobalTimerInterval) = 0 Or SpreadDelay = 0 Then
                SpreadDelayTimer = 0
            End If
            If SpreadDelayTimer = 0 Then
                'Spread every cell
                Dim x, y, r As Integer
                Dim AdjCellsFertility(3), Sorted(3), TargetFertility, SpreadAmount As Double
                Dim AdjCells(3), TargetCell As Point
                For x = 0 To Columns
                    For y = 0 To Rows
                        If Fertility(x, y) <> 0 Then
                            'Determine which cell has the smallest value

                            'Upper cell
                            If y <> 0 Then
                                AdjCells(0) = New Point(x, y - 1)
                                AdjCellsFertility(0) = Fertility(x, y - 1)
                            Else
                                AdjCells(0) = Nothing
                            End If

                            'Right cell
                            If x <> Columns Then
                                AdjCells(1) = New Point(x + 1, y)
                                AdjCellsFertility(1) = Fertility(x + 1, y)
                            Else
                                AdjCells(1) = Nothing
                            End If

                            'Lower cell
                            If y <> Rows Then
                                AdjCells(2) = New Point(x, y + 1)
                                AdjCellsFertility(2) = Fertility(x, y + 1)
                            Else
                                AdjCells(2) = Nothing
                            End If

                            'Left cell
                            If x <> 0 Then
                                AdjCells(3) = New Point(x - 1, y)
                                AdjCellsFertility(3) = Fertility(x - 1, y)
                            Else
                                AdjCells(3) = Nothing
                            End If

                            r = form.IntRandomInclusive(0, 3)
                            While AdjCells(r) = Nothing
                                r = form.IntRandomInclusive(0, 3)
                            End While
                            TargetCell = AdjCells(r)
                            TargetFertility = AdjCellsFertility(r)

                            'Furtility must be greater to spread
                            If Fertility(x, y) > TargetFertility Then

                                'Determine Spread amount
                                SpreadAmount = (Fertility(x, y) - TargetFertility) * Spread

                                'Spread
                                Fertility(x, y) -= SpreadAmount
                                Fertility(TargetCell.X, TargetCell.Y) += SpreadAmount

                            End If
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Public Function FertilityRealPosition(ByVal Column As Integer, ByVal Row As Integer) As Point
        FertilityRealPosition.X = Column * CellLength
        FertilityRealPosition.Y = Row * CellLength
    End Function

    Public Function FertilityArrayPosition(ByVal XArg As Integer, ByVal YArg As Integer) As Point
        FertilityArrayPosition = New Point(form.MaxNum(form.MinNum(Floor(XArg / CellLength), 0), form.Env.Width - 1), form.MaxNum(form.MinNum(Floor(YArg / CellLength), 0), form.Env.Height - 1))
    End Function

    'Function OptimumAngle(ByVal Fertility As Double, ByVal Spread As Double) As Double
    '    'Find agle between square distance between 2 furthest cells

    '    'Find angles of 2 triangles and subtracts from PI/2
    '    Dim Angle1, Angle2 As Double
    '    Dim XDiff, YDiff As Integer

    '    'Bottom triangle
    '    XDiff = Round(Fertility / Spread) * CellLength
    '    YDiff = XDiff

    '    Angle1 = Atan(YDiff / XDiff)

    '    'Top triangle
    '    XDiff = (Round(Fertility / Spread) - 1) * CellLength

    '    Angle2 = Atan(XDiff / YDiff)

    '    OptimumAngle = (PI / 2) - Angle1 - Angle2

    '    'Debug.WriteLine("Optimum angle: " & OptimumAngle)
    'End Function

    Public Sub Reset()
        Dim v, i As Integer
        For v = 0 To Columns
            For i = 0 To Rows
                Fertility(v, i) = 0
            Next
        Next
    End Sub

    Public Sub GenerateLand(ByVal Cells As Integer, ByVal Time As Integer, ByVal Hardness As Integer, ByVal _Spread As Double)
        Dim OldSpread As Double = Spread

        Spread = _Spread

        Dim i As Integer
        For i = 1 To Cells
            ChangeCellFertility(True, form.IntRandomInclusive(0, form.Env.Width), form.IntRandomInclusive(0, form.Env.Height), Hardness)
        Next

        For i = 1 To Time
            SpreadFurtility()
        Next

        Spread = OldSpread
    End Sub
End Class