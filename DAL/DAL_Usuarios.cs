using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Entity;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;




namespace DAL
{
    public class DAL_Usuarios
    {
        FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
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


        public static async Task AgregarUsuario(string P_Nombre, string P_Apaterno, string P_Amasterno, string P_Correo, string P_Contraseña)
        {
            await firebase
              .Child("Usuarios")
              .PostAsync(new DtoUsuario() 
              {     IdUsuario = Guid.NewGuid(), 
                    NomUsuario = P_Nombre,
                    APaterno = P_Apaterno,
                    AMaterno = P_Amasterno,
                    Correo = P_Correo,
                    Contraseña = P_Contraseña

              });
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(cadena))
            //    {
            //        conn.ExecuteReader(sentencia, Parametros, commandType: CommandType.StoredProcedure);
            //    }
            //}
            //catch (SqlException e)
            //{
            //    throw e;
            //}

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
