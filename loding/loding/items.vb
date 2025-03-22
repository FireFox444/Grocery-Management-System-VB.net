Imports System.Data.SqlClient
Public Class items
    Dim con = New SqlConnection("Data Source=.\sqlexpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Groucery.mdf1;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        If itemname.Text = "" Or Quantity.Text = "" Or price.Text = "" Or catagory.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else

            Dim itname As String
            Dim qun As Integer
            Dim pri As Integer
            Dim cat As String


            itname = itemname.Text
            qun = Quantity.Text
            pri = price.Text
            cat = catagory.Text

            con.Open()
            Dim cmd As New SqlCommand("insert into stock values('" & itname & "'," & qun & "," & pri & ",'" & cat & "')", con)

            cmd.ExecuteNonQuery()
            MessageBox.Show("Data inserted")
            con.Close()
            DisplayItem()
            Clear()
        End If
    End Sub

    Private Sub items_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DisplayItem()
    End Sub
    Private Sub Clear()

        itemname.Text = ""
        Quantity.Text = ""
        price.Text = ""
        catagory.Text = ""
    End Sub

    Private Sub btn_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        Clear()
    End Sub

    Dim dt As New DataTable

    Private Sub DisplayItem()
        con.open()
        Dim cmd As New SqlCommand("select * from stock", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(da)
        Dim ds As New DataSet
        da.Fill(ds)
        iteamsdgv.DataSource = ds.Tables(0)
        con.close()

    End Sub

    Private Sub filterbycatagory()

        con.open()
        Dim cmd As New SqlCommand("select * from stock where cat = '" & serchbox.SelectedItem.ToString() & "'", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(da)
        Dim ds As New DataSet
        da.Fill(ds)
        iteamsdgv.DataSource = ds.Tables(0)
        con.close()

    End Sub

    Private Sub iteamsdgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles iteamsdgv.CellContentClick
        Dim row As DataGridViewRow = iteamsdgv.Rows(e.RowIndex)

        itemname.Text = row.Cells(1).Value.ToString
        Quantity.Text = row.Cells(2).Value.ToString
        price.Text = row.Cells(3).Value.ToString
        catagory.Text = row.Cells(4).Value.ToString
    End Sub

    Private Sub btn_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_edit.Click


        Dim iname As String
        Dim var_qun As Integer
        Dim var_pri As Integer
        Dim var_cat As String


        iname = itemname.Text
        var_qun = Quantity.Text
        var_pri = price.Text
        var_cat = catagory.SelectedItem.ToString()

        con.Open()
        Dim cmd As New SqlCommand("Update stock set qun='" & var_qun & "' , pri='" & var_pri & "',cat='" & var_cat & "' WHERE itname ='" & iname & "' ", con)
        cmd.ExecuteNonQuery()
        MessageBox.Show("Data Updated")
        con.Close()
        DisplayItem()
        Clear()
    End Sub

    Private Sub btn_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete.Click
        Dim var_itname As String = Console.ReadLine


        var_itname = itemname.Text


        con.Open()
        Dim cmd As New SqlCommand("Delete From stock where itname ='" & var_itname & "'", con)
        cmd.ExecuteNonQuery()
        MessageBox.Show("Data Deleted")
        con.Close()
        DisplayItem()
        Clear()
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub btn_logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_logout.Click
        Dim obj = New login
        obj.Show()
        Me.Hide()
    End Sub


    Private Sub serchbox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles serchbox.SelectionChangeCommitted
        filterbycatagory()
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        DisplayItem()
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        Dim obj = New Purchas
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub serchbox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles serchbox.SelectedIndexChanged

    End Sub
End Class

