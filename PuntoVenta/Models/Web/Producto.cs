using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PuntoVenta.Models.Web
{
    public class Producto
    {
        [Key]
        [Display(Name = "Sku")]
        public string SKU { get; set; }

        [Display(Name = "Nombre Producto")]
        public string NOMBRE { get; set; }

        [Display(Name = "Existencia")]
        public int EXISTENCIA { get; set; }

        [Display(Name = "Precio")]
        public decimal PRICE { get; set; }

        [Display(Name = "Estatus")]
        public Nullable<bool> ACTIVO { get; set; }
    }
}