using System;
using System.IO;
using System.Windows;

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
        ///  
        public const string RutaDB = "";

        public Configuracion()
        {
            InitializeComponent();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = true;
            textBox1.IsEnabled = true;
            textBox4.IsEnabled = true;
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = false;
            textBox1.IsEnabled = false;
            textBox4.IsEnabled = false;
        }

        private void listo_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cadena = "Data Source="+ textBox4.Text + ";Version=3;Password="+ textBox1.Text + ";";

                string encryptedstring = Crypto.StringCipher.Encrypt(cadena, "UniverCell");

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config.ucll", cadena);

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

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            textBlock_pass.Text = passwordBox.Password;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            textBlock_pass.Text = null;
        }

        private void btnCargarBD_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".db";
            dlg.Filter = "Archivo de Base de datos (*.sqlite)|*.db";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                textBox4.Text = filename;   
            }
        }
    }
}
