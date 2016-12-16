using MySql.Data.MySqlClient;
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

namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Ajustes.xaml
    /// </summary>
    public partial class Ajustes : Window
    {
        private MainWindow VentanaPrincipal;
        /// <summary>
        /// Inicializar ventana
        /// </summary>
        public Ajustes(MainWindow parent)
        {
            VentanaPrincipal = parent;
            Owner = parent;
            InitializeComponent();
            ObtenerDatosTienda();
        }

        //Leer los datos de la tienda
        private void ObtenerDatosTienda()
        {
            //Las monedas se obtienen mediante una funcion especial para los ComboBox
            Moneda.CargarMonedas(this);

            try
            {
                Conexion.conect.Open();
                string Comando = "Select*From ajustes;";
                MySqlCommand cmd = new MySqlCommand(Comando, Conexion.conect);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    nombre_tienda.Text = Reader.GetString("nombre_negocio");
                    telefono_tienda.Text = Reader.GetString("telefono");
                    email_tienda.Text = Reader.GetString("email");
                    ciudad_tienda.Text = Reader.GetString("ciudad");
                    direccion_tienda.Text = Reader.GetString("direccion");

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
            int Moned = Moneda.IDMoneda(moneda_tienda.SelectedItem.ToString());

            string CadenaCrear = "INSERT INTO `cellmax`.`ajustes` (`nombre_negocio`, `moneda_id`, `email`, `ciudad`, `telefono`, `direccion`) VALUES ('" + Nombre + "', '" + Moned + "', '" + Email + "', '" + Ciudad + "', "+ Telefono +", '" + Direccion + "');";

            string CadenaActualizar = "UPDATE `cellmax`.`ajustes` SET `nombre_negocio`='" + Nombre + "', `moneda_id`=" + Moned + ", `email`='" + Email + "', `ciudad`='" + Ciudad + "', `telefono`='" + Telefono + "', `direccion`='" + Direccion + "' WHERE `id`='1';";
            try
            {
                string Cadena = null;
                //Verificar si se deben crear nuevos datos o actualizarlos
                if (Nombre == "" && Telefono == "" && Email == "" && Ciudad == "" && Direccion == "")
                {
                    Cadena = CadenaCrear;
                }
                else
                {
                    Cadena = CadenaActualizar;
                }

                Conexion.conect.Open();
                MySqlCommand CMD = new MySqlCommand(Cadena, Conexion.conect);
                CMD.ExecuteNonQuery();
                Conexion.conect.Close();
                VentanaPrincipal.Activate();
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al guardar los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
            Close();
            VentanaPrincipal.ActualizarDatosTienda();
            
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
