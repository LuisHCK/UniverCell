using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverCell
{
    /// <summary>
    /// 
    /// </summary>
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
                if (Rec_txt_box_cantidad.Value > 0)
                {
                    decimal recarga = Convert.ToDecimal(Rec_txt_box_cantidad.Value);
                    int id = (Combo_id_comp.SelectedIndex) + 1;

                    Conexion.conect.Open();
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO recargas (saldo_id, valor) VALUES (@saldo_id, @valor);", Conexion.conect);
                    cmd.Parameters.Add(new SQLiteParameter("@saldo_id", Combo_id_comp.SelectedValue));
                    cmd.Parameters.Add(new SQLiteParameter("@valor", Rec_txt_box_cantidad.Value));
                    cmd.ExecuteNonQuery();
                    Conexion.conect.Close();

                    ActualizarTablaRecargas();
                    EstadisticasRecargas();
                    Rec_txt_box_cantidad.Value = null;
                }
                else
                {
                    MessageBox.Show("La Cantidad no es válida", "Error");
                }
            }
            catch(SQLiteException ex)
            {
                MessageBox.Show("Ocurrió un error al realizar la venta, por favor verifica los datos e intentalo nuevamente" +ex, "Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ActualizarTablaRecargas()
        {
            DataTable dt = new DataTable();
            Conexion.conect.Open();
            string query = "SELECT saldo_recargas.compania, recargas.valor, saldo_recargas.ganancia, recargas.fecha_venta FROM recargas INNER JOIN saldo_recargas on recargas.saldo_id = saldo_recargas.id order by fecha_venta desc;";

            using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                da.Fill(dt);
            Conexion.conect.Close();
            Console.WriteLine("Operacion realizada");
            dataGrid_venta_recargas.ItemsSource = dt.DefaultView;
        }


        private void actualizar_tabla_recargas_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTablaRecargas();
        }
        ///Generar estadísticas de las recargas vendidas 
        public void EstadisticasRecargas()
        {
            try
            {
                Conexion.conect.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM saldo_recargas;", Conexion.conect);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader["id"].ToString();

                    if (id == "1")
                    {
                        Tile_Recargas_1.Title = ("Saldo en: " + reader["compania"].ToString());
                        lbl_saldo_1.Content = Tienda.signo_moneda + ": " + reader["saldo"].ToString();
                    }
                    else if (id == "2")
                    {
                        Tile_Recargas_2.Title = ("Saldo en: " + reader["compania"].ToString());
                        lbl_saldo_2.Content = Tienda.signo_moneda + ": " + reader["saldo"].ToString();
                    }
                }
                Conexion.conect.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al obtener los datos: " + ex, "Error");
            }
        }

        ///Abrir un formulario para actualizar el saldo de recargas
        private void Tile_Recargas_1_Click(object sender, RoutedEventArgs e)
        {
            Editar_Saldo_Recargas Editar_Saldo = new Editar_Saldo_Recargas();
            var dialogResult = Editar_Saldo.ShowDialog();
            EstadisticasRecargas();
        }
    }
}
