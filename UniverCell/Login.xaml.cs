using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            IniciarMariaDB();
        }

        private void IniciarMariaDB()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "mysql_start.bat";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            PruebaConexion();

        }

        //Probar si la conexion está abierta
        void PruebaConexion()
        {
               if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config.ucll") == false)
                {
                Configuracion WinConf = new Configuracion();
                WinConf.Show();
                if (MessageBox.Show("Aun no se ha configurado el acceso al sistema. ¿Desea realizar la configuracion ahora?", "Alerta", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    WinConf.Close();
                }
                this.Close();
            }
        }

        /// <summary>
        /// La funcion obtiene los datos de un usuario basado en el nombre de usuario y lo almacena en 
        /// la clase estatica sesión
        /// </summary>
        /// <param name="usuario"></param>
        public void DatosUsuario(string usuario)
        {
            try
            {
                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM cellmax.usuarios where nombre_usuario = '"+usuario+"';", Conexion.conect);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Sesion.id_usuario = reader.GetString("id");
                    Sesion.nomb_usuario = reader.GetString("nombre_usuario");
                    Sesion.nomb_completo = reader.GetString("nombre_completo");
                    Sesion.puest = reader.GetString("puesto");
                    Sesion.dir = reader.GetString("direccion");
                    Sesion.lvl = reader.GetString("lvl");
                    Sesion.ced = reader.GetString("cedula");
                    Sesion.fech_ingr = reader.GetString("fecha_ingreso");
                }
                Conexion.conect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void button_Click(object sender, RoutedEventArgs e)
        {
            string usr = txt_user.Text;
            try
            {
                //Conectar con la base de datos para consultar el inventaio
                Conexion.conect.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexion.conect;

                cmd.CommandText = "select cellmax.login('" + txt_user.Text + "', '" + txt_pass.Password + "')";
                /*loguear autimaticamente (temporal)
                *cmd.CommandText = "select cellmax.login('luishck', '123')";
                */
                int login = int.Parse(cmd.ExecuteScalar().ToString());
                Conexion.conect.Close();

                //Si el login es correcto mandar a la ventana principal
                if (login == 1)
                {
                    DatosUsuario(usr);

                    MainWindow VentPrincipal = new MainWindow();
                    VentPrincipal.Show();
                    this.Close();
                }
                //Caso contrario mandar un mensaje de error
                else
                {
                    MessageBox.Show("Usuario Incorrecto");
                }

            }
            //Si ocurre un error con la conexion a la BD mandar una excepcion
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocurió un error" + ex);
            }
        }
    }
}

