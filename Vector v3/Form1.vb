Imports System.IO
Imports EasyExploits
Imports WeAreDevs_API
Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Public Class Form1
    Private borderRadius As Integer = 30
    Private borderSize As Integer = 3
    Dim lastPoint As Point
    ' Dim hack As EasyExploits.Module = New EasyExploits.Module
    Dim hack As ExploitAPI = New ExploitAPI
    Dim p() As Process
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("settings.Vector",
               System.Text.Encoding.UTF32)

        FastColoredTextBox1.Text = fileReader

        Dim a As Integer
        For a = 10 To 40 Step +1
            Me.Opacity = a / 40
            Me.Refresh()
            Threading.Thread.Sleep(5)
        Next
        ListBox1.Items.Clear()
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Clear()
        Dim a As Integer
        For a = 30 To 10 Step -1
            Me.Opacity = a / 30
            Me.Refresh()
            Threading.Thread.Sleep(15)
        Next
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ListBox1.Items.Clear()
        Dim a As Integer
        For a = 30 To 10 Step -1
            Me.Opacity = a / 30
            Me.Refresh()
            Threading.Thread.Sleep(15)
        Next
        Me.WindowState = FormWindowState.Minimized
        Me.Opacity = 100
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        lastPoint = New Point(e.X, e.Y)
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If (e.Button = MouseButtons.Left) Then
            Me.Left += e.X - lastPoint.X
            Me.Top += e.Y - lastPoint.Y
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.Title = "Please select a lua file"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Filter = "lua Files|*.lua"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName,
               System.Text.Encoding.UTF32)
            FastColoredTextBox1.Text = fileReader
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SaveFileDialog1.Filter = "lua Files (*.lua*)|*.lua"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
         Then
            My.Computer.FileSystem.WriteAllText _
            (SaveFileDialog1.FileName, FastColoredTextBox1.Text, True)
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        FastColoredTextBox1.Text = File.ReadAllText($"./Scripts/{ListBox1.SelectedItem}")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.Items.Clear()
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        f_Attach()
    End Sub

    Private Sub FastColoredTextBox1_Load(sender As Object, e As EventArgs) Handles FastColoredTextBox1.Load
        FastColoredTextBox1.Language = FastColoredTextBox1.Language.Lua

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        hack.SendLuaScript(FastColoredTextBox1.Text)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            Process.GetProcessesByName("RobloxPlayerBeta")(0).Kill()
        Catch ex As Exception
            MsgBox("cannot terminate the process 'RobloxPlayerBeta.exe' ", 0 + 16, "Vector v3")
        End Try

    End Sub

    Private Sub InjTest_Tick(sender As Object, e As EventArgs) Handles InjTest.Tick
        If hack.isAPIAttached = True Then
            InjTest.Stop()
            Reset.Start()

            Dim fileReader As String
            Try
                Label3.Text = "injected"
                ListBox1.Items.Add("injected")

                ListBox1.Items.Clear()
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")

            Catch ex As Exception
                ListBox1.Items.Add("error")
                Label3.Text = "injected: 1 error"
                MsgBox("could not read 'Menu.Vector' try reinstalling the program")
                Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
                ListBox1.Items.Clear()
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")

            End Try

        Else
        End If
    End Sub

    Private Sub Reset_Tick(sender As Object, e As EventArgs) Handles Reset.Tick
        If hack.isAPIAttached = False Then
            Reset.Stop()
            InjTest.Start()
            Label3.Text = "not injected"
            ListBox1.Items.Clear()
            ListBox1.Items.Add("error")
            ListBox1.Items.Add("Roblox closed")
            MsgBox("//", 0 + 16, "//")
            Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
            ListBox1.Items.Clear()
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")


        End If
    End Sub

    Private Sub FastColoredTextBox1_TextChanging(sender As Object, e As FastColoredTextBoxNS.TextChangingEventArgs) Handles FastColoredTextBox1.TextChanging
        If FastColoredTextBox1.Text = "./reset" Then
            MsgBox("unexpected error occurred", 0 + 16, "Vector v3")
            Me.Close()
            MsgBox("experimental settings enabled")
        ElseIf FastColoredTextBox1.Text = "./vector>developer_settings = true" Then
            Button9.Enabled = True

        ElseIf FastColoredTextBox1.Text = "./vector>developer_settings = false" Then
            MsgBox("experimental settings disabled")
            Button9.Enabled = False
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ScriptHub.Show()
    End Sub

    Private Sub FastColoredTextBox1_TextChanged(sender As Object, e As FastColoredTextBoxNS.TextChangedEventArgs)

    End Sub
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer, ByVal nWidthEllipse As Integer, ByVal nHeightEllipse As Integer) As IntPtr

    End Function
    Public Sub New()
        InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.None
        Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20))
    End Sub
    Private Sub f_Attach()
        p = Process.GetProcessesByName("RobloxPlayerBeta")
        If p.Count > 0 Then
            ListBox1.Items.Clear()
            ListBox1.Items.Add("attempting to inject")
            hack.LaunchExploit()
        Else

            ListBox1.Items.Clear()
            ListBox1.Items.Add("error")

            ListBox1.Items.Add("not in a game")
            MsgBox("You are currently not in a game so no attempt to inject was made", 0 + 16, "vector V3 -error")
            Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
            ListBox1.Items.Clear()
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
        End If
    End Sub

    Private Sub time_ontop_Tick(sender As Object, e As EventArgs) Handles time_ontop.Tick
        Me.TopMost = True
    End Sub
End Class