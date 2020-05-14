
Imports MySql.Data.MySqlClient
Public Class Form1
    Dim conexion As MySqlConnection
    Dim cmd As New MySqlCommand

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conexion = New MySqlConnection

        Dim cmd As New MySqlCommand
        conexion.ConnectionString = "server=localhost;database=veterinaria;Uid=root;Pwd=;"
        If txtName.Text <> "" And txtRaza.Text <> "" And txtColor.Text <> "" Then
            Try
                conexion.Open()
                cmd.Connection = conexion

                cmd.CommandText = "INSERT INTO perros(nombre,raza,color) VALUES (@nombre,@raza,@color)"
                cmd.Parameters.AddWithValue("@nombre", txtName.Text)
                cmd.Parameters.AddWithValue("@raza", txtRaza.Text)
                cmd.Parameters.AddWithValue("@color", txtColor.Text)
                cmd.ExecuteNonQuery()

                MsgBox("Datos registrados correctamente.")
                conexion.Close()

                ActualizarSelect()



            Catch ex As Exception
                MsgBox("Error en la conexion a la base de datos, error:" & ex.Message)

            End Try
        Else
            MsgBox("No pueden haber campos vacios")


        End If



    End Sub




    Sub ActualizarSelect()
        Dim ds As DataSet = New DataSet

        Dim adaptador As MySqlDataAdapter = New MySqlDataAdapter

        '
        conexion = New MySqlConnection


        conexion.ConnectionString = "server=localhost; database=veterinaria;Uid=root;Pwd=;"

        Try
            conexion.Open()

            cmd.Connection = conexion

            cmd.CommandText = "SELECT * FROM perros ORDER BY nombre ASC"
            adaptador.SelectCommand = cmd
            adaptador.Fill(ds, "Tabla")
            DataGridView1.DataSource = ds
            DataGridView1.DataMember = "Tabla"

            conexion.Close()




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

















    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActualizarSelect()

    End Sub







    Private Sub DataGridView1_SelectionChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        If (DataGridView1.SelectedRows.Count > 0) Then
            txtName2.Text = DataGridView1.Item("nombre", DataGridView1.SelectedRows(0).Index).Value
            txtRaza2.Text = DataGridView1.Item("raza", DataGridView1.SelectedRows(0).Index).Value
            txtColor2.Text = DataGridView1.Item("color", DataGridView1.SelectedRows(0).Index).Value
            ID.Text = DataGridView1.Item("id", DataGridView1.SelectedRows(0).Index).Value
            
        End If
    End Sub

    Private Sub updateButton_Click_1(sender As Object, e As EventArgs) Handles updateButton.Click
        Dim result1 As DialogResult = MessageBox.Show("Seguro que quieres actualizar?", "Actualizar", MessageBoxButtons.YesNo)

        If result1 = DialogResult.Yes Then
            conexion = New MySqlConnection
            conexion.ConnectionString = "server=localhost; database=veterinaria;Uid=root;Pwd=;"

            Try
                conexion.Open()
                cmd.Connection = conexion
                cmd.CommandText = "UPDATE perros SET nombre=@nombre, raza=@raza, color=@color WHERE id=@id"
                cmd.Prepare()

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@nombre", txtName2.Text)
                cmd.Parameters.AddWithValue("@raza", txtRaza2.Text)

                cmd.Parameters.AddWithValue("@color", txtColor2.Text)

                cmd.Parameters.AddWithValue("@id", ID.Text)

                cmd.ExecuteNonQuery()

                MsgBox("fue actualizado correctamente")

                txtName2.Clear()

                txtRaza2.Clear()

                txtColor2.Clear()

                ID.Clear()

                conexion.Close()

                ActualizarSelect()






            Catch ex As Exception
                MsgBox("Error")
            End Try


        End If






    End Sub

    Private Sub deleteButton_Click(sender As Object, e As EventArgs) Handles deleteButton.Click
        Dim result1 As DialogResult = MessageBox.Show("Seguro que quieres eliminar", "Eliminar", MessageBoxButtons.YesNo)

        If result1 = DialogResult.Yes Then
            conexion = New MySqlConnection
            conexion.ConnectionString = "server=localhost; database=veterinaria;Uid=root;Pwd=;"

            Try
                conexion.Open()
                cmd.Connection = conexion
                cmd.CommandText = "DELETE FROM perros WHERE id=@id"
                cmd.Prepare()

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@id", ID.Text)

                cmd.ExecuteNonQuery()
                ID.Clear()




                conexion.Close()

                MsgBox("fue eliminado correctamente")
                txtName2.Clear()
                txtRaza2.Clear()
                txtColor2.Clear()

                ActualizarSelect()


            Catch ex As Exception
                MsgBox(ex)
            End Try
        End If






    End Sub
End Class
