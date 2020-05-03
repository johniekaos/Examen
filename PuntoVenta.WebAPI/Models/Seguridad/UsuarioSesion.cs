using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.WebAPI.Models.Seguridad
{
    public class UsuarioSesion : IResponse
    {
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

    }
}