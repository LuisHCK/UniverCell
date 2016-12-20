using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Controls;

namespace UniverCell
{
    public partial class MainWindow
    {

        private void vnt_txt_box_art_TextChanged(object sender, RoutedEventArgs e)
        {
            limpiar_from();

            if (vnt_txt_box_art.Text != "")
            {
                try
                {
                    Conexion.conect.Open();
                    MySqlCommand cmd = new MySqlCommand("call cellmax.buscar_articulo('" + vnt_txt_box_art.Text + "');", Conexion.conect);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        vnt_txt_bx_nombre.Text = reader.GetString("nombre");
                        vnt_txt_bx_disp.Text = reader.GetString("existencias");
                        vnt_txt_descr.Text = reader.GetString("descripcion");
                        vnt_txt_prec_unit.Text = reader.GetString("precio_venta");
                        //vnt_txt_prov.Text = reader.GetString("proveedor_id
                    }
                    Conexion.conect.Close();
                }
                catch (MySqlException ex)
                {
                    vnt_txt_box_art.Text = null;
                    MessageBox.Show("El código no es válido." + ex );
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
        }


        //Cancelar la venta y limpiar el form
        private void vnt_btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            vnt_txt_box_art.Text = null;
        }

        private void vnt_btn_vender_Clic(object sender, RoutedEventArgs e)
        {
            try
            {
                int Producto_Id = Convert.ToInt32(vnt_txt_box_art.Text);
                int Cantidad_Producto = Convert.ToInt32(vnt_txt_bx_cantidad.Value);
                int Moneda_id = Moneda.IDMoneda(combo_bx_Moneda.SelectedItem.ToString());
                decimal Total_Venta = Convert.ToDecimal(vnt_TOTAL.Text);

                Conexion.conect.Open();

                MySqlCommand cmd = new MySqlCommand("call cellmax.vender_producto('" + Producto_Id + "', '" + Cantidad_Producto + "', '" + Moneda_id + "', '" + Total_Venta + "');", Conexion.conect);
               int i = cmd.ExecuteNonQuery();
                Console.Write("Insertado '"+i+"' fila");

                Conexion.conect.Close();
                ActualizarTablaVentas();
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
            try {
                Conexion.conect.Open();
                DataTable dt = new DataTable();
                    string query = "call cellmax.ver_registro_ventas();";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, Conexion.conect))
                        da.Fill(dt);
                    Console.WriteLine("Operacion realizada");
                dataGrid_ventas.ItemsSource = dt.DefaultView;
                Conexion.conect.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocurrió un error en la operación: " + ex);
            }

        }
    }
}
