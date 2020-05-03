using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.WebAPI.Models
{
    public abstract class IRequest
    {
        public string Token { get; set; }
    }
}