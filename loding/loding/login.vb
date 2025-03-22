Public Class login

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_logn.Click
        If Username.Text = "" Or password.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Enter user name, password, and page ")
        ElseIf Username.Text = "Admin" And password.Text = "1234" Then
            Me.Hide()
            If ComboBox1.SelectedIndex = 0 Then
                items.Show()
            ElseIf ComboBox1.SelectedIndex = 1 Then
                Purchas.Show()
            ElseIf ComboBox1.SelectedIndex = 2 Then
                Billing.Show()
            ElseIf ComboBox1.SelectedIndex = 3 Then
                Report.Show()
            End If
            'Dim obj = New items
            'obj.Show()
            'Me.Hide()
        Else
            MsgBox("Wrong username or password")
            Username.Text = "" And password.Text = ""
        End If
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Application.Exit()


    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim obj = New Billing
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class