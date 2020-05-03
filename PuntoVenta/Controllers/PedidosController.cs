using PuntoVenta.Funciones;
using PuntoVenta.Models.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PuntoVenta.Controllers
{
    public class PedidosController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [SessionExpireFilter]
        public ActionResult Index()
        {
            var listado = new List<Pedido>();
            try
            {
                var httpResponseMessage = GlobalVariables.webClient.GetAsync("Pedidos").Result;
                listado = httpResponseMessage.Content.ReadAsAsync<List<Pedido>>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Error obteniendo los pedidos", ex);
            }
            return View(listado);
        }

        [HttpGet]
        [SessionExpireFilter]
        public ActionResult Create()
        {

            try
            {
                var response = GlobalVariables.webClient.GetAsync("Productos").Result;
                var listaProductos = response.Content.ReadAsAsync<List<Producto>>().Result;
                ViewBag.SKU = new SelectList(listaProductos, "SKU", "NOMBRE");

            }
            catch (Exception ex)
            {
                log.Error("Error en la consulta", ex);
            }
            return View();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Create(Pedido pedido)
        {
            return View(pedido);
        }

        [HttpPost]
        public ActionResult PostDetails(ListaDetallesSimple detalles)
        {
            var _response = false;
            Pedido pedido = new Pedido();
            List<DetallesPedido> detalle = new List<DetallesPedido>();
            try
            {
                pedido.DATE_SALE = DateTime.Now;
                pedido.USERNAME = General.GetUsuario().Usuario;
                foreach (var d in detalles.Registro)
                {
                    DetallesPedido temp = new DetallesPedido();
                    temp.AMOUT = int.Parse(d.Cantidad);
                    temp.SKU = d.Sku;
                    detalle.Add(temp);
                }
                pedido.PEDIDOS_DETALLE_W = detalle;

                var httpResponse = GlobalVariables.webClient.PostAsJsonAsync("Pedidos/Post", pedido).Result;
                var pedidoResponse = httpResponse.Content.ReadAsAsync<Pedido>().Result;
                _response = true;
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error en la insercion del pediddo", ex);
                _response = false;
            }
            return Json(_response, JsonRequestBehavior.AllowGet);

        }

        [SessionExpireFilter]
        public ActionResult Details(int Id)
        {
            ViewBag.ID_PEDIDO = Id;
            var detalle = new List<DetallesPedido>();
            try
            {
                var httpResponse = GlobalVariables.webClient.GetAsync($"Pedidos/Get/{Id}").Result;
                var pedido = httpResponse.Content.ReadAsAsync<Pedido>().Result;
                detalle = pedido.PEDIDOS_DETALLE_W;
                ViewBag.TOTAL = pedido.TOTAL;
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error en la insercion del pediddo", ex);
            }
            return View(detalle);
        }

    }
}