using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Entity;




namespace DAL
{
    public class DAL_Usuarios
    {
        public static DataTable ConsultarUsuarios(string cadena, string sentencia)
        {
            DataTable dUsuarios = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    var lstUsuario = conn.ExecuteReader(sentencia, commandType: CommandType.StoredProcedure);
                    dUsuarios.Load(lstUsuario);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return dUsuarios;
        }


        public static void AgregarUsuario(string cadena, string sentencia, object Parametros)
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

        public static SqlDataReader Login(string cadena, string sentencia, DtoUsuario Usuario)
        {
            SqlDataReader dtUsuario;
            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = sentencia;
                        cmd.Parameters.AddWithValue("@P_Usuario", Usuario.Correo);
                        cmd.Parameters.AddWithValue("@P_Contraseña", Usuario.Contraseña);

                        dtUsuario = cmd.ExecuteReader();

                        cmd.Parameters.Clear();
                    }

                    //var usuario = conn.ExecuteReader(sentencia, Parametros, commandType: CommandType.StoredProcedure);
                    //dtUsuario.Load(usuario);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return dtUsuario;
        }
    }
}
