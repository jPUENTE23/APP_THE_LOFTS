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
    }
}
