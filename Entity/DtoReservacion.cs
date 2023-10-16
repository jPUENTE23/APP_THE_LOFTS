using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class DtoReservacion
    {
        public int IdReservacion { get; set; }
        public int IdUsuario { get; set; }
        public int FecReservacion { get; set; }
        public int FecInicioResrvacion { get; set; }
        public int FecFinReservacion { get; set; }
        public int CantHospedados { get; set; }
        public int TipoPago { get; set; }
        public int EstatusPago { get; set; }
        public int TotalPagar { get; set; }
        public int IdHabitacion { get; set; }
    }
}
