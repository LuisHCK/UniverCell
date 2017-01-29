using System;
using System.Windows;
using System.Data.SQLite;
using System.Data;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Collections.Generic;

namespace UniverCell
{
    public partial class MainWindow
    {
        private void art_bnt_crear_editar_Click(object sender, RoutedEventArgs e)
        {
            //Actualizar_tabla_Articulos();
            try
            {
                string nombre_articulo = art_txt_box_nomb.Text;
                string descripcion_articulo = art_txt_box_descr.Text;
                decimal precio_compra = Convert.ToDecimal(art_txt_box_prec_compra.Value);
                decimal precio_venta = Convert.ToDecimal(art_txt_box_prec_vent.Value);
                int exist = Convert.ToInt32(art_num_existencias.Value);
                int id_invent = 0;
                if (art_txt_inv_id.Text != "") { id_invent = Convert.ToInt32(art_txt_inv_id.Text); }
                int prov_id = IdProv(combo_nombre_proveedor.SelectedItem.ToString());

                string comando1 = null;
                string comando2 = null;
                if (art_bnt_crear_editar.Content.ToString() == "Editar")
                {
                    comando1 = "UPDATE inventario SET existencias=" + exist + " WHERE id=" + id_invent + ";";
                    comando2 = "UPDATE `cellmax`.`articulos` SET `nombre`='" + nombre_articulo + "', `descripcion`='" + descripcion_articulo + "', proveedor_id = '" + prov_id + "', `precio_compra`='" + precio_compra + "', `precio_venta`='" + precio_venta + "', `moneda_id`='" + Tienda.id_moneda + "' WHERE `id`='" + art_txt_prd_id.Text + "';";
                }
                else
                {
                    ProcedimientosAlmacenados pa = new ProcedimientosAlmacenados();
                    pa.CrearArt(nombre_articulo,descripcion_articulo,precio_compra,precio_venta,'1',prov_id);
                }
                if(Conexion.conect.State == ConnectionState.Open) { Conexion.conect.Close(); }
                Conexion.conect.Open();
                SQLiteCommand cmd = new SQLiteCommand(comando1+comando2, Conexion.conect);
                cmd.ExecuteNonQuery();

                Conexion.conect.Close();

                Actualizar_Tabla_Inventario();
                Limpiar_Formulario();
                MessageBox.Show("El artículo fue guardado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar el artículo. Por favor revisa los datos. Detalles del error: " + ex);
                Limpiar_Formulario();
            }
        }

        void Limpiar_Formulario()
        {
            art_txt_box_nomb.Text = null;
            combo_nombre_proveedor.SelectedValue = null;
            art_txt_box_descr.Text = null;
            art_txt_box_prec_compra.Value = null;
            art_txt_box_prec_vent.Value = null;
            art_bnt_crear_editar.Content = "Crear";
        }

        /// <summary>
        /// 
        /// </summary>
        public void Actualizar_Tabla_Inventario()
        {
            try
            {
                Conexion.conect.Open();
                DataTable dt = new DataTable();
                string query = "SELECT inventario.id, articulos.nombre, inventario.existencias, articulos.precio_compra, articulos.precio_venta, articulos.id as art_id, proveedores.nombre as prov_nombre FROM articulos INNER JOIN inventario ON articulos.id = inventario.articulo_id INNER JOIN proveedores ON articulos.proveedor_id = proveedores.id; ";

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                    da.Fill(dt);
                dataGrid_Inventario.ItemsSource = dt.DefaultView;
                Conexion.conect.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Ocurrió un error en la operación: " + ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //public void Actualizar_tabla_Articulos()
        //{
        //    try
        //    {
        //        string comando = "SELECT id, nombre, descripcion FROM articulos;";

        //        Conexion.conect.Open();
        //        DataTable dt = new DataTable();
        //        using (Conexion.conect)
        //        {
        //            SQLiteDataAdapter da = new SQLiteDataAdapter(comando, Conexion.conect);
        //            da.Fill(dt);
        //            Console.WriteLine("Operacion realizada");
        //        }
        //        Conexion.conect.Close();
        //        dataGrid_articulos.ItemsSource = dt.DefaultView;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(Convert.ToString(ex));
        //    }
        //}

        //Boton para editar un articulo seleccionado
        private void button_inv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid_Inventario.SelectedItem;
                string result = (drv.Row[5]).ToString();
                EditarArticulo(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show("El artículo seleccionado no es válido, o no se ha seleccionado ninguno" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                dataGrid_Inventario.SelectedItem = null;
            }
        }

        //Boton para eliminar articulos de la lista
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid_Inventario.SelectedItem;
                string ArtId = (drv.Row[5]).ToString();
                string InvId = (drv.Row[0]).ToString();
                string Nombre = (drv["nombre"]).ToString();

                string comando1 = "DELETE FROM inventario WHERE id=" + InvId + "";
                string comando2 = "DELETE FROM `cellmax`.`articulos` WHERE `id`='" + ArtId + "';";

                if (MessageBox.Show("Se dispone a eliminar el articulo: " + ArtId + ": " + Nombre, "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Conexion.conect.Open();
                    SQLiteCommand cmd = new SQLiteCommand(comando1+comando2, Conexion.conect);
                    cmd.ExecuteNonQuery();
                    Conexion.conect.Close();

                    MessageBox.Show("Se eliminó correctamente el articulo");
                    Actualizar_Tabla_Inventario();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("La selección no es valida, verifica el articulo seleccionado e intentalo de nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void inv_btn_vender_art_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)dataGrid_Inventario.SelectedItem;
                string result = (drv.Row[5]).ToString();
                //MessageBox.Show(result);
                vnt_txt_box_art.Text = result;
                tabControl.SelectedIndex = 1;
            }
            catch
            {
                MessageBox.Show("La selección no es válida");
            }
        }

        //Buscador de articulos en el inventario
        private void textBox_Buscar_Articulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Conexion.conect.Open();
                DataTable dt = new DataTable();
                string query = "call buscar_('" + textBox_Buscar_Articulo.Text + "');";

                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                    da.Fill(dt);
                Console.WriteLine("Operacion realizada");
                dataGrid_Inventario.ItemsSource = dt.DefaultView;
                Conexion.conect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontró ningún resultado" + ex, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        ///Editar articul
        void EditarArticulo(string articulo_id)
        {
            if (articulo_id != null)
            {
                try
                {
                    if (Conexion.conect.State == ConnectionState.Open) { Conexion.conect.Close(); }
                    Conexion.conect.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT inventario.id, articulos.nombre, articulos.descripcion, inventario.existencias, articulos.precio_compra, articulos.precio_venta, articulos.id as art_id FROM articulos INNER JOIN inventario ON articulos.id = inventario.articulo_id WHERE articulos.id = "+articulo_id+"; ", Conexion.conect);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        art_txt_inv_id.Text = (reader["id"].ToString());
                        art_txt_box_nomb.Text = (reader["nombre"]).ToString();
                        art_txt_box_descr.Text = (reader["descripcion"].ToString());
                        art_txt_box_prec_compra.Value = Convert.ToDouble(reader["precio_compra"]);
                        art_txt_box_prec_vent.Value = Convert.ToDouble(reader["precio_venta"]);
                        art_txt_prd_id.Text = (reader["art_id"].ToString());
                        art_num_existencias.Value = Convert.ToDouble(reader[("existencias")]);
                    }

                    Conexion.conect.Close();
                    art_bnt_crear_editar.Content = "Editar";
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Ocurrió un error al buscar el articulo seleccionado" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex);
                }

            }
        }

        /// <summary>
        /// Obtiene el id del proveedor basado en el nombre
        /// </summary>
        /// <param name="nombre"></param>
        public int IdProv(string nombre)
        {
            int ID = 0;
            try
            {
                Conexion.conect.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT id FROM proveedores where nombre = '" + nombre + "';", Conexion.conect);
                SQLiteDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    ID = Convert.ToInt32(Reader["id"]);
                }
                Conexion.conect.Close();
            }
            catch
            {
                MessageBox.Show("No se pudo encontrar ningun proveedor con el Nombre: " + nombre, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
            return ID;
        }
    }
}
