using PuntoVenta.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.Funciones
{
    public class General
    {
        public static UsuarioSesion GetUsuario()
        {
            var usr = (HttpContext.Current.Session[Constantes.VAR_SESION] as UsuarioSesion);
            return usr;
        }
    }
}