using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PuntoVenta.DataAccess;

namespace PuntoVenta.WebAPI.Controllers
{
    public class PedidosController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ExamenDatabase db = new ExamenDatabase();

        [HttpGet]
        public IQueryable<PEDIDOS_W> Get()
        {
            return db.PEDIDOS_W;
        }

        [HttpGet]
        [ResponseType(typeof(PEDIDOS_W))]
        public IHttpActionResult Get(int id)
        {
            PEDIDOS_W pedidos = db.PEDIDOS_W.Find(id);
            if (pedidos == null)
            {
                return NotFound();
            }
            pedidos.PEDIDOS_DETALLE_W = db.PEDIDOS_DETALLE_W.Where(p => p.ID_PEDIDO == id).ToList();

            return Ok(pedidos);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, PEDIDOS_W pEDIDOS_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pEDIDOS_W.ID)
            {
                return BadRequest();
            }

            db.Entry(pEDIDOS_W).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(PEDIDOS_W))]
        public IHttpActionResult Post(PEDIDOS_W pedido)
        {
            log.Debug($"Se recibe pedido {pedido.PEDIDOS_DETALLE_W.Count} productos");
            decimal total = 0;
            try
            {
                if (pedido.PEDIDOS_DETALLE_W != null && pedido.PEDIDOS_DETALLE_W.Count > 0)
                {
                    foreach (var det in pedido.PEDIDOS_DETALLE_W)
                    {
                        var productoDB = db.PRODUCTO_W.Where(p => p.SKU == det.SKU).FirstOrDefault();

                        det.PRODUCTO_W = productoDB;
                        det.PRICE = productoDB.PRICE;
                        total += det.AMOUT.Value * det.PRICE.Value;
                        det.ID_PEDIDO = pedido.ID;
                        db.PEDIDOS_DETALLE_W.Add(det);
                        log.Debug($" ---- Producto sku [{det.SKU}], cantidad [{det.AMOUT}] total[{det.AMOUT.Value * det.PRICE.Value}] Acumulado [{total}]");
                        productoDB.EXISTENCIA -= Convert.ToInt32(det.AMOUT.Value);
                        db.Entry(productoDB).State = EntityState.Modified;
                    }
                }

                if (!(pedido.TOTAL.Value > 0))
                {
                    pedido.TOTAL = total;
                }
                log.Debug($"Total pedido [{pedido.TOTAL}]");
                db.PEDIDOS_W.Add(pedido);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error("Ocurrio un error en el almacenado", ex);
            }
            return Ok(pedido);
        }

        [HttpDelete]
        [ResponseType(typeof(PEDIDOS_W))]
        public IHttpActionResult Delete(int id)
        {
            PEDIDOS_W pEDIDOS_W = db.PEDIDOS_W.Find(id);
            if (pEDIDOS_W == null)
            {
                return NotFound();
            }

            db.PEDIDOS_W.Remove(pEDIDOS_W);
            db.SaveChanges();

            return Ok(pEDIDOS_W);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidosExists(int id)
        {
            return db.PEDIDOS_W.Count(e => e.ID == id) > 0;
        }
    }
}