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
            Obtener_companias();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Console.WriteLine("Item: "+ Combo_id_comp.SelectedValue);
                object conect = Conexion.conect;
                Conexion.conect.Open();
                decimal cantidad = Convert.ToDecimal(textBox_cantidad_agregada.Text);
                string comando = "call comprar_saldo(null, '" + Combo_id_comp.SelectedValue.ToString() + "', " + cantidad + ");";

                SQLiteCommand CMD = new SQLiteCommand(comando, Conexion.conect);

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

        public void Obtener_companias()
        {
            Conexion.conect.Open();
            SQLiteCommand CMD = new SQLiteCommand("select saldo_recargas.id, saldo_recargas.compania FROM saldo_recargas;", Conexion.conect);

            SQLiteDataReader reader = CMD.ExecuteReader();

            while (reader.Read())
            {
                Combo_id_comp.Items.Add(reader["compania"].ToString());
            }
            Conexion.conect.Close();
        }
    }
}
