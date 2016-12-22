using System;
using System.Windows;
using MySql.Data.MySqlClient;
namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Editar_articulo.xaml
    /// </summary>
    public partial class Proveedores : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public Proveedores()
        {
            InitializeComponent();
            ActualizarTablaProv();
        }

        void ActualizarTablaProv()
        {
            Archivos arc = new Archivos();
            try
            {
                arc.ActualizarTabla(this.dataGrid, "SELECT * From cellmax.proveedores;");
            }
            catch
            {
                MessageBox.Show("No se pudieron obtener información de la base de datos");
            }
        }
    }
}
