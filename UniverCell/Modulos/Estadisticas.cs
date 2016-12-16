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
        /// <summary>
        /// Cargar los datos del usuario en la pantalla
        /// </summary>
        public void MostrarDatosUsuario()
        {
            text_nombre_comp.Text = Sesion.nomb_completo;
            text_usr_nombre.Text = Sesion.nomb_usuario;
            text_usr_puesto.Text = Sesion.puest;
            text_usr_cedula.Text = Sesion.ced;
            text_usr_dir.Text = Sesion.dir;
        }
    }
}
