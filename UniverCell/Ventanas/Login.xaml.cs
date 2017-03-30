using System.Data.SQLite;
using System;
using System.Windows;

namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Inicializar Ventana
        /// </summary>
        public Login()
        {
            InitializeComponent();
            PruebaConexion();
            //IniciarMariaDB();
        }

        // Dejar MySql y usar SqLite
        //private void IniciarMariaDB()
        //{
        //    Process proc = new Process();
        //    proc.StartInfo.FileName = "C:\\xampp\\xampp_start.exe";
        //    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    proc.Start();
        //    //Esperar a que cargue MySQL
        //    System.Threading.Thread.Sleep(5000);
        //    
        //}

        //Probar si la conexion está abierta
        void PruebaConexion()
        {
            try
            {
                Conexion.conect.Open();
                Conexion.conect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
            //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config.ucll") == false)
            // {
            // Configuracion WinConf = new Configuracion();
            // WinConf.Show();
            // if (MessageBox.Show("Aun no se ha configurado el acceso al sistema. ¿Desea realizar la configuracion ahora?", "Alerta", MessageBoxButton.YesNo) == MessageBoxResult.No)
            // {
            //     WinConf.Close();
            // }
            // this.Close();
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
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM usuarios where nombre_usuario = '" + usuario + "';", Conexion.conect);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Sesion.id_usuario = reader["id"].ToString();
                    Sesion.nomb_usuario = reader["nombre_usuario"].ToString();
                    Sesion.nomb_completo = reader["nombre_completo"].ToString();
                    Sesion.puest = reader["puesto"].ToString();
                    Sesion.dir = reader["direccion"].ToString();
                    Sesion.lvl = reader["lvl"].ToString();
                    Sesion.ced = reader["cedula"].ToString();
                    Sesion.fech_ingr = reader["fecha_ingreso"].ToString();
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
                Conexion.conect.Open();

                SQLiteCommand cmd = new SQLiteCommand("select Count(*) from usuarios where nombre_usuario = '" + txt_user.Text + "' and contrasena ='" + txt_pass.Password + "'", Conexion.conect);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                Conexion.conect.Close();

                if (count > 0)
                {
                    DatosUsuario(usr);
                    MainWindow Mw = new MainWindow();
                    Mw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El nombre de usuario o la contraseña no son correctos", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
    }
}

