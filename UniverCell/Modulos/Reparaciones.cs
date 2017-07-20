using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverCell
{
    public partial class MainWindow
    {
        /// <summary>
        /// Obtener el resgistro de reparaciones
        /// </summary>
        public void ActualizarTablaReparaciones()
        {
            try
            {
                Conexion.conect.Open();
                DataTable dt = new DataTable();
                string query = "SELECT * FROM reparaciones;";
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                    da.Fill(dt);
                dataGrid_Reparaciones.ItemsSource = dt.DefaultView;
                Conexion.conect.Close();
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show("Ocurrió un error al obtener los datos. Detalles del error: " + ex, "Error");
            }
        }

        private void check_bx_repuesto_Checked(object sender, RoutedEventArgs e)
        {
            lbl_precio_rep.IsEnabled = true;
            txt_box_rep_precio.IsEnabled = true;
            lbl_descr_rep.IsEnabled = true;
            txt_box_desc_repuesto.IsEnabled = true;
        }

        private void check_bx_repuesto_Unchecked(object sender, RoutedEventArgs e)
        {
            lbl_precio_rep.IsEnabled = false;
            txt_box_rep_precio.IsEnabled = false;
            lbl_descr_rep.IsEnabled = false;
            txt_box_desc_repuesto.IsEnabled = false;
        }

        private void radio_hardware_rep_Checked(object s, RoutedEventArgs e)
        {
            check_bx_repuesto.IsEnabled = true;
        }

        private void radio_hardware_rep_Unchecked(object s, RoutedEventArgs e)
        {
            check_bx_repuesto.IsEnabled = false;
        }

        //registrar una reparacion realizada
        private void btn_reparacion_Clic(object s, RoutedEventArgs e)
        {
            //Guardar el registro solo si los campos están llenos
            if (textBox_detalle_rep.Text != "" && txt_box_prec_rep.Value > 0)
            {
                //Obtener el tipo de trabajo que se está registrando
                string tipo = null;
                if (radio_hardware_rep.IsChecked == true)
                {
                    tipo = radio_hardware_rep.Content.ToString();
                }
                else if (radio_software_rep.IsChecked == true)
                {
                    tipo = radio_software_rep.Content.ToString();
                }

                string Comando = "INSERT INTO `reparaciones` (`tipo`, `detalles`, `observaciones`, `precio_repuesto`, `detalles_repuesto`, `precio`) VALUES ('" + tipo + "', '" + textBox_detalle_rep.Text + "', '" + _textBox_obs.Text + "', '" + txt_box_rep_precio.Value + "', '" + txt_box_desc_repuesto.Text + "', '" + txt_box_prec_rep.Value + "');";
                try
                {
                    if (Conexion.conect.State == ConnectionState.Open) { Conexion.conect.Close(); }
                    Conexion.conect.Open();
                    SQLiteCommand CMD = new SQLiteCommand(Comando, Conexion.conect);
                    CMD.ExecuteNonQuery();
                    Conexion.conect.Close();
                    MessageBox.Show("Se guardó correctamente. Imprimendo Recibo...", "Realizado", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    LimpiarFormReparacion();

                    AgregarACaja(Convert.ToDouble(txt_box_prec_rep.Value));
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar los datos " + ex, "Error");
                }
                //Actualizar la tabla
            }
            else
            {
                MessageBox.Show("Los datos no son correctos. Por favor verifícalos y vuelve a intentarlo.","Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
            ActualizarTablaReparaciones();
        }

        private void cancelar_rep__Click(object sender, RoutedEventArgs e)
        {
            LimpiarFormReparacion();
        }

        private void LimpiarFormReparacion()
        {
            textBox_detalle_rep.Text = null;
            _textBox_obs.Text = null;
            txt_box_rep_precio.Value = null;
            txt_box_desc_repuesto.Text = null;
            txt_box_prec_rep.Value = null;
        }
    }
}
