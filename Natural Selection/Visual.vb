Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports Microsoft.DirectX.DXHelp
Imports System.math

Public Class Visual
    Dim form As Environment
    Public _device As Device
    Public _sprite As Sprite
    'Gatherers
    Private TexGatherer, TexGathererDead, TexGathererHit, TexGathererFleeing, TexGathererTalking, TexGathererFeeding, TexGathererDividing, TexGathererLook(8), TexGathererReach(8), TexGathererStatsHealth(8), TexGathererTail, TexGathererTailDead, TexSelectedGatherer, TexFamilySelectedGatherer As Texture
    'Plants
    Private TexPlantStem, TexPlantLeaf, TexPlantFood(8), TexPlantDeadStem, TexPlantDeadLeaf, TexSelectedPlant As Texture
    'Soil
    Private TexFertility(5), TexSelectedSoil As Texture

    Public Sub New(ByVal _form As Environment)
        form = _form
    End Sub

    Public Sub SetDisplay()
        'Set display
        Dim presentParams As New PresentParameters
        Dim frmHandle As IntPtr
        frmHandle = form.Env.Handle

        presentParams.Windowed = True
        presentParams.SwapEffect = SwapEffect.Discard
        _device = New Device(0, DeviceType.Hardware, frmHandle, CreateFlags.HardwareVertexProcessing Or CreateFlags.FpuPreserve, presentParams)
        _sprite = New Sprite(_device)
    End Sub

    Public Sub LoadTextures()
        'Load Textures
        Dim v As Integer
        TexSelectedGatherer = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\Selected.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexFamilySelectedGatherer = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\FamilySelected.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexSelectedSoil = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\SelectedSoil.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexSelectedPlant = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\SelectedPlant.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        'Gatherers
        TexGatherer = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\gatherer.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererDead = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererDead.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererHit = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererHit.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererFleeing = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererFleeing.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererTalking = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererTalking.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererFeeding = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererFeeding.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererDividing = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererDividing.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererTail = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererTail.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexGathererTailDead = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererTailDead.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        For v = 1 To 8
            TexGathererLook(v) = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererLook" & v & ".png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
            TexGathererReach(v) = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererReach" & v & ".png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
            TexGathererStatsHealth(v) = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\GathererStatsHealth" & v & ".png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        Next
        'Plants
        TexPlantStem = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\PlantStem.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexPlantLeaf = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\PlantLeaf.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexPlantDeadStem = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\PlantDeadStem.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        TexPlantDeadLeaf = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\PlantDeadLeaf.png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        For v = 1 To 5
            TexPlantFood(v) = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\PlantFood" & v & ".png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        Next
        'Soil
        For v = 1 To 5
            TexFertility(v) = TextureLoader.FromFile(_device, Application.StartupPath & "\Vis\Fertility" & v & ".png", 0, 0, D3DX.Default, 0, Format.Unknown, Pool.Default, Filter.Point, Filter.Point, Color.Magenta.ToArgb)
        Next
    End Sub

    '++++++ make Reaching 1 texture and move it around the circle instead
    '       of having 8 diff textures
    Public Sub Render()

        If _device Is Nothing Then Return

        _device.Clear(ClearFlags.Target, form.Env.BackColor, 1.0F, 0)
        _device.BeginScene()

        _sprite.Begin(SpriteFlags.AlphaBlend) '-----------------------------------

        'Soil
        Dim v, h, i As Integer
        Dim c As Double
        For v = 0 To form.Soil.Columns
            For h = 0 To form.Soil.Rows
                If Round(form.Soil.Fertility(v, h), 1) <> 0 Then
                    c = form.MaxNum(Round(form.Soil.Fertility(v, h) * 10), 5)
                    _sprite.Draw(TexFertility(c), Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(form.Soil.FertilityRealPosition(v, h).X - 4, form.Soil.FertilityRealPosition(v, h).Y - 4, 0.0F), Color.White)
                End If
            Next
        Next

        'Plants
        Dim g As Gatherer
        Dim p As Plant
        For v = 1 To form.Plants.Count
            p = form.Plants.Item(v)

            If p.Alive Then
                'Base
                If p.StemState Then
                    _sprite.Draw(TexPlantStem, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(p.x, p.y, 0.0F), Color.White)
                Else
                    _sprite.Draw(TexPlantLeaf, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(p.x, p.y, 0.0F), Color.White)
                End If

                'Food
                h = p.HealthPercent / 20
                If h <> 0 Then
                    _sprite.Draw(TexPlantFood(h), Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(p.x, p.y, 0.0F), Color.White)
                End If
            Else
                'Dead
                If p.StemState Then
                    _sprite.Draw(TexPlantDeadStem, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(p.x, p.y, 0.0F), Color.White)
                Else
                    _sprite.Draw(TexPlantDeadLeaf, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(p.x, p.y, 0.0F), Color.White)
                End If
            End If
        Next

        'Gatherers
        For v = 1 To form.Gatherers.Count
            g = form.Gatherers.Item(v)
            If g.Alive = False Then
                'Dead tails              
                If g.Tails <> 0 Then
                    For h = 1 To g.Tails
                        If h Mod Int((g.TailPosition.Length - 1) / 3) = 0 Then
                            _sprite.Draw(TexGathererTailDead, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.TailPosition(h).X, g.TailPosition(h).Y, 0.0F), Color.White)
                        End If
                    Next
                End If

                'Dead
                _sprite.Draw(TexGathererDead, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
            End If
        Next
        For v = 1 To form.Gatherers.Count
            g = form.Gatherers.Item(v)
            If g.Alive Then
                'Tails
                If g.Tails <> 0 Then
                    For h = g.Tails To 1 Step -1
                        If h Mod Int((g.TailPosition.Length - 1) / 3) = 0 Then
                            _sprite.Draw(TexGathererTail, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.TailPosition(h).X, g.TailPosition(h).Y, 0.0F), Color.White)
                        End If
                    Next
                End If

                'StatsHealth
                h = g.HealthPercent / 12.5
                If h <> 0 Then
                    _sprite.Draw(TexGathererStatsHealth(h), Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
                End If

                'Facing
                _sprite.Draw(TexGathererLook(g.Facing), Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)

                'Dividing
                If g.Dividing Then
                    _sprite.Draw(TexGathererDividing, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
                Else
                    'Reach
                    If g.Reaching <> 0 Then
                        _sprite.Draw(TexGathererReach(g.Reaching), Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.ReachingTexLocation.X, g.ReachingTexLocation.Y, 0.0F), Color.White)
                    End If

                    'Feeding
                    If g.Action = "Feeding" Then
                        _sprite.Draw(TexGathererFeeding, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
                    End If

                    'Communicating
                    If g.Status = "Communicating" Then
                        _sprite.Draw(TexGathererTalking, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
                    End If

                    'Fighting
                    If g.Action = "Fighting" Then
                        _sprite.Draw(TexGathererHit, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
                    End If

                    'Fleeing
                    If g.Action = "Fleeing" Then
                        _sprite.Draw(TexGathererFleeing, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x, g.y, 0.0F), Color.White)
                    End If
                End If

                'Family Selected
                If g.FamilySelected Then
                    _sprite.Draw(TexFamilySelectedGatherer, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(g.x + (g.Width / 2) - 8.0F, g.y + (g.Height / 2) - 8.0F, 0.0F), Color.White)
                End If
            End If
        Next

        If TypeOf form.Selected Is Gatherer Then
            _sprite.Draw(TexSelectedGatherer, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(form.Selected.x - 4, form.Selected.y - 4, 0.0F), Color.White)
        End If

        If TypeOf form.Selected Is Plant Then
            _sprite.Draw(TexSelectedPlant, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(form.Selected.x - 8, form.Selected.y - 8, 0.0F), Color.White)
        End If

        If TypeOf form.Selected Is Point Then
            _sprite.Draw(TexSelectedSoil, Rectangle.Empty, New Vector3(0, 0, 0), New Vector3(form.Selected.x - 4, form.Selected.y - 4, 0.0F), Color.White)
        End If

        _sprite.End() '-----------------------------------------------------------

        _device.EndScene()
        _device.Present()

    End Sub


End Class
