using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.Models.Api
{
    public class UsuarioSesion
    {
        public int ErrorCode { get; set; }
        public string ErrorDetail { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

    }
}