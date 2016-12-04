using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniverCell
{
    public class Conexion
    {
        public MySqlConnection conect = new MySqlConnection("server=localhost;user id=root; pwd=ljco1800; port=3307;database=cellmax;");
    }
}
