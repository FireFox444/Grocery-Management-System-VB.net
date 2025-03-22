Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class Billing
    Private Const ConnectionString As String = "Data Source=.\sqlexpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Groucery.mdf;Integrated Security=True;Encrypt=True;Trust Server Certificate=true"
    Dim WithEvents Pd As New PrintDocument
    Dim ppd As New PrintPreviewDialog
    Dim longpaper As Integer

    Private Sub AddBill()
        Dim cname As String
        Dim bdate As String
        cname = txt_cname.Text
        bdate = DateTimePicker1.Text
        con.Open()
        Dim cmd As New SqlCommand("insert into BILLING1 values('" & cname & "'," & GrdTotal & " ,'" & bdate.ToString & "' )", con)

        cmd.ExecuteNonQuery()
        MessageBox.Show("Bill saved Successfully")
        con.Close()
        'lbl_total.Text = "Total"

        DisplayItem()
    End Sub

    Private Sub UpdateItem()
        Dim newqty = stock - Convert.ToInt32(txt_qun.Text)
        If newqty > 0 Then
            con.Open()
            Dim cmd As New SqlCommand("UPDATE stock SET qun = '" & newqty & "' WHERE itname ='" & key & "'", con)
            cmd.ExecuteNonQuery()
            con.Close()
            DisplayItem()

        End If
    End Sub
    Dim i = 0, GrdTotal = 0
    Private con = New SqlConnection(ConnectionString)

    Public Sub New(con As Object)
        Me.con = con
    End Sub

    Private Sub Billing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DisplayItem()
    End Sub
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
    Private Sub btn_Addtobill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Addtobill.Click
        If txt_pri.Text = "" Or txt_qun.Text = "" Then
            MsgBox("enter quntity")
        ElseIf txt_itname.Text = "" Then
            MsgBox("select item")
        Else
            Dim rnum As Integer = billDGV.Rows.Add()
            i = i + 1
            Dim total = Convert.ToInt32(txt_qun.Text) * Convert.ToInt32(txt_pri.Text)
            billDGV.Rows.Item(rnum).Cells("Column1").Value = i
            billDGV.Rows.Item(rnum).Cells("Column2").Value = txt_itname.Text
            billDGV.Rows.Item(rnum).Cells("Column3").Value = txt_pri.Text
            billDGV.Rows.Item(rnum).Cells("Column4").Value = txt_qun.Text
            billDGV.Rows.Item(rnum).Cells("Column5").Value = total
            GrdTotal = GrdTotal + total
            lbl_total.Text = (Convert.ToString(GrdTotal))
            UpdateItem()
            AddBill()
            Reset()
            DisplayItem()


        End If
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub
    Private Sub Reset()

        txt_itname.Text = ""
        txt_cname.Text = ""
        txt_qun.Text = ""
        txt_pri.Text = ""

        key = 0
        stock = 0
    End Sub
    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        Reset()
    End Sub

    Private Sub btn_logout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_logout.Click
        Dim obj = New login
        obj.Show()
        Me.Hide()

    End Sub

    Dim stock = 0, key = 0
    Private Sub iteamsdgv_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles iteamsdgv.CellMouseClick
        Dim row As DataGridViewRow = iteamsdgv.Rows(e.RowIndex)
        txt_itname.Text = row.Cells(1).Value.ToString

        txt_pri.Text = row.Cells(3).Value.ToString
        If txt_itname.Text = "" Then
            key = 0
        Else
            key = Convert.ToString(row.Cells(1).Value.ToString)
            stock = Convert.ToInt32(row.Cells(2).Value.ToString)
        End If
    End Sub

    Private Sub billDGV_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles billDGV.CellContentClick

    End Sub
    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lbl_total.Text = "total"
        billDGV.Rows.Clear()
    End Sub

   

    Private Sub Pd_BeginPrint1(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Pd.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, 500)
        Pd.DefaultPageSettings = pagesetup
    End Sub

    Private Sub Pd_Print(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles Pd.PrintPage
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As New Integer
        Dim centermargin As New Integer
        Dim rightmargin As New Integer
        centermargin = Pd.DefaultPageSettings.PaperSize.Width / 2
        leftmargin = Pd.DefaultPageSettings.Margins.Left
        rightmargin = Pd.DefaultPageSettings.PaperSize.Width
        'font alignment
        Dim rightas As New StringFormat
        Dim center As New StringFormat
        rightas.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "---------------------------------------------------------------------------"

        e.Graphics.DrawString("Ghanshyam Grocery store", f14, Brushes.Black, centermargin, 2, center)
        e.Graphics.DrawString("Apc chatralaya ", f10, Brushes.Black, centermargin, 30, center)
        e.Graphics.DrawString("tel +91 6351787644", f8, Brushes.Black, centermargin, 45, center)

        e.Graphics.DrawString("Id", f8, Brushes.Black, 0, 60)
        e.Graphics.DrawString(":", f10, Brushes.Black, 50, 60)
        e.Graphics.DrawString("A3277", f8, Brushes.Black, 70, 60)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 75)
        e.Graphics.DrawString(":", f10, Brushes.Black, 50, 75)
        e.Graphics.DrawString("GHANSHYAM", f8, Brushes.Black, 70, 75)

        ' e.Graphics.DrawString("07/28/2023 | (18.00)", f8, Brushes.Black, 0, 90)

        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 100)

        e.Graphics.DrawString("Qty", f10b, Brushes.Black, 90, 90)

        Dim height As New Integer
        Dim i As Long

        billDGV.AllowUserToAddRows = False
        For row As Integer = 0 To billDGV.RowCount - 1
            height += 15
            e.Graphics.DrawString(billDGV.Rows(row).Cells(1).Value.ToString, f10, Brushes.Black, 0, 100 + height)
            e.Graphics.DrawString(billDGV.Rows(row).Cells(3).Value.ToString, f10, Brushes.Black, 100, 100 + height)
            i = billDGV.Rows(row).Cells(2).Value
            billDGV.Rows(row).Cells(1).Value = Format(i, "##,##0")
            e.Graphics.DrawString(billDGV.Rows(row).Cells(2).Value.ToString, f10, Brushes.Black, rightmargin, 100 + height, rightas)
        Next
        Dim hights2 As Integer
        bTotal()
        hights2 = 110 + height
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, hights2)
        e.Graphics.DrawString("Total : " & Format(total, "##,##0"), f10b, Brushes.Black, rightmargin, 15 + hights2, rightas)
        ' e.Graphics.DrawString("Qty", f10b, Brushes.Black, 90, 15 + hights2)

        e.Graphics.DrawString("Thanks for Shopping", f10, Brushes.Black, centermargin, 40 + hights2, center)
        e.Graphics.DrawString("Ghanshyam Grocery Store", f10, Brushes.Black, centermargin, 55 + hights2, center)
    End Sub
    Dim total As Integer
    Sub bTotal()

        total = lbl_total.Text
    End Sub
    Private Sub btn_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_print.Click
        ppd.Document = Pd
        ppd.ShowDialog()

    End Sub
End Class