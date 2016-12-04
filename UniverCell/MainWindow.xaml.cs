using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /* Solución temporal al error de renderizado causado por el re-login en windows
         * Se cambió el método de renderizado de hardware a software
         * https://github.com/MahApps/MahApps.Metro/issues/2734#issuecomment-260839524
         */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var w = (Window)sender;
            var src = PresentationSource.FromVisual(w) as System.Windows.Interop.HwndSource;
            var target = src.CompositionTarget;
            target.RenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        }

        public Conexion CON = new Conexion();

        //Crear el objeto que almacena los datos del usuario actual.
        public class usuario
        {
            public string id;
            public string nombre;
            public string nombre_completo;
            public string puesto;
            public string cedula;
            public string direccion;
            public string permisos;
        }

        public MainWindow()
        {
            InitializeComponent();
            Actualizar_Tabla();
            Actualizar_Tabla_Inventario();
            Actualizar_tabla_Articulos();
            EstadisticasRecargas();
            /*
            lbl_usuario.Content = "LuisHCK";
            Debug.WriteLine("Pasando variable: "+nombre_usuario);
            */
        }



        //Los tiles dirigen a un tab
        private void tile_ventas_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 1;
        }

        private void tile_recargas_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 2;
        }

        private void tile_reparaciones_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 3;
        }

        private void tile_inventario_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 4;
        }

        private void tile_estadisticas_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 5;
        }

        private void tile_caja_Click(object sender, RoutedEventArgs e)
        {
            this.tabControl.SelectedIndex = 6;
        }

        private void atras_btn_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex > 0)
            {
                tabControl.SelectedIndex -= 1;
            }
            else
                atras_btn.IsEnabled = false;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            atras_btn.IsEnabled = true;
        }
    }
}
