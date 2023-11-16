using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Entity;
using System.Threading.Tasks;

namespace BLL
{
    public class BL_RESERVACIONES
    {
        public static async Task<List<string>> GuardarReservacion(DtoReservacion Rservacion)
        {
            List<string> response = new List<string>();

            try
            {
                await DAL_Reservacion.AgregarRersvacion(Rservacion);
                response.Add("00");
                response.Add("La reservacion se registro correctamente");

            }
            catch(Exception e)
            {
                response.Add("14");
                response.Add(e.Message);
            }

            return response;
        }

        public static int nochesRersevacion (DateTime fenInicio, DateTime fecFin)
        {
            TimeSpan diferencia = fecFin - fenInicio;

            int numeroDeNoches = diferencia.Days;

            return numeroDeNoches;
        }

        public static async Task<List<DtoReservacion>> Rsservaciones(string TipoHab)
        {
            List<DtoReservacion> habReservadas = new List<DtoReservacion>();
            List<DtoReservacion> lstRservaciones = await DAL_Reservacion.Reservaciones();
            foreach (DtoReservacion reserv in lstRservaciones)
            {
                if (reserv.TipoHab == TipoHab && reserv.Estatus == 1)
                {
                    habReservadas.Add(reserv);
                }
            }
            return habReservadas;
        }

        public static async Task<List<DtoReservacion>> RsservacionesUsuario(string IdUsuario)
        {
            List<DtoReservacion> reservacionUsuario = await DAL_Reservacion.RservacionesUsuario(IdUsuario);
            return reservacionUsuario;


        }

        public static async Task actualziarEstatusRservaion()
        {
            List<DtoReservacion> lstReservaciones = await DAL_Reservacion.Reservaciones();

            foreach (DtoReservacion reserv in lstReservaciones)
            {
                if(DateTime.Now > reserv.FecFinReservacion)
                {
                    await DAL_Reservacion.ActualizarRservacion(reserv.IdReservacion, 0);
                }
            }
        }

        public static async Task<List<DtoReservacion>> datosRservacion (string idRservacion)
        {
            List<DtoReservacion> lstRservacion = new List<DtoReservacion>();

            try
            {
                DtoReservacion reservacion = await DAL_Reservacion.detalleRservacion(idRservacion);
                lstRservacion.Add(reservacion);
            }
            catch (Exception e)
            {
                throw e;
            }

            return lstRservacion;
        }

        public static async Task<List<string>> eliminarReserv(string idRservacion)
        {
            List<string> response = new List<string>();
            try
            {
                await DAL_Reservacion.eliminarRservacion(idRservacion);
                response.Add("00");
                response.Add("La reservacion se elimino correctamente");
            }
            catch(Exception e)
            {
                response.Add("14");
                response.Add(e.Message);
            }

            return response;
        }
    }
}
