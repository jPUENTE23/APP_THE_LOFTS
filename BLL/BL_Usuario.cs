using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Entity;
using System.Data;
using System.Threading.Tasks;
using Firebase.Database.Query;
using Firebase.Database;
using System.Linq;


namespace BLL
{
    public class BL_Usuario
    {
        
        public static List<DtoUsuario> Usuarios(string cadena)
        {

            List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
            DataTable dtUsuario = new DataTable();

            dtUsuario = DAL_Usuarios.ConsultarUsuarios(cadena, "SP_USUARIOS");

            if (dtUsuario.Rows.Count > 0)
            {
                foreach (DataRow row in dtUsuario.Rows)
                {
                    DtoUsuario Usuario = new DtoUsuario();
                    Usuario.IdUsuario = (string)row["IdUsuario"];
                    Usuario.NomUsuario = (string)row["Nombre"];
                    Usuario.APaterno = (string)row["APaterno"];
                    Usuario.AMaterno = (string)row["AMaterno"];
                    Usuario.Correo = (string)row["Correo"];
                    Usuario.Contraseña = (string)row["Contraseña"];
                    lstUsuario.Add(Usuario);
                }
            }

            return lstUsuario;
        }

        public static async Task<List<string>> AltaUsuario(DtoUsuario usuario)
        {
            List<string> responde = new List<string>();

            try
            {
                await DAL_Auth.Aunteticacion(usuario.Correo, usuario.Contraseña);
                await DAL_Usuarios.AgregarUsuario(usuario.NomUsuario, usuario.Apellidos, usuario.Correo,usuario.Contraseña);
                responde.Add("00");
                responde.Add("El usuario se agrego correctamente");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                responde.Add("14");
                responde.Add(e.Message);
            }


            return responde;
        }


        public static async Task<List<DtoUsuario>> datosUsuario(string usuario, string contraseña)
        {
            List<DtoUsuario> lstUsuario = new List<DtoUsuario>();
            try
            {

                DtoUsuario UserLogin = new DtoUsuario()
                {
                    Correo = usuario,
                    Contraseña = contraseña
                };

                var Usuario = await DAL_Usuarios.DetalleUsuario(UserLogin);
                lstUsuario.Add(Usuario);

                //foreach (DtoUsuario user in usuarios)
                //{
                //    if (user.Correo == usuario && user.Contraseña == contraseña)
                //    {
                //        Usuario.Add(user);
                //        break;
                //    }
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lstUsuario;

            
        }
    }
}
