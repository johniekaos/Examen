using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PuntoVenta.Models.Web
{
    public class Pedido
    {
        public Pedido()
        {
            PEDIDOS_DETALLE_W = new List<DetallesPedido>();
        }
        [Key]
        [Display(Name = "Id Pedido")]
        public int ID { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "El {0} es obligatorio")]
        public decimal TOTAL { get; set; }

        [Display(Name = "Fecha de venta")]

        public DateTime DATE_SALE { get; set; }

        [Display(Name = "Usuario venta")]
        public string USERNAME { get; set; }

        [Display(Name = "Detalles")]
        public List<DetallesPedido> PEDIDOS_DETALLE_W { get; set; }

    }
}