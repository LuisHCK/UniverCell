using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        Conexion CON = new Conexion();

        public Login()
        {
            InitializeComponent();
        }

        /*La funcion obtiene los datos de un usuario basado en el nombre de usuario y lo almacena en 
         * la clase estatica sesión
         */
        public void DatosUsuario(string usuario)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM cellmax.usuarios where nombre_usuario = '"+usuario+"';", CON.conect);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            string usr = txt_user.Text;
            MessageBox.Show(usr);
            try
            {
                //Conectar con la base de datos para consultar el inventaio
                CON.conect.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = CON.conect;

                cmd.CommandText = "select cellmax.login('" + txt_user.Text + "', '" + txt_pass.Password + "')";
                /*loguear autimaticamente (temporal)
                *cmd.CommandText = "select cellmax.login('luishck', '123')";
                */
                int login = int.Parse(cmd.ExecuteScalar().ToString());
                
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

                CON.conect.Close();
            }
            //Si ocurre un error con la conexion a la BD mandar una excepcion
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocurió un error" + ex);
            }
        }
    }
}

