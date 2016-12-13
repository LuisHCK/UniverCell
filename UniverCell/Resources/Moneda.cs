using MySql.Data.MySqlClient;
using System.Windows;

namespace UniverCell
{
    /// <summary>
    /// Esta una coleccion de funciones utiles para facilitar la manipulacion de las
    /// monedas existentes en la BD
    /// </summary>
    public class Moneda
    {
        ///Obtener el nombre de una moneda mediante su id
        public static string NombreMoneda(int id)
        {
            string Nombre = null;
            try
            {
                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT* FROM cellmax.monedas where id = id;", Conexion.conect);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Nombre = Reader.GetString("nombre");
                }
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudo encontrar ninguna moneda con el ID"+ id, "Error",MessageBoxButton.OK,MessageBoxImage.Error,MessageBoxResult.OK,MessageBoxOptions.ServiceNotification);
            }
            return Nombre;
        }

        ///Obtener el ID de una moneda mediante su Nombre
        public static int IDMoneda(string nombre)
        {
            int ID = 0;
            try
            {
                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT* FROM cellmax.monedas where id = id;", Conexion.conect);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    ID = Reader.GetInt32("id");
                }
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudo encontrar ninguna moneda con el Nombre" + nombre, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
            return ID;
        }

        ///Llenar un ComboBox con las monedas guardadas en la BD
        public static void CargarMonedas(Ajustes ajustes)
        {
            try
            {
                Conexion.conect.Open();
                MySqlCommand CMD = new MySqlCommand("Select*From Monedas;", Conexion.conect);
                MySqlDataReader Reader = CMD.ExecuteReader();
                while (Reader.Read())
                {
                    var nombre = Reader.GetString("nombre");
                    ajustes.moneda_tienda.Items.Add(nombre);
                }
                Conexion.conect.Close();
            }
            catch
            {
                Conexion.conect.Close();
                MessageBox.Show("Ocurrió un error al cargar la lista de monedas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
