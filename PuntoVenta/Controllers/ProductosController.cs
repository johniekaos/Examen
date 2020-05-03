using Microsoft.Ajax.Utilities;
using PuntoVenta.Models.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.UI;

namespace PuntoVenta.Controllers
{
    public class ProductosController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [System.Web.Mvc.HttpGet]
        public ActionResult Get(string sku)
        {
            var producto = new Producto();
            try
            {
                var response = GlobalVariables.webClient.GetAsync($"Productos/Get/{sku}").Result;
                producto = response.Content.ReadAsAsync<Producto>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener Producto", ex);
            }
            return Json(producto, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsAvailable(int cantidad, string SKU)
        {
            bool resultado = false;
            try
            {
                var response = GlobalVariables.webClient.GetAsync($"Productos/Get/{SKU}").Result;
                var producto = response.Content.ReadAsAsync<Producto>().Result;
                if (cantidad <= producto.EXISTENCIA)
                    resultado = true;
            }
            catch (Exception ex)
            {
                log.Error("error en verificar disponibilidad", ex);
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var productos = new List<Producto>();
            try
            {
                var httpResponse = GlobalVariables.webClient.GetAsync("Productos/Get").Result;
                productos = httpResponse.Content.ReadAsAsync<List<Producto>>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener Productos", ex);
            }
            return View(productos);
        }
    }
}