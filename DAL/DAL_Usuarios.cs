using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Entity;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;




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


        public static async Task<List<DtoUsuario>> Usuarios()
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            List<DtoUsuario> lstUsuarios = new List<DtoUsuario>();

            var usuariosQuery = await firebase
                .Child("Usuarios")
                .OnceAsync<DtoUsuario>();

            foreach (var user in usuariosQuery)
            {
                DtoUsuario Usuario = user.Object;
                lstUsuarios.Add(Usuario);
            }

            return lstUsuarios;
        }


        public static async Task<DtoUsuario> DetalleUsuario(DtoUsuario Usuario)
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            var allPersons = await Usuarios();
            await firebase
              .Child("Usuarios")
              .OnceAsync<DtoUsuario>();
            return allPersons.Where(a => a.Correo == Usuario.Correo && a.Contraseña == Usuario.Contraseña).FirstOrDefault();
        }


        public static async Task AgregarUsuario(string P_Nombre, string P_Apellidos, string P_Correo, string P_Contraseña)
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            await firebase
              .Child("Usuarios")
              .PostAsync(new DtoUsuario() 
              {     IdUsuario = Guid.NewGuid().ToString(), 
                    NomUsuario = P_Nombre,
                    Apellidos = P_Apellidos,
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
