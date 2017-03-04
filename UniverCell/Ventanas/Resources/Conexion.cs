using System.Data.SQLite;
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
        //private static string cript = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "config.ucll");
        //private static string decripted = Crypto.StringCipher.Decrypt(cript, "UniverCell");
        /// <summary>
        /// 
        /// </summary>
        public static string CadenaConexion = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "UniverCell.db;Version=3;";
        /// <summary>
        /// 
        /// </summary>
        public static SQLiteConnection conect = new SQLiteConnection(CadenaConexion);        
    } 

}
