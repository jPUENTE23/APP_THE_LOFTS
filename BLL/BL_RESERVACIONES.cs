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

            int numeroDeNoches = diferencia.Days - 1;

            return numeroDeNoches;
        }

        public static async Task<List<DtoReservacion>> RsservacionesUs(string IdUsuario)
        {
            List<DtoReservacion> lstRservaciones = await DAL_Reservacion.RservacionesUsuario(IdUsuario);

            return lstRservaciones;
        }
    }
}
