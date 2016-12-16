using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniverCell
{
    /// <summary>
    /// Clase utilizada para gestionar el uso y creacion de archivos
    /// </summary>
    public class Archivos
    {
        /// <summary>
        /// Cargar Imagenes desde los archivos de la PC
        /// </summary>
        public static void CargarImagen(string funcion, string nombre, int IdUsuario)
        {
            Stream checkStream = null;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Multiselect = false;
            //Filtrar los archivos            
            openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            if ((bool)openFileDialog.ShowDialog())
            {
                try
                {
                    if ((checkStream = openFileDialog.OpenFile()) != null)
                    {
                        CrearDir("Images");
                        Bitmap local_avatar = new Bitmap(openFileDialog.FileName);
                        var directiorio = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + nombre + ".jpg";
                        if (File.Exists(directiorio))
                        {
                            File.Delete(directiorio);
                        }
                        //Guardar la imagen
                        local_avatar.Save(directiorio);

                        //imagen.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo leer el archivo. Detalles del error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error, por favor revisa los datos e intenta nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Crear directiorios
        /// </summary>
        public static void CrearDir(string dir)
        {
            // Specify the directory you want to manipulate.
            string path = AppDomain.CurrentDomain.BaseDirectory + "" + dir + "\\";

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
