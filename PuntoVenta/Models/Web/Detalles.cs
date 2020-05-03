using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PuntoVenta.Models.Web
{
    public class DetallesPedido
    {
        public DetallesPedido()
        {
            PRODUCTO_W = new Producto();
        }

        [Display(Name = "Id")]
        public int ID { get; set; }

        [Display(Name = "Id Pedido")]
        public int ID_PEDIDO { get; set; }

        [Display(Name = "Sku")]
        public string SKU { get; set; }

        [Display(Name = "Cantidad")]
        [Remote("IsAvailable", "Productos", AdditionalFields = "SKU", ErrorMessage = "Producto no disponible")]
        [Range(0, Int32.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public decimal AMOUT { get; set; }

        [Display(Name = "Precio")]
        public decimal PRICE { get; set; }

        [Display(Name = "Pedido")]
        public Pedido PEDIDOS_W { get; set; }

        [Display(Name = "Producto")]
        public Producto PRODUCTO_W { get; set; }
    }
}