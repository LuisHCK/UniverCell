using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverCell
{
    public partial class MainWindow
    {
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
         
            string Comando = "INSERT INTO `cellmax`.`reparaciones` (`tipo`, `detalles`, `observaciones`, `precio_repuesto`, `detalles_repuesto`, `precio`) VALUES ('"+tipo+"', '"+ textBox_detalle_rep.Text+ "', '"+_textBox_obs.Text + "', '"+ txt_box_rep_precio.Value + "', '"+ txt_box_desc_repuesto.Text + "', '"+ txt_box_prec_rep.Value + "');";
            try
            {
                Conexion.conect.Open();
                MySqlCommand CMD = new MySqlCommand(Comando, Conexion.conect);
                CMD.ExecuteNonQuery();
                Conexion.conect.Close();
                MessageBox.Show("Se guardó correctamente. ¿Desea imprimir un recibo?","Realizado",MessageBoxButton.YesNo,MessageBoxImage.Question);
                LimpiarFormReparacion();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Ocurrió un error al guardar los datos " + ex, "Error");
            }
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
