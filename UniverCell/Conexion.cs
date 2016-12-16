using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverCell
{
    /// <summary>
    /// 
    /// </summary>
    public static class Conexion
    {
        private static string cript = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "config.ucll");
        private static string decripted = Crypto.StringCipher.Decrypt(cript, "UniverCell");
        /// <summary>
        /// 
        /// </summary>
        public static string CadenaConexion = decripted;
        /// <summary>
        /// 
        /// </summary>
        public static MySqlConnection conect = new MySqlConnection(CadenaConexion);
    } 

}
