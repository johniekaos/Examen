using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.Models.Seguridad
{
    public class LogIn
    {
        public string Usuario { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }

    }
}