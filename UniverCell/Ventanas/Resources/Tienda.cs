using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UniverCell
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboProveedores
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Clase publica que almacena datos de la tienda
    /// </summary>
    public static class Tienda
    {
        /// <summary>
        /// 
        /// </summary>
        public static string nombre_moneda;
        /// <summary>
        /// 
        /// </summary>
        public static string signo_moneda;
        /// <summary>
        /// 
        /// </summary>
        public static int id_moneda;
        /// <summary>
        /// 
        /// </summary>
        public static decimal taza_moneda;

        /// <summary>
        /// Almacena en una lista las monedas con las que se trabaja
        /// </summary>
        public static List<string> ListaMonedas = new List<string>();

        /// <summary>
        /// Almacena en una lista las monedas con las que se trabaja
        /// </summary>
        //public static List<string> ListaProveedores = new List<string>();
        public static List<ComboProveedores> ListData = new List<ComboProveedores>();

    }
}
