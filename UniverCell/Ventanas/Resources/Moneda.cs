using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UniverCell
{
    /// <summary>
    /// Esta una coleccion de funciones utiles para facilitar la manipulacion de las
    /// monedas existentes en la BD
    /// </summary>
    public class Moneda
    {
        ///Obtener el nombre de una moneda mediante su id
        public static string Nombre(int id)
        {
            string Nombre = null;
            try
            {
                Conexion.conect.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT nombre FROM monedas where id = '"+ id +"';", Conexion.conect);
                SQLiteDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Nombre = Reader["nombre"].ToString();
                }
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudo encontrar ninguna moneda con el ID: "+ id, "Error",MessageBoxButton.OK,MessageBoxImage.Error,MessageBoxResult.OK,MessageBoxOptions.ServiceNotification);
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
                SQLiteCommand cmd = new SQLiteCommand("SELECT id FROM monedas where nombre = '" + nombre + "';", Conexion.conect);
                SQLiteDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    ID = Convert.ToInt32(Reader["id"]);
                }
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudo encontrar ninguna moneda con el Nombre: " + nombre, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
            return ID;
        }

        /// <summary>
        /// Devolver el signo de la moneda 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string Signo(int ID)
        {
            string Simbolo = null;
            try
            {
                Conexion.conect.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT simbolo FROM monedas where id = '"+ID+"';", Conexion.conect);
                SQLiteDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Simbolo = Reader["simbolo"].ToString();
                }
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudo encontrar ninguna moneda con el ID: " + ID, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
            return Simbolo;
        }


        ///Llenar un ComboBox con las monedas guardadas en la BD
        public static void CargarMonedas()
        {
            try {
                Conexion.conect.Open();
                SQLiteCommand CMD = new SQLiteCommand("Select nombre From monedas;", Conexion.conect);
                SQLiteDataReader Reader = CMD.ExecuteReader();

                while(Reader.Read())
                {
                    Tienda.ListaMonedas.Add(Reader["nombre"].ToString());
                }
                Conexion.conect.Close();
            }
            catch(Exception ex)
            {
                Conexion.conect.Close();
                MessageBox.Show("Ocurrió un error al cargar la lista de monedas." + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CargarMonedas1(ComboBox Combo)
        {
            try
            {
                Conexion.conect.Open();
                SQLiteCommand CMD = new SQLiteCommand("Select nombre From monedas;", Conexion.conect);
                SQLiteDataReader Reader = CMD.ExecuteReader();

                while (Reader.Read())
                {
                    Combo.Items.Add(Reader["nombre"].ToString());
                }
                Conexion.conect.Close();
            }
            catch (Exception ex)
            {
                Conexion.conect.Close();
                MessageBox.Show("Ocurrió un error al cargar la lista de monedas." + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
