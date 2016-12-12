using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverCell
{
    public partial class MainWindow
    {
        private void button4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EstadisticasRecargas();
        }

        private void btn_vender_recarga_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (Rec_txt_box_cantidad.Value != 0 || Rec_txt_box_cantidad.Value != null)
                {
                    decimal recarga = Convert.ToDecimal(Rec_txt_box_cantidad.Value);
                    int id = (Combo_id_comp.SelectedIndex) + 1;

                    Conexion.conect.Open();
                    MySqlCommand cmd = new MySqlCommand("call cellmax.vender_recarga(" + recarga + ", " + id + ");", Conexion.conect);
                    cmd.ExecuteNonQuery();
                    Conexion.conect.Close();

                    ActualizarTablaRecargas();
                    EstadisticasRecargas();
                    Rec_txt_box_cantidad.Value = null;
                }
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al realizar la venta, por favor verifica los datos e intentalo nuevamente", "Error");
            }
        }

        public void ActualizarTablaRecargas()
        {
            DataTable dt = new DataTable();
            Conexion.conect.Open();
            string query = "SELECT saldo_recargas.compania, recargas.valor, saldo_recargas.ganancia, recargas.fecha_venta FROM cellmax.recargas INNER JOIN saldo_recargas on recargas.saldo_id = saldo_recargas.id order by fecha_venta desc;";

            using (MySqlDataAdapter da = new MySqlDataAdapter(query, Conexion.conect))
                da.Fill(dt);
            Conexion.conect.Close();
            Console.WriteLine("Operacion realizada");
            dataGrid_venta_recargas.ItemsSource = dt.DefaultView;
        }
        
        //Generar estadísticas de las recargas vendidas 
        public void EstadisticasRecargas()
        {
            try
            {
                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM cellmax.saldo_recargas;", Conexion.conect);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader.GetString("id");

                    if (id == "1")
                    {
                        Tile_Recargas_1.Title = ("Saldo en: " + reader.GetString("compania"));
                        lbl_saldo_1.Content = reader.GetString("saldo");
                    }
                    else if (id == "2")
                    {
                        Tile_Recargas_2.Title = ("Saldo en: " + reader.GetString("compania"));
                        lbl_saldo_2.Content = reader.GetString("saldo");
                    }
                }
                Conexion.conect.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al obtener los datos: " + ex, "Error");
            }
        }

        private void Tile_Recargas_1_Click(object sender, RoutedEventArgs e)
        {
            Editar_Saldo_Recargas Editar_Saldo = new Editar_Saldo_Recargas();
            Editar_Saldo.Show();
        }

        private void Tile_Recargas_2_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
