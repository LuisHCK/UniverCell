using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Editar_Saldo_Recargas.xaml
    /// </summary>
    public partial class Editar_Saldo_Recargas : Window
    {
        /// <summary>
        /// Perite editar el saldo de las recargas
        /// </summary>
        public Editar_Saldo_Recargas()
        {
            InitializeComponent();
        }
        private static string saldo;

        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Console.WriteLine("Item: "+ Combo_id_comp.SelectedValue);
                object conect = Conexion.conect;
                Conexion.conect.Open();
                
                //Sumar la cantidad existente mas la cantidad comprada
                decimal cantidad = Convert.ToDecimal(textBox_cantidad_agregada.Text) + Convert.ToDecimal(saldo);

                string comando = "UPDATE saldo_recargas SET saldo = @nuevo_saldo WHERE id=@saldo_id";

                SQLiteCommand CMD = new SQLiteCommand(comando, Conexion.conect);
                CMD.Parameters.AddWithValue("@nuevo_saldo", cantidad);
                CMD.Parameters.AddWithValue("@saldo_id", Combo_id_comp.SelectedIndex+1);

                CMD.ExecuteNonQuery();
                Conexion.conect.Close();

               if (MessageBox.Show( "La operación se realizó con éxito", "Realizado", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al actualizar el saldo, verifica que los datos introducidos son corretos", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Obtiene el saldo de recargas
        /// </summary>
        public void obtenerSaldo()
        {
            string query = "SELECT saldo FROM saldo_recargas WHERE id = @saldo_id;";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.conect);
            cmd.Parameters.AddWithValue("@saldo_id", Combo_id_comp.SelectedIndex+1);

            try
            {
                if ( Conexion.conect.State == System.Data.ConnectionState.Open) { Conexion.conect.Close(); }

                Conexion.conect.Open();

                SQLiteDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    saldo = Reader["saldo"].ToString();
                }         
                txt_cantidad_actual.Text = saldo;

                Conexion.conect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex, "Error");
            }
        }

        private void Combo_id_comp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            obtenerSaldo();
        }
    }
}
