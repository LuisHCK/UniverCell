using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Inicializar Ventana
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            /*
             *Preparar la ventana principal  
             */
            //Cargar los datos del usuario
            MostrarDatosUsuario();
            //Cargar tablas
            Actualizar_Tabla();
            Actualizar_tabla_Articulos();
            EstadisticasRecargas();
            Actualizar_Tabla_Inventario();
        }

        /*Cargar los datos de la tienda
         * en caso contrario mandar a la configuracion
        */
        private void CargarDatosTienda()
        {

        }

        private void Label_Usuario(object sender, RoutedEventArgs e)
        {
            // ... Get label.
            var label = sender as Label;
            // ... Set date in content.
            label.Content = Sesion.nomb_completo;
        }

        private void Label_Puesto(object sender, RoutedEventArgs e)
        {
            // ... Get label.
            var label = sender as Label;
            // ... Set date in content.
            label.Content = Sesion.puest;
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

        /// <summary>
        /// Actualizar los datos de la tienda
        /// </summary>
        public static void ActualizarDatosTienda()
        {
            MessageBox.Show("Datos actualizados");
        }

        private void tienda_ajustes_Click(object sender, RoutedEventArgs e)
        {
            Ajustes aj = new Ajustes();
            aj.Show();
        }
    }
}
