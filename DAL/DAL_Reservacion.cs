using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using Entity;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Reservacion
    {
        public async static Task AgregarRersvacion(DtoReservacion Reservacion)
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            await firebase
              .Child("Reservaciones")
              .PostAsync(new DtoReservacion()
              {
                  IdReservacion = Guid.NewGuid().ToString(),
                  IdUsuario = Reservacion.IdUsuario,
                  FecReservacion = DateTime.Now,
                  FecInicioResrvacion = Reservacion.FecInicioResrvacion,
                  FecFinReservacion = Reservacion.FecFinReservacion,
                  Total = Reservacion.Total,
                  CantNoches = Reservacion.CantNoches,
                  TipoHab = Reservacion.TipoHab

              });
        }

        public static async Task<List<DtoReservacion>> Reservaciones()
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            List<DtoReservacion> lstReservaciones = new List<DtoReservacion>();
            try
            {
                var reservacionesQuery = await firebase
                    .Child("Reservaciones")
                    .OnceAsync<DtoReservacion>();

                if (reservacionesQuery.Any())
                {
                    foreach (var reserv in reservacionesQuery)
                    {
                        DtoReservacion reservacion = reserv.Object;
                        lstReservaciones.Add(reservacion);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lstReservaciones;
        }

        public async static Task<List<DtoReservacion>> RservacionesUsuario(string IdUsuario)
        {
            List<DtoReservacion> reservcionesUsuario = new List<DtoReservacion>();

            List<DtoReservacion> lstRservaciones = await Reservaciones();

            foreach (DtoReservacion reserv in lstRservaciones)
            {
                if (reserv.IdUsuario == IdUsuario)
                {
                    reservcionesUsuario.Add(reserv);
                }
            }

            return reservcionesUsuario;
        }
    }
}
