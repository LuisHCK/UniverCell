using System;
using System.Windows;
using System.Data.SQLite;
using System.Data;

namespace UniverCell
{
    public partial class MainWindow
    {
        private static double Existencias = 0;

        private void vnt_txt_box_art_TextChanged(object sender, RoutedEventArgs e)
        {
            limpiar_from();

            if (vnt_txt_box_art.Text != "")
            {
                try
                {
                    string id = vnt_txt_box_art.Text;
                    if (Conexion.conect.State == ConnectionState.Open)
                    {
                        Conexion.conect.Close();
                    }
                    Conexion.conect.Open();
                    string comando = "SELECT articulos.id, articulos.nombre, articulos.descripcion, articulos.precio_venta, inventario.existencias, articulos.proveedor_id FROM inventario INNER JOIN articulos where(inventario.articulo_id = " + id + " and articulos.id = " + id + "); ";
                    SQLiteCommand cmd = new SQLiteCommand(comando, Conexion.conect);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        vnt_txt_bx_nombre.Text = reader["nombre"].ToString();

                        if(Convert.ToDouble(reader["existencias"].ToString()) > 0)
                        {
                            vnt_txt_bx_disp.Text = Convert.ToString(Convert.ToDouble(reader["existencias"].ToString()) - 1);
                        }
                        else
                        {
                            vnt_txt_bx_disp.Text = reader["existencias"].ToString();
                        }

                        vnt_txt_descr.Text = reader["descripcion"].ToString();
                        vnt_txt_prec_unit.Text = reader["precio_venta"].ToString();
                        vnt_txt_bx_cantidad.Value = 1;

                        Existencias = Convert.ToDouble(reader["existencias"].ToString());
                    }
                    Conexion.conect.Close();

                    // Si no hay existencias deshabilitar el boton de vender
                    if (Convert.ToInt32(vnt_txt_bx_disp.Text) <= 0)
                    {
                        vnt_btn_vender.IsEnabled = false;
                    }
                    else
                    {
                        vnt_btn_vender.IsEnabled = true;
                    }
                }
                catch (SQLiteException ex)
                {
                    vnt_txt_box_art.Text = null;
                    MessageBox.Show("El código no es válido." + ex );
                }
                catch (FormatException)
                {
                    vnt_btn_vender.IsEnabled = false;
                }
            }
        } // fin de vnt_txt_box_art_TextChanged

        //validar la cantidad de articulos a vender
        private void vnt_txt_bx_Increment(object sender, RoutedEventArgs e)
        {
            try
            {
                int disp = Convert.ToInt32(vnt_txt_bx_disp.Text);

                if (vnt_txt_bx_disp.Text != "" && disp > 0)
                { //Si ya se seleccionó un articulo y hay mas de 0 se puede sumar
                    vnt_txt_bx_disp.Text = Convert.ToString(Convert.ToInt32(vnt_txt_bx_disp.Text) - 1);
                    //al ser correcta la funcion se calcula el sub total
                }

                else
                    vnt_txt_bx_cantidad.Value -= 1;
            }
            catch (FormatException)
            {
                MessageBox.Show("Primero tienes que seleccionar un articulo");
                vnt_txt_bx_cantidad.Value = null;
            }
        }
        //valiar los articulos a vender
        private void vnt_txt_bx_Decrement(object sender, RoutedEventArgs e)
        {
            //devolver articulos seleccionados
            if (vnt_txt_bx_disp.Text != "")
            {
                vnt_txt_bx_disp.Text = Convert.ToString(Convert.ToInt32(vnt_txt_bx_disp.Text) + 1);
            }
        }


        //Calcular el sub_total de la venta
        static decimal calcular_sub_total(decimal precio_unitario, int cantidad, decimal descuento)
        {
            //Calcular el sub total de la venta
            decimal sub_total = (precio_unitario * cantidad);
            //Calcular la cantidad del descuento
            descuento = (descuento / 100) * sub_total;
            //Actualizar el total menos el descuento y devolverlo
            return sub_total = (sub_total - descuento);
        }

        //Si se aumenta la cantidad de articulos por vender se realiza el calculo
        private void vnt_txt_bx_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                int disp = Convert.ToInt32(vnt_txt_bx_disp.Text);

                if (vnt_txt_bx_disp.Text != "" && disp >= 0)
                    vnt_txt_subtotal.Text = Convert.ToString(calcular_sub_total(
                        Convert.ToDecimal(vnt_txt_prec_unit.Text),
                        Convert.ToInt32(vnt_txt_bx_cantidad.Value),
                        Convert.ToDecimal(vnt_descuento.Value)
                        ));
                Total();
            }
            catch
            {

            }

          }

        //Aplicar descuento
        private void vnt_bx_descuento_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                int disp = Convert.ToInt32(vnt_txt_bx_disp.Text);

                if (vnt_txt_bx_disp.Text != "" && disp >= 0)
                {
                    decimal sub_total = calcular_sub_total(
                        Convert.ToDecimal(vnt_txt_prec_unit.Text),
                        Convert.ToInt32(vnt_txt_bx_cantidad.Value),
                        Convert.ToDecimal(vnt_descuento.Value)
                        );

                    vnt_txt_subtotal.Text = Convert.ToString(sub_total);
                    Total();
                }
            }
            catch
            {

            }
        }

        //Calcular el total de la venta

        void Total()
        {
            decimal sub_total = Convert.ToDecimal(vnt_txt_subtotal.Text);
            decimal iva = Convert.ToDecimal(vnt_txt_iva.Text);
            //Primero calcular el iva total:
            iva = sub_total * (iva / 100);
            //Sumar el total del iva al sub total de la venta
            decimal total = Math.Round( (iva + sub_total), 2 );
            //Devolver el total en el formulario
            vnt_TOTAL.Text = Convert.ToString(total);
        }

        //Calcular el vuelto
        private void vnt_paga_con_TextChanged(object sender, RoutedEventArgs e)
        {
           if (vnt_txt_bx_paga_con.Text != "") {
                try
                {
                    decimal total_a_pagar   = Convert.ToDecimal(vnt_TOTAL.Text);
                    decimal paga_con        = Convert.ToDecimal(vnt_txt_bx_paga_con.Text);
                    decimal vuelto          = Math.Round((paga_con - total_a_pagar), 2);
                    vnt_txt_vuelto.Text     = Convert.ToString(vuelto);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error", "El valor introducido no es correcto. Por favor usar solo numeros.");
                }
            }
           else
            {
                vnt_txt_vuelto.Text = null;
            }
        }

        ///Limpiar el formulario.
        public void limpiar_from()
        {
            vnt_txt_bx_disp.Text        = null;
            vnt_txt_bx_cantidad.Value   = null;
            vnt_txt_bx_nombre.Text      = null;
            vnt_txt_descr.Text          = null;
            vnt_txt_prec_unit.Text      = null;
            vnt_txt_prov.Text           = null;
            vnt_txt_subtotal.Text       = null;
            vnt_txt_bx_cantidad.Value   = null;
            vnt_descuento.Value         = null;
            vnt_txt_bx_paga_con.Text    = null;
            vnt_txt_vuelto.Text         = null;
            vnt_TOTAL.Text              = null;
        }


        //Cancelar la venta y limpiar el form
        private void vnt_btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            vnt_txt_box_art.Text = null;
            vnt_btn_vender.IsEnabled = true;
        }

        /// <summary>
        /// Realizar la Venta de un artículo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vnt_btn_vender_Clic(object sender, RoutedEventArgs e)
        {
            try
            {
                int Articulo_Id = Convert.ToInt32(vnt_txt_box_art.Text);
                int Cantidad_Producto = Convert.ToInt32(vnt_txt_bx_cantidad.Value);
                decimal Total_Venta = Convert.ToDecimal(vnt_TOTAL.Text);

                if (Existencias >= vnt_txt_bx_cantidad.Value)
                {
                    string ComandoInsert = "INSERT INTO ventas (codigo_articulo, cantidad, total, usuario_id) VALUES(@cod_art, @cantd, @totl, @usr_id);";
                    string ComandoUpdate = "UPDATE inventario set existencias = inventario.existencias - @cantd WHERE inventario.articulo_id = @cod_art;";

                    Conexion.conect.Open();
                    //Guarda el registro de la venta
                    SQLiteCommand cmd1 = new SQLiteCommand(ComandoInsert, Conexion.conect);
                    cmd1.Parameters.Add(new SQLiteParameter("@cod_art", Articulo_Id));
                    cmd1.Parameters.Add(new SQLiteParameter("@cantd", Cantidad_Producto));
                    cmd1.Parameters.Add(new SQLiteParameter("@totl", Total_Venta));
                    cmd1.Parameters.Add(new SQLiteParameter("@usr_id", Sesion.id_usuario));
                    cmd1.ExecuteNonQuery();

                    //Extrae del inventario la cantidad de producto
                    SQLiteCommand cmd2 = new SQLiteCommand(ComandoUpdate, Conexion.conect);
                    cmd2.Parameters.Add(new SQLiteParameter("@cantd", Cantidad_Producto));
                    cmd2.Parameters.Add(new SQLiteParameter("@cod_art", Articulo_Id));
                    cmd2.ExecuteNonQuery();

                    string concepto = vnt_txt_bx_nombre.Text;
                    string descripcion = Convert.ToString(vnt_txt_bx_cantidad.Value + " " + vnt_txt_bx_nombre.Text);
                    SQLiteCommand cmd3 = new SQLiteCommand("INSERT INTO caja_registro(concepto,descripcion,valor,usuario_id,tipo) VALUES('" + concepto + "','" + descripcion + "','" + vnt_TOTAL.Text + "', '" + Sesion.id_usuario + "','VENTA')", Conexion.conect);
                    cmd3.ExecuteNonQuery();

                    Conexion.conect.Close();

                    AgregarACaja(Convert.ToDouble(vnt_TOTAL.Text));

                    ActualizarTablaVentas();
                    Actualizar_Tabla_Inventario();

                    limpiar_from();

                    MessageBox.Show("Se vendió el artículo. Imprimiendo Recibo...", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No hay articulos en el inventario", "Error", MessageBoxButton.OK,  MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al realizar la venta " + ex);
            }

            ActualizarTablaVentas();
        }

        private void Rec_btn_Actualizar_Clic(object sender, RoutedEventArgs e)
        {
            ActualizarTablaRecargas();
        }

        void ActualizarTablaVentas()
        {
            try
            {
                ProcedimientosAlmacenados pa = new ProcedimientosAlmacenados();
                DataTable a = pa.ActualizarTablaVentas();
                dataGrid_ventas.ItemsSource = a.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la operación: " + ex);
            }
        }
    }
}
