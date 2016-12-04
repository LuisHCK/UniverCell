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
        public Login()
        {
            InitializeComponent();
        }

        public void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Conectar con la base de datos para consultar el inventaio
                Conexion CON = new Conexion();
                CON.conect.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = CON.conect;

                //cmd.CommandText = "select cellmax.login('" + txt_user.Text + "', '" + txt_pass.Password + "')";
                //loguear autimaticamente (temporal)
                cmd.CommandText = "select cellmax.login('luishck', '123')";

                int login = int.Parse(cmd.ExecuteScalar().ToString());
                CON.conect.Close();

                //Si el login es correcto mandar a la ventana principal
                if (login == 1)
                {
                    this.Hide();
                    MainWindow Form = new MainWindow();
                    Form.lbl_usuario.Content = this.txt_user.Text;
                    Form.Show();
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

