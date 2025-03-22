Imports System.Data.SqlClient
Public Class Report
    Dim con = New SqlConnection("Data Source=.\sqlexpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Groucery.mdf;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")

    Private Sub btn_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        If Date1.Enabled = True And Date2.Enabled = True Then
            con.open()
            Dim d1 As String = Date1.Value.ToString("yyyy-MM-dd")
            Dim d2 As String = Date2.Value.ToString("yyyy-MM-dd")
            Dim sql As String = "select * from BILLING1 where BILL_DATE between '" & d1 & "' and '" & d2 & "'"
            Dim cmd As New SqlCommand(sql, con)
            'cmd.Parameters.Add("date1", SqlDbType.DateTime).Value = d1
            'cmd.Parameters.Add("date2", SqlDbType.DateTime).Value = d2
            Dim count As New SqlCommand("select count(*) from BILLING1 where BILL_DATE between '" & d1 & "' and '" & d2 & "'", con)
            If count.ExecuteScalar > 0 Then
                Dim da As New SqlDataAdapter(cmd)
                Dim table As New DataTable("table")
                da.Fill(table)
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                DataGridView1.DataSource = table
            Else
                MsgBox("No data available between this range")
            End If
            con.close()
        End If
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Application.Exit()

       
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim obj = New login
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class