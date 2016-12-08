using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace UniverCell
{
    public partial class MainWindow
    {
        public void MostrarDatosUsuario()
        {
            text_nombre_comp.Text = Sesion.nomb_completo;
            text_usr_nombre.Text = Sesion.nomb_usuario;
            text_usr_puesto.Text = Sesion.puest;
            text_usr_cedula.Text = Sesion.ced;
            text_usr_dir.Text = Sesion.dir;
            text_usr_perm.Text = Sesion.lvl;
            Crear_Dir_Images();

        }

        private void btn_cambiar_foto_Click(object sender, RoutedEventArgs e)
        {
            Stream checkStream = null;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Multiselect = false;
            //Filtrar los archivos            
            openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +"Portable Network Graphic (*.png)|*.png";

            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        Bitmap local_avatar = new Bitmap(openFileDialog.FileName);
                        local_avatar.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\" + Sesion.nomb_usuario+".jpg");

                        Avatar.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("No se pudo leer el archivo. Detalles del error: " + ex.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Error","Ha ocurrido un error, por favor revisa los datos e intenta nuevamente",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public static void Crear_Dir_Images()
        {
            // Specify the directory you want to manipulate.
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }
    }
}
