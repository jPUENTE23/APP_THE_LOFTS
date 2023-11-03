using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class DtoUsuario
    {
        public string IdUsuario { get; set; }
        public string NomUsuario { get; set; }
        public string Apellidos { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
