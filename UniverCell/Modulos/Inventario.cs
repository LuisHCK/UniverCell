using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Interop;
using System.Windows.Controls;

namespace UniverCell
{
    public partial class MainWindow
    {
        private void art_bnt_crear_editar_Click(object sender, RoutedEventArgs e)
        {
            Actualizar_Tabla_Inventario();
            Actualizar_tabla_Articulos();

            try
            {
                string nombre_articulo = art_txt_box_nomb.Text;
                string proveedor = art_txt_box_prov.Text;
                string descripcion_articulo = art_txt_box_descr.Text;
                decimal precio_compra = Convert.ToDecimal(art_txt_box_prec_compra.Value);
                decimal precio_venta = Convert.ToDecimal(art_txt_box_prec_vent.Value);
                int moneda = Convert.ToInt16(art_combo_moneda.Text);

                CON.conect.Open();
                string comando = "call cellmax.crear_articulos('" + nombre_articulo + "', '" + descripcion_articulo + "', " + precio_compra + ", " + precio_venta + ", null, "+proveedor+", " + moneda + ");";

                MySqlCommand cmd = new MySqlCommand(comando, CON.conect);
                    cmd.ExecuteNonQuery();
                CON.conect.Close();
                Limpiar_Formulario();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex);
                Limpiar_Formulario();
            }
        }

        void Limpiar_Formulario()
        {
            art_txt_box_nomb.Text   =  null;
            art_txt_box_prov.Text    =  null;
            art_txt_box_descr.Text  =  null;
            art_txt_box_prec_compra.Value   = null;
            art_txt_box_prec_vent.Value     = null;
        }

       public void Actualizar_Tabla_Inventario()
        {
            try
            {
                DataTable dt = new DataTable();
                using (CON.conect)
                {
                    CON.conect.Open();
                    string query = "call cellmax.ver_inventario();";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, CON.conect))
                        da.Fill(dt);
                    CON.conect.Close();
                    Console.WriteLine("Operacion realizada");
                }
                dataGrid_Inventario.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocurrió un error en la operación: " + ex);
            }
        }

       public void Actualizar_tabla_Articulos()
        {
            try
            {
                string comando = "SELECT id, nombre, descripcion FROM cellmax.articulos;";

                CON.conect.Open();
                DataTable dt = new DataTable();
                using (CON.conect)
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(comando, CON.conect);
                    da.Fill(dt);
                    Console.WriteLine("Operacion realizada");
                }
                CON.conect.Close();
                dataGrid_articulos.ItemsSource = dt.DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        //Boton para editar un articulo seleccionado
        private void button_inv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid_articulos.SelectedItem;
                string id = (drv["Id"]).ToString();

                Editar_articulo Editar = new Editar_articulo(id);
                Editar.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("El artículo seleccionado no es válido, o no se ha seleccionado ninguno","Error",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        //Boton para eliminar articulos de la lista
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid_articulos.SelectedItem;
                string Id = (drv["Id"]).ToString();
                string Nombre = (drv["nombre"]).ToString();
                string comando = "DELETE FROM `cellmax`.`articulos` WHERE `id`='"+Id+"';";
                
                if (MessageBox.Show("Se dispone a eliminar el articulo: " + Id + ": " + Nombre, "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CON.conect.Open();
                    MySqlCommand cmd = new MySqlCommand(comando,CON.conect);
                    cmd.ExecuteNonQuery();
                    CON.conect.Close();

                    MessageBox.Show("Se eliminó correctamente el articulo");
                    Actualizar_tabla_Articulos();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("La selección no es valida, verifica el articulo seleccionado e intentalo de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                Console.Write(ex);

            }
        }

        private void inv_btn_vender_art_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)dataGrid_ventas.SelectedItem;
            Console.WriteLine(drv);
            //string Id = (drv["Código Artículo"]).ToString();
            //vnt_txt_box_art.Text = Id;
            this.tabControl.SelectedIndex = 2;
        }

        //Buscador de articulos en el inventario
        private void textBox_Buscar_Articulo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
