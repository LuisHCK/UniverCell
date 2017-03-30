using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace UniverCell
{
    class Caja
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


    }
}
