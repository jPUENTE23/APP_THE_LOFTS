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
                  CantNoches = Reservacion.CantNoches

              });
        }

        public async static Task<List<DtoReservacion>> Reservaciones()
        {
            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            List<DtoReservacion> lstReservaciones = new List<DtoReservacion>();

            var reservacionesQuery = await firebase
                .Child("Rservaciones")
                .OnceAsync<DtoReservacion>();

            foreach (var reserv in reservacionesQuery)
            {
                DtoReservacion reservacion = reserv.Object;
                lstReservaciones.Add(reservacion);
            }

            return lstReservaciones;
        }

        public async static Task<List<DtoReservacion>> RservacionesUsuario (string IdUsuario)
        {
            List<DtoReservacion> lstReservaciones = new List<DtoReservacion>();

            FirebaseClient firebase = new FirebaseClient("https://thelofts-1c252-default-rtdb.firebaseio.com/");
            var allPersons = await Reservaciones();
            await firebase
              .Child("Rservaciones")
              .OnceAsync<DtoReservacion>();

            lstReservaciones.Add(allPersons.Where(a => a.IdUsuario == IdUsuario).FirstOrDefault());

            return lstReservaciones;
        }
    }
}
