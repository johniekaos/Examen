using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PuntoVenta.Models.Web
{
    public class ListaDetallesSimple
    {
        public ListaDetallesSimple()
        {
            Registro = new List<DetalleSimple>();
        }
        public List<DetalleSimple> Registro { get; set; }

    }
    public class DetalleSimple
    {
        public string Sku { get; set; }
        public string Cantidad { get; set; }
    }
}