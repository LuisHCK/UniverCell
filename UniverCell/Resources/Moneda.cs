using MySql.Data.MySqlClient;
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
                MySqlCommand cmd = new MySqlCommand("SELECT nombre FROM cellmax.monedas where id = "+ id +";", Conexion.conect);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Nombre = Reader.GetString("nombre");
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
                MySqlCommand cmd = new MySqlCommand("SELECT id FROM cellmax.monedas where nombre = '" + nombre + "';", Conexion.conect);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    ID = Reader.GetInt32("id");
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
                MySqlCommand cmd = new MySqlCommand("SELECT simbolo FROM cellmax.monedas where id = "+ID+";", Conexion.conect);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Simbolo = Reader.GetString("simbolo");
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
                MySqlCommand CMD = new MySqlCommand("Select nombre From Monedas;", Conexion.conect);
                MySqlDataReader Reader = CMD.ExecuteReader();

                while(Reader.Read())
                {
                    Tienda.ListaMonedas.Add(Reader.GetString("nombre"));
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
                MySqlCommand CMD = new MySqlCommand("Select nombre From Monedas;", Conexion.conect);
                MySqlDataReader Reader = CMD.ExecuteReader();

                while (Reader.Read())
                {
                    Combo.Items.Add(Reader.GetString("nombre"));
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
