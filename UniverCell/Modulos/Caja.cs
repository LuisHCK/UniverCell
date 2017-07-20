using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Data;

namespace UniverCell
{
    public partial class MainWindow
    {
        public static void InicializarCaja()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM caja;", Conexion.conect);

            try
            {
                Int32 count = (Int32)cmd.ExecuteScalar();

                if (count < 1)
                {
                    double cantidad = 0;
                    string comando = "INSERT INTO caja(saldo,usuario_id) VALUES(@saldo,@usuario)";
                    SQLiteCommand cmd2 = new SQLiteCommand(comando, Conexion.conect);

                    var w = new SaldoInicial();
                    if (w.ShowDialog() == true)
                    {
                        cantidad = w.Cantidad;
                    }
                    cmd.Parameters.Add(new SQLiteParameter("@saldo", cantidad));
                    cmd.Parameters.Add(new SQLiteParameter("@usuario", Sesion.id_usuario));

                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Se inicializó el control de caja", "Exito");
                }
            }
            catch
            {
                MessageBox.Show("Ocurrió un fallo al inicializar el estado de caja", "Error");
            }
        }

        private void ActualizarCaja()
        {
            Conexion.conect.Open();
            DataTable dt = new DataTable();
            string query = "SELECT*FROM caja";

            datagrid_registros_caja.ItemsSource = null;

            using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                da.Fill(dt);
            datagrid_registros_caja.ItemsSource = dt.DefaultView;

            string query2 = "SELECT id, saldo FROM caja WHERE fecha_cierre = 'NA';";
            SQLiteCommand cmd = new SQLiteCommand(query2, Conexion.conect);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lbl_saldo_caja.Content = reader["saldo"].ToString();
                caja_id.Content = reader["id"].ToString();
            }
            Conexion.conect.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double saldo = Convert.ToDouble(lbl_saldo_caja.Content);
            double cantidad_a_dejar = saldo -= Convert.ToDouble(txt_saldo_dejar.Value);

            string query = "UPDATE caja SET fecha_cierre=@fecha_cierre, saldo=@saldo WHERE id=@caja_id;";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.conect);
            cmd.Parameters.AddWithValue("@fecha_cierre", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@caja_id", caja_id.Content.ToString());
            cmd.Parameters.AddWithValue("@saldo", cantidad_a_dejar.ToString());

            string query2 = "INSERT INTO caja(saldo, usuario_id) VALUES(@saldo, @usuario_id);";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, Conexion.conect);
            cmd2.Parameters.AddWithValue("@saldo", txt_saldo_dejar.Value.ToString());
            cmd2.Parameters.AddWithValue("@usuario_id", Sesion.id_usuario);

            try
            {
                Conexion.conect.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                MessageBox.Show("Se realizó el cierre de caja", "exito");
                Conexion.conect.Close();

                ActualizarCaja();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al realizar el cierre de caja: "+ex , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AgregarACaja(double cantidad)
        {
            double caja_actual = Convert.ToDouble(lbl_saldo_caja.Content);
            double nueva_cantidad = caja_actual + cantidad;

            MessageBox.Show("Cantidad: " + nueva_cantidad); 

            string query = "UPDATE caja SET saldo = @cantidad WHERE caja.id=@caja_id";
            SQLiteCommand cmd = new SQLiteCommand(query, Conexion.conect);
            cmd.Parameters.AddWithValue("@cantidad", nueva_cantidad);
            cmd.Parameters.AddWithValue("@caja_id", caja_id.Content.ToString());

            try
            {
                Conexion.conect.Open();
                cmd.ExecuteNonQuery();
                Conexion.conect.Close();

                ActualizarCaja();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo actualizar caja : " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Tile_Saldo_Caja_Click(object sender, RoutedEventArgs e)
        {
            ActualizarCaja();
        }

    }
}
