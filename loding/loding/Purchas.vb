Imports System.Data.SqlClient
Public Class Purchas
    Dim con = New SqlConnection("Data Source=.\sqlexpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Groucery.mdf;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub items_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DisplayItem()
    End Sub
    Private Sub Clear()

        itemname.Text = ""
        Quantity.Text = ""
        txt_price.Text = ""
        catagory.Text = ""
    End Sub

    Private Sub DisplayItem()
        con.open()
        Dim cmd As New SqlCommand("select * from PURCHES1", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(da)
        Dim ds As New DataSet
        da.Fill(ds)
        iteamsdgv.DataSource = ds.Tables(0)
        con.close()

    End Sub

    Private Sub iteamsdgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim row As DataGridViewRow = iteamsdgv.Rows(e.RowIndex)
        itemname.Text = row.Cells(0).Value.ToString
        Quantity.Text = row.Cells(1).Value.ToString
        txt_price.Text = row.Cells(2).Value.ToString
        catagory.Text = row.Cells(3).Value.ToString
    End Sub

    Private Sub btn_logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim obj = New login
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub btn_save_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        If itemname.Text = "" Or Quantity.Text = "" Or catagory.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else

            Dim iname As String
            Dim qun As Integer
            Dim pri As Integer
            Dim cat As String
            Dim pdate As String


            iname = itemname.Text
            qun = Quantity.Text
            pri = txt_price.Text
            cat = catagory.Text
            pdate = DateTimePicker1.Text

            con.Open()
            Dim cmd As New SqlCommand("insert into PURCHES1 values('" & iname & "'," & qun & "," & pri & ",'" & cat & "','" & pdate & "')", con)

            cmd.ExecuteNonQuery()


            Dim cmd_count As New SqlCommand("select count(*) from stock where itname='" & iname & "' ", con)


            Dim count As Int32 = Convert.ToInt16(cmd_count.ExecuteScalar())

            MsgBox(count.ToString())


            If count > 1 Then
                Dim new_qun As Integer


                Dim cmd_quan As New SqlCommand("select qun from stock where itname='" & iname & "' ", con)

                Dim old_qun As Integer = cmd_quan.ExecuteScalar()

                new_qun = old_qun + qun

                Dim cmd1 As New SqlCommand("Update stock set  qun='" & new_qun & "' , pri='" & pri & "',cat='" & cat & "' where itname ='" & iname & "'   ", con)
                cmd1.ExecuteNonQuery()
            Else
                Dim cmd1 As New SqlCommand("insert into stock values('" & iname & "'," & qun & "," & pri & ",'" & cat & "')", con)

                cmd1.ExecuteNonQuery()
            End If



            MessageBox.Show("Data inserted")
            con.Close()
            DisplayItem()
            Clear()
        End If
    End Sub

    Private Sub btn_clear_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        Clear()
    End Sub


    Private Sub btn_logout_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_logout.Click
        Dim obj = New login
        obj.Show()
        Me.Hide()

    End Sub

    Private Sub Quantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Quantity.TextChanged

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        Dim obj = New items
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub DisplayItem1()
        con.open()
        Dim cmd As New SqlCommand("select * from stock", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(da)
        Dim ds As New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        con.close()


    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        itemname.Text = row.Cells(1).Value.ToString
        Quantity.Text = row.Cells(2).Value.ToString
        txt_price.Text = row.Cells(3).Value.ToString
        catagory.Text = row.Cells(4).Value.ToString
    End Sub

    Private Sub Purchas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DisplayItem1()
        DisplayItem()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub iteamsdgv_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles iteamsdgv.CellContentClick

    End Sub
End Class

