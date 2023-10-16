using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DAL
{
    public class DAL_Reservacion
    {
        public static void AgregarRersvacion(string cadena, string senetencia, object Parametros)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.ExecuteReader(senetencia, Parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
