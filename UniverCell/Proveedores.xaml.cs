using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;

namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Editar_articulo.xaml
    /// </summary>
    public partial class Proveedores : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public Proveedores()
        {
            InitializeComponent();
            ActualizarTablaProv();
        }

        void ActualizarTablaProv()
        {
            Archivos arc = new Archivos();
            try
            {
                if (Conexion.conect.State == ConnectionState.Open)
                {
                    Conexion.conect.Close();
                }
                arc.ActualizarTabla(this.dataGrid, "SELECT * From cellmax.proveedores;");
            }
            catch(Exception)
            {
                MessageBox.Show("No se pudieron obtener información de la base de datos");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_nombre.Text == "")
            {
                return;
            }
            string nomb = textBox_nombre.Text;
            string dir = textBox_dir.Text;
            string tel = textBox_tel.Text;
            string email = textBox_email.Text;
            string id = textBox_ID.Text;

            string comando = null;
            string mensaje = null;
            if (btn_guardar.Content.ToString() == "Actualizar")
            {
                comando = "UPDATE `cellmax`.`proveedores` SET `nombre`='" + nomb + "', `telefono`='" + tel + "', `direccion`='" + dir + "', `email`='" + email + "', `fecha_actualizacion`= CURRENT_TIMESTAMP WHERE `id`='" + id + "';";
                mensaje = "Se actualizó correctamente la información del proveedor";
            }
            else
            {
                comando = "INSERT INTO `cellmax`.`proveedores` (`nombre`, `telefono`, `direccion`, `email`) VALUES ('" + nomb + "', '" + tel + "', '" + dir + "', '" + email + "');";
                mensaje = "Se guardó correctamente el nuevo proveedor";
            }

            try
            {
                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand(comando,Conexion.conect);
                cmd.ExecuteNonQuery();
                Conexion.conect.Close();
                ActualizarTablaProv();
                LimpiarForProveedor();

                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al crear o modificar el Proveedor", "Error");
            }
        }

        private void LimpiarForProveedor()
        {
            textBox_nombre.Text = null;
            textBox_dir.Text = null;
            textBox_tel.Text = null;
            textBox_email.Text = null;
            textBox_ID.Text = null;
            btn_guardar.Content = "Guardar";
        }

        private void btn_editar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                textBox_nombre.Text = (drv.Row[1]).ToString();
                textBox_tel.Text = (drv.Row[2]).ToString();
                textBox_dir.Text = (drv.Row[3]).ToString();
                textBox_email.Text = (drv.Row[4]).ToString();
                textBox_ID.Text = (drv.Row[0]).ToString();
                btn_guardar.Content = "Actualizar";
            }
            catch
            {
                MessageBox.Show("El valor seleccionado no es correcto","Error");
                LimpiarForProveedor();
            }
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                string id = (drv.Row[0]).ToString();

                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `cellmax`.`proveedores` WHERE `id`= '" + id + "';", Conexion.conect);
                cmd.ExecuteNonQuery();
                Conexion.conect.Close();
                ActualizarTablaProv();
                MessageBox.Show("Se ha eliminado el proveedor correctamente", "Listo");
            }
            catch
            {
                MessageBox.Show("El valor seleccionado no es correcto", "Error");
                LimpiarForProveedor();
            }
        }
    }
}
