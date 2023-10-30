using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Entity;
using System.Data;
using System.Data.SqlClient;

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
                    Usuario.IdUsuario = (int)row["IdUsuario"];
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

        public static async List<string> AltaUsuario(DtoUsuario usuario)
        {
            List<string> responde = new List<string>();

            try
            {
                await DAL_Auth.Aunteticacion(usuario.Correo, usuario.Contraseña);
                await DAL_Usuarios.AgregarUsuario(usuario.NomUsuario, usuario.APaterno, usuario.AMaterno, usuario.Contraseña);
                responde.Add("00");
                responde.Add("El usuario se agrego correctamente");
            }
            catch (Exception e)
            {
                responde.Add("14");
                responde.Add(e.Message);
            }
            //try
            //{
            //    var Paraemtros = new
            //    {
            //        P_Nombre = usuario.NomUsuario,
            //        P_APaterno = usuario.APaterno,
            //        P_AMaterno = usuario.AMaterno,
            //        P_Correo = usuario.Correo,
            //        P_Contraseña = usuario.Contraseña

            //    };

            //    DAL_Usuarios.AgregarUsuario(cadena, "SP_GUARDAR_USUARIO", Paraemtros);
            //    responde.Add("00");
            //    responde.Add("El usuario se agrego correctamente");
            //}
            //catch (SqlException e)
            //{
            //    responde.Add("14");
            //    responde.Add(e.Message);
            //}

            return responde;
        }

        public static List<DtoUsuario> ValidarUsuario(string cadena, string usuario, string contraseña)
        {
            List<DtoUsuario> lstUsuario = new List<DtoUsuario>();

            //var parametros = new
            //{
            //    P_Usuario = usuario,
            //    P_Contraseña = contraseña
            //};

            DtoUsuario User = new DtoUsuario()
            {
                Correo = usuario,
                Contraseña = contraseña
            };

            SqlDataReader dtUsuario = DAL_Usuarios.Login(cadena, "SP_LOGIN", User);

            if (dtUsuario.FieldCount>0)
            {

                //foreach (DataRow row in dtUsuario.Rows)
                //{
                //    DtoUsuario Usuario = new DtoUsuario();
                //    Usuario.IdUsuario = (int)row["IdUsuario"];
                //    Usuario.NomUsuario = (string)row["Nombre"];
                //    Usuario.APaterno = (string)row["APaterno"];
                //    Usuario.AMaterno = (string)row["AMaterno"];
                //    Usuario.Correo = (string)row["Correo"];
                //    lstUsuario.Add(Usuario);
                //}

                while (dtUsuario.Read())
                {
                    DtoUsuario Usuario = new DtoUsuario();
                    Usuario.IdUsuario = (int)dtUsuario["IdUsuario"];
                    Usuario.NomUsuario = (string)dtUsuario["Nombre"];
                    Usuario.APaterno = (string)dtUsuario["APaterno"];
                    Usuario.AMaterno = (string)dtUsuario["AMaterno"];
                    Usuario.Correo = (string)dtUsuario["Correo"];
                    lstUsuario.Add(Usuario);
                }
            }

            return lstUsuario;
        }
    }
}
