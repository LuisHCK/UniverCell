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
            string query = "SELECT inventario.id, nombre, precio_venta, existencias FROM articulos INNER JOIN inventario ON articulos.id = inventario.articulo_id; ";
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
            try
            {
                Articulo articulo = new Articulo();
                double total = 0;

                if (txt_Total_Factura.Text != "") { total = Convert.ToDouble(txt_Total_Factura.Text); }

                DataRowView drv = (DataRowView)DataGrid_factura_Productos.SelectedItem;
                articulo.id = Convert.ToInt32(drv["id"]);
                articulo.concepto = drv["nombre"].ToString();
                articulo.precio = Convert.ToDouble(drv["precio_venta"]);
                articulo.cantidad = Convert.ToDouble(CantidadArticulo.Value);
                articulo.subtotal = Convert.ToDouble(SubtotalArticulo.Text);

                if (articulo.cantidad < Convert.ToDouble(drv["existencias"]))
                {
                    dataGridFacturaItems.Items.Add(articulo);
                    txt_Total_Factura.Text = Convert.ToString(total += articulo.subtotal);
                }
                else
                {
                    MessageBox.Show("No hay existencias en el inventario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Los valores introducidos no son válidos, revise el formulario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                MessageBox.Show("La selección no es válida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnAgregarServicioFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Articulo servicio = new Articulo();

                double total = 0;
                if (txt_Total_Factura.Text != "") { total = Convert.ToDouble(txt_Total_Factura.Text); }

                servicio.concepto = FacturaServicioConcepto.Text;
                servicio.precio = Convert.ToDouble(PrecioTotalServicio.Value);
                servicio.cantidad = Convert.ToDouble(CantidadTotalServicio.Value);
                servicio.subtotal = Convert.ToDouble(PrecioTotalServicio.Value * CantidadTotalServicio.Value);

                dataGridFacturaItems.Items.Add(servicio);

                txt_Total_Factura.Text = Convert.ToString(total += servicio.subtotal);
            }
            catch
            {
                MessageBox.Show("La selección no es válida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGrid_factura_Productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubtotalArticulo.Clear();
        }

        private void button_RealizarFactura_Click(object sender, RoutedEventArgs e)
        {
            string total = txt_Total_Factura.Text;
            string cliente = Convert.ToString(FacturaClienteNombre.Text);
            string tipo_pago;

            if (FacturaPagoContado.IsChecked == true)
            {
                tipo_pago = "contado";
            }
            else if(FacturaPagoCredito.IsChecked == true)
            {
                tipo_pago = "credito";
            }
            else
            {
                tipo_pago = "contado";
            }
            
            string query = "INSERT INTO facturas(total, cliente, tipo_pago) VALUES(@total, @cliente, @tipo_pago);";
            SQLiteCommand CMD = new SQLiteCommand(query, Conexion.conect);
            CMD.Parameters.AddWithValue("@total", total);
            CMD.Parameters.AddWithValue("@cliente", cliente);
            CMD.Parameters.AddWithValue("@tipo_pago", tipo_pago);

            try
            {
                Conexion.conect.Open();
                CMD.ExecuteNonQuery();
                MessageBox.Show("La factura a nombre de: " + cliente + " se realizó con exito.", "Realizado", MessageBoxButton.OK, MessageBoxImage.Information);
                Conexion.conect.Close();

                facturasRealizadas();
                AgregarACaja(Convert.ToDouble(total));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: "+ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void txt_PagaCon_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                decimal total = Convert.ToDecimal(txt_Total_Factura.Text);
                decimal pagaCon = Convert.ToDecimal(txt_PagaCon.Text);

                textBoxCambio.Text = (Convert.ToString(pagaCon - total));
            }
            catch
            {
                
            }
        }

        private void buttonDeshacerFactura_Click(object sender, RoutedEventArgs e)
        {
            dataGridFacturaItems.ItemsSource = null;
        }

        private void facturasRealizadas()
        {
            try
            {
                string query = "SELECT * FROM facturas;";
                System.Data.DataTable dt = new System.Data.DataTable();

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                    da.Fill(dt);
                dataGridFacturasRealizadas.ItemsSource = dt.DefaultView;
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudieron obtener las facturas", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

    }
}
