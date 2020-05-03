using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.WebAPI.Models.Seguridad
{
    public class LoginRequest:IRequest
    {
        public string Usuario { get; set; }
        public string Password { get; set; }

    }
}