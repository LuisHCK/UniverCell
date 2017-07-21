using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Data;

namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Ajustes.xaml
    /// </summary>
    public partial class Ajustes : Window
    {
        /// <summary>
        /// Inicializar ventana
        /// </summary>
        public Ajustes()
        {
            InitializeComponent();
            ObtenerDatosTienda();
        }
        //id ajustes
        int id = 0;

        //Leer los datos de la tienda
        private void ObtenerDatosTienda()
        {
            //Obtener las monedas 
            Moneda.CargarMonedas1(this.moneda_tienda);

            try
            {
                Conexion.conect.Open();
                string Comando = "Select*From ajustes;";
                SQLiteCommand cmd = new SQLiteCommand(Comando, Conexion.conect);
                SQLiteDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    id = Convert.ToInt32(Reader["id"]);
                    nombre_tienda.Text = Reader["nombre_negocio"].ToString();
                    telefono_tienda.Text = Reader["telefono"].ToString();
                    email_tienda.Text = Reader["email"].ToString();
                    ciudad_tienda.Text = Reader["ciudad"].ToString();
                    direccion_tienda.Text = Reader["direccion"].ToString();
                }
                Conexion.conect.Close();
            }
            catch
            {

            }
        }

        //Guardar los datos de la tienda en la BD
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string Nombre = nombre_tienda.Text;
            string Telefono = telefono_tienda.Text;
            string Email = email_tienda.Text;
            string Ciudad = ciudad_tienda.Text;
            string Direccion = direccion_tienda.Text;
            //int Moned = Moneda.IDMoneda(moneda_tienda.SelectedItem.ToString());

            string CadenaCrear = "INSERT INTO ajustes (`nombre_negocio`, `email`, `ciudad`, `telefono`, `direccion`) VALUES ('" + Nombre + "',  '" + Email + "', '" + Ciudad + "', '"+ Telefono +"', '" + Direccion + "');";

            string CadenaActualizar = "UPDATE ajustes SET `nombre_negocio`='" + Nombre + "',`email`='" + Email + "', `ciudad`='" + Ciudad + "', `telefono`='" + Telefono + "', `direccion`='" + Direccion + "';";
            try
            {
                string Cadena = null;
                //Verificar si se deben crear nuevos datos o actualizarlos
                if (id == 0)
                {
                    Cadena = CadenaCrear;
                }
                else
                {
                    Cadena = CadenaActualizar;
                }

                if (Conexion.conect.State == ConnectionState.Open) { Conexion.conect.Close(); }
                Conexion.conect.Open();
                SQLiteCommand CMD = new SQLiteCommand(Cadena, Conexion.conect);
                CMD.ExecuteNonQuery();
                Conexion.conect.Close();
                MessageBox.Show(Cadena);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar los datos" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
            Close();
            //VentanaPrincipal.ActualizarDatosTienda();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            Archivos.CargarImagen("Ajustes", "Logo", 0);
            CargarImagen();            
        }
        /// <summary>
        /// Cargar Imagen desde los archivos
        /// </summary>
        private void CargarImagen()
        {
            string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\logo.jpg";
            FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            logo_tienda.Source = BitmapFrame.Create(stream,BitmapCreateOptions.None,BitmapCacheOption.OnLoad);
            stream.Close();
        }
    }
}
