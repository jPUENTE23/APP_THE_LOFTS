using System;
using System.Collections.Generic;
using System.Text;

namespace THE_LOFTS.Access
{
    public class Conection
    {
        //"data source= DESKTOP-6JRCA2I\\SQLDEV2017; user id= sa; pwd= sql2017; initial catalog= DB_THE_LOFTS"
        //private readonly static string sevidor = "DESKTOP-6JRCA2I\\SQLDEV2017";
        //private readonly static string usuario = "sa";
        //private readonly static string contraseña = "sql2017";
        //private readonly static string database = "DB_THE_LOFTS";

        public static string cadenaString()
        {
            string cadena = "Server=DESKTOP-6JRCA2I\\SQLDEV2017;Database=DB_THE_LOFTS;User=sa;Password=sql2017";

            return cadena;
        }
    }
}
