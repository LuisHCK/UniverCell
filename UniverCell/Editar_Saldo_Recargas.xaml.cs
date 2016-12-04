using MySql.Data.MySqlClient;
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
        public Editar_Saldo_Recargas()
        {
            InitializeComponent();
            Obtener_companias();
        }
        Conexion CON = new Conexion();
        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Console.WriteLine("Item: "+ Combo_id_comp.SelectedValue);

                CON.conect.Open();
                decimal cantidad = Convert.ToDecimal(textBox_cantidad_agregada.Text);
                string comando = "call cellmax.comprar_saldo(null, '" + Combo_id_comp.SelectedValue.ToString() + "', " + cantidad + ");";

                MySqlCommand CMD = new MySqlCommand(comando, CON.conect);

                CMD.ExecuteNonQuery();
                CON.conect.Close();

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

        public void Obtener_companias()
        {
            CON.conect.Open();
            MySqlCommand CMD = new MySqlCommand("select saldo_recargas.id, saldo_recargas.compania FROM saldo_recargas;", CON.conect);

            MySqlDataReader reader = CMD.ExecuteReader();

            while (reader.Read())
            {
                Combo_id_comp.Items.Add(reader.GetString("compania"));
            }
            CON.conect.Close();
        }
    }
}
