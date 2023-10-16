using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DAL
{
    public class DAL_Habitaciones
    {
        public static DataTable Habitaciones(string cadena, string senetcnia)
        {
            DataTable dtHabitaciones = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var Habitaciones = conn.ExecuteReader(senetcnia, commandType: CommandType.StoredProcedure);
                    dtHabitaciones.Load(Habitaciones);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return dtHabitaciones;
        }

        public static DataTable ListarTiposHabitacion(string cadena, string senetencia, object Parametros)
        {
            DataTable dtHabitaciones = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var habitaciones = conn.ExecuteReader(senetencia, Parametros, commandType: CommandType.StoredProcedure);
                    dtHabitaciones.Load(habitaciones);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return dtHabitaciones;
        }

        public static void ActualizarEstatus(string cadena, string sentencia, object Parametros)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.ExecuteReader(sentencia, Parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public static DataTable HabitacioneDisp(string cadena, string senetencia, object Parametros)
        {
            DataTable dtCantHabitaciones = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var habitaciones = conn.ExecuteReader(senetencia, Parametros, commandType: CommandType.StoredProcedure);
                    dtCantHabitaciones.Load(habitaciones);

                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return dtCantHabitaciones;
        }
    }
}
