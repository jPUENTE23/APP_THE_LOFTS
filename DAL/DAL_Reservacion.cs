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
                  TipoHab = Reservacion.TipoHab,
                  Estatus = Reservacion.Estatus,
                  noTarjeta = Reservacion.noTarjeta,
                  noHabitacion = Reservacion.noHabitacion

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

        public static async Task ActualizarRservacion(string reservacion, int estatus)
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            var actRservacion = (await firebase
              .Child("Reservaciones")
              .OnceAsync<DtoReservacion>()).Where(a => a.Object.IdReservacion == reservacion).FirstOrDefault();

            await firebase
              .Child("Reservaciones")
              .Child(actRservacion.Key)
              .PutAsync(new DtoReservacion() { Estatus = estatus});
        }


        public static async Task<DtoReservacion> detalleRservacion(string idRservacion)
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            var reservaciones = await Reservaciones();
            await firebase
              .Child("Reservaciones")
              .OnceAsync<DtoReservacion>();
            return reservaciones.Where(a => a.IdReservacion == idRservacion ).FirstOrDefault();
        }

        public static async Task eliminarRservacion(string idRservacion)
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            var reservacion = (await firebase
              .Child("Reservaciones")
              .OnceAsync<DtoReservacion>()).Where(a => a.Object.IdReservacion == idRservacion).FirstOrDefault();
            await firebase.Child("Reservaciones").Child(reservacion.Key).DeleteAsync();

        }
    }
}
