using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.WebAPI.Models
{
    public abstract class IResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorDetail { get; set; }
    }
}