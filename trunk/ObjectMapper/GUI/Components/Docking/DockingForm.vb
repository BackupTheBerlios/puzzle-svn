Imports WeifenLuo.WinFormsUI

Public Class DockingForm
    Inherits DockContent


    Public Sub New()
        Me.HideOnClose = True
        Me.DockableAreas = DockAreas.DockBottom Or DockAreas.DockLeft Or DockAreas.DockRight Or DockAreas.DockTop Or DockAreas.Document Or DockAreas.Float
    End Sub


    Private content As Panel
    Private oldParent As Control
    Public Sub SetContent(ByVal content As Panel, ByVal title As String)

        Me.HideOnClose = True

        Me.content = content
        Me.oldParent = content.Parent

        Me.Controls.Clear()
        content.Parent = Me
        content.Dock = DockStyle.Fill
        Me.Text = title
        content.Visible = True
    End Sub

End Class
