using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UniverCell
{
    public partial class MainWindow
    {
        private void actualizarTablaArticulosFactura()
        {
            string query = "SELECT inventario.id, nombre, precio_venta FROM articulos INNER JOIN inventario ON articulos.id = inventario.articulo_id; ";
            System.Data.DataTable dt = new System.Data.DataTable();

            using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                da.Fill(dt);
            DataGrid_factura_Productos.ItemsSource = dt.DefaultView;
            Conexion.conect.Close();
        }

        private void btn_actualizar_prod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                actualizarTablaArticulosFactura();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error", ex.Message);
            }
        }

        private void BuscarProductoFactura_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string query = @"SELECT inventario.id, nombre, precio_venta FROM articulos 
                                 INNER JOIN inventario ON articulos.id = inventario.articulo_id
                                 WHERE articulos.nombre LIKE '%"+ BuscarProductoFactura.Text + "%';";
                DataTable dt = new DataTable();

                try
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                        da.Fill(dt);
                    DataGrid_factura_Productos.ItemsSource = dt.DefaultView;
                    Conexion.conect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    actualizarTablaArticulosFactura();
                }
            }

        }

        private class Articulo
        {
            public int id { get; set; }
            public string concepto { get; set; }
            public double precio { get; set; }
            public double cantidad { get; set; }
            public double subtotal { get; set; }
        }

        private void Agregar_Producto_Factura_Click(object sender, RoutedEventArgs e)
        {
            Articulo articulo = new Articulo();

            DataRowView drv = (DataRowView)DataGrid_factura_Productos.SelectedItem;
            articulo.id = Convert.ToInt32(drv["id"]);
            articulo.concepto = drv["nombre"].ToString();
            articulo.precio = Convert.ToDouble(drv["precio_venta"]);
            articulo.cantidad = Convert.ToDouble(CantidadArticulo.Value);
            articulo.subtotal = Convert.ToDouble(SubtotalArticulo.Text);

            dataGridFacturaItems.Items.Add(articulo);
        }

        private void CantidadArticulo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            try
            {
                DataRowView drv = (DataRowView)DataGrid_factura_Productos.SelectedItem;
                double precio = Convert.ToDouble(drv["precio_venta"]);
                SubtotalArticulo.Text = (precio * CantidadArticulo.Value).ToString();
            }
            catch
            {
                MessageBox.Show("Error", "La selección no es válida", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnAgregarServicioFactura_Click(object sender, RoutedEventArgs e)
        {
            Articulo servicio = new Articulo();
            servicio.concepto = FacturaServicioConcepto.Text;
            servicio.precio = Convert.ToDouble(PrecioTotalServicio.Value);
            servicio.cantidad = Convert.ToDouble(CantidadTotalServicio.Value);
            servicio.subtotal = Convert.ToDouble(PrecioTotalServicio.Value * CantidadTotalServicio.Value);

            dataGridFacturaItems.Items.Add(servicio);
        }

        private void DataGrid_factura_Productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubtotalArticulo.Clear();
        }
    }
}
