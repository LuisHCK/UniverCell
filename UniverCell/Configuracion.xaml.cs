using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public Configuracion()
        {
            InitializeComponent();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = true;
            textBox1.IsEnabled = true;
            textBox3.IsEnabled = true;
            textBox4.IsEnabled = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = false;
            textBox1.IsEnabled = false;
            textBox3.IsEnabled = false;
            textBox4.IsEnabled = false;
        }

        private void listo_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cadena = "server=" + textBox3.Text + ";user id=" + textBox1.Text + "; pwd=" + passwordBox.Password.ToString() + "; port=" + textBox2.Text + ";database=" + textBox4.Text + ";";

                string encryptedstring = Crypto.StringCipher.Encrypt(cadena, "UniverCell");

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config.ucll", encryptedstring);

                if (MessageBox.Show("Se ha guardado la configuración", "Exto", MessageBoxButton.OK,MessageBoxImage.Information)== MessageBoxResult.OK)
                {
                    Login login = new Login();
                    this.Close();
                    login.Show();
                }

            }
            catch
            {
                MessageBox.Show("Ocurrió un error al guardar la configuración, verifica que los datos ingresados son válidos.");
            }
        }
    }
}
