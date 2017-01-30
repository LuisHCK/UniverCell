using System;
using System.Data;
using System.Data.SQLite;

namespace UniverCell
{
    /// <summary>
    /// Biblioteca de procedimientos SQL
    /// </summary>
    public class ProcedimientosAlmacenados
    {
        /// <summary>
        /// Optiene todos los articulos en el inventario
        /// </summary>
        /// <returns>SQL String</returns>
        public string VerInventario()
        {
            string inv = "SELECT inventario.id, articulos.nombre, inventario.existencias,articulos.precio_compra, articulos.precio_venta, articulos.id as art_id, proveedores.nombre as prov_nombre FROM articulos INNER JOIN inventario ON articulos.id = inventario.articulo_id INNER JOIN proveedores ON proveedor_id = proveedores.id;";
            return inv;
        }

        /// <summary>
        /// Devuelve un string SQL para insertar un articulo
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="prec_comp"></param>
        /// <param name="prec_vent"></param>
        /// <param name="cat"></param>
        /// <param name="nomb"></param>
        /// <param name="prov"></param>
        /// <returns></returns>
        public void CrearArt(string nomb, string descr, decimal prec_comp, decimal prec_vent, int cat, int prov)
      {
            var art = "insert into articulos(nombre, descripcion, precio_compra, precio_venta, categoria_id, proveedor_id)values('" + nomb + "', '" + descr + "', '" + prec_comp + "', '" + prec_vent + "', '" + cat + "', '" + prov + "');";

            Conexion.conect.Open();
            SQLiteCommand cmd = new SQLiteCommand(art, Conexion.conect);
            cmd.ExecuteNonQuery();
            Conexion.conect.Close();
        }

        /// <summary>
        /// Actualiza la tabla de los registros de ventas
        /// </summary>
        public DataTable ActualizarTablaVentas()
        {
            Conexion.conect.Open();
            DataTable dt = new DataTable();
            string query = "SELECT*FROM ventas;";
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, Conexion.conect))
                da.Fill(dt);
            Conexion.conect.Close();
            return dt;
        }
        /// <summary>
        /// Realiza una venta del articulo indicado
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="cantidad"></param>
        /// <param name="moneda_id"></param>
        /// <param name="total"></param>
        /// <param name="usuario"></param>
        public void RealizarVenta(int codigo, decimal cantidad, int moneda_id, decimal total, int usuario )
        {
            string ComandoInsert = "INSERT INTO ventas (codigo_articulo, cantidad, moneda_id, fecha_venta, total, usuario_id) VALUES("+ codigo +", " + cantidad + ", " + moneda_id + ", '" + DateTime.Now + "', " + total + ", " + usuario + "); ";
            string ComandoUpdate = "UPDATE inventario set existencias = inventario.existencias - " + cantidad + " WHERE inventario.articulo_id = " + codigo + ";";

            Conexion.conect.Open();
            SQLiteCommand cmd1 = new SQLiteCommand(ComandoInsert, Conexion.conect);
            SQLiteCommand cmd2 = new SQLiteCommand(ComandoUpdate, Conexion.conect);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery()
            Conexion.conect.Close();
        }
    }
}
