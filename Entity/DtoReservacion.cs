using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class DtoReservacion
    {
        public string IdReservacion { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FecReservacion { get; set; }
        public DateTime FecInicioResrvacion { get; set; }
        public DateTime FecFinReservacion { get; set; }
        public int Total { get; set; }
        public int CantNoches { get; set; }
        public string TipoHab { get; set; }
        public int Estatus { get; set; }
        public string noTarjeta { get; set; }
        public string noHabitacion { get; set; }

    }
}
