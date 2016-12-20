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

        public static List<string> ListaMonedas = new List<string>();
    }
}
