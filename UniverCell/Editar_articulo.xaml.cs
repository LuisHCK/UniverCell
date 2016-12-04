using System;
using System.Windows;
using MySql.Data.MySqlClient;
namespace UniverCell
{
    /// <summary>
    /// Lógica de interacción para Editar_articulo.xaml
    /// </summary>
    public partial class Editar_articulo : Window
    {

        Conexion CON = new Conexion();

        //Crear una variable local para el articulo
        public string art_id;

        public Editar_articulo(string articulo_id)
        {
            InitializeComponent();
            CON.conect.Open();
            //pasar a la variable global el id del articulo
            art_id = articulo_id;

            //Puentear el id del articulo para ser accedido desde fuera de la instancia
            if (articulo_id != null)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("Select * From articulos where articulos.id = " + articulo_id + ";",CON.conect);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_Nombre.Text = (reader.GetString("nombre"));
                        textBox2_Descripcion.Text = (reader.GetString("descripcion"));
                        textBox_precio_compra.Text = (reader.GetString("precio_compra"));
                        textBox_precio_venta.Text = (reader.GetString("precio_venta"));
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Ocurrió un error al buscar el articulo seleccionado" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex);
                }
            }
            else
            {
                MessageBox.Show("El id es nulo");
            }
            CON.conect.Close();
        }

        //Se crea una clase publica para poder acceder al id del articulo pasado al iniciar la ventana
        private void bt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CON.conect.Open();

                int Id = Convert.ToInt16(art_id);
                Console.Write("El id del articulo es: " + Id);
                int Cantidad = Convert.ToInt16(art_txt_box_prec_vent.Value);

                MySqlCommand cmd = new MySqlCommand("call cellmax.agregar_al_inventario(" + Id + ", " + Cantidad + ");", CON.conect);
                cmd.ExecuteNonQuery();

                CON.conect.Close();

                //Si la accion se realiza con exito se muestra un mensaje y luego se cierra la ventana
                if(MessageBox.Show("Se agregó correctamente el artículo al inventario", "Exito", MessageBoxButton.OK, MessageBoxImage.Asterisk) == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex);
            }
        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
