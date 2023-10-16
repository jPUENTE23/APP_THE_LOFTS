using System;
using DAL;
using Entity;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace BLL
{
    public class BL_Habitacion
    {
        public static List<DtoHabitacion> Habitaciones(string cadena)
        {
            List<DtoHabitacion> lstHabitacion = new List<DtoHabitacion>();
            DataTable dtHabitacion = DAL_Habitaciones.Habitaciones(cadena, "SP_HABITACIONES");


            foreach (DataRow row in dtHabitacion.Rows)
            {
                DtoHabitacion Habitacion = new DtoHabitacion();
                Habitacion.IdHabitacion = (int)row["IdHabitacion"];
                Habitacion.NoHabitacion = (int)row["NoHabitacion"];
                Habitacion.TipoHbitacion = (string)row["TipoHbitacion"];
                Habitacion.Estatus = (int)row["Estatus"];
                lstHabitacion.Add(Habitacion);

            }
            return lstHabitacion;

        }

        public static List<DtoHabitacion> TipoHabitacion(string cadena, string Tipo)
        {
            List<DtoHabitacion> lstHabitaciones = new List<DtoHabitacion>();

            var Parametro = new
            {
                P_TipoHabitacion = Tipo
            };

            DataTable dtHabitacion = DAL_Habitaciones.ListarTiposHabitacion(cadena, "SP_LISTAR_TIPO_HABITACION", Parametro);
            foreach (DataRow row in dtHabitacion.Rows)
            {
                DtoHabitacion Habitacion = new DtoHabitacion();
                Habitacion.IdHabitacion = (int)row["IdHabitacion"];
                Habitacion.NoHabitacion = (int)row["NoHabitacion"];
                Habitacion.TipoHbitacion = (string)row["TipoHbitacion"];
                Habitacion.Estatus = (int)row["Estatus"];
                lstHabitaciones.Add(Habitacion);
            }

            return lstHabitaciones;
        }


        public static int ValidarEstatus(string cadena, string Tipo)
        {
            int disponibles = 0;
            var Parametros = new
            {
                P_Tipo = Tipo
            };

            DataTable dt = DAL_Habitaciones.HabitacioneDisp(cadena, "SP_VALIDAR_ESTATUS", Parametros);

            foreach (DataRow row in dt.Rows)
            {
                disponibles = (int)row["CantHabitaciones"];
            }


            return disponibles;

        }
    }
}
