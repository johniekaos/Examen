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
    public class PedidosDetalleController : ApiController
    {
        private ExamenDatabase db = new ExamenDatabase();

        [HttpGet]
        public List<PEDIDOS_DETALLE_W> Get()
        {
            return db.PEDIDOS_DETALLE_W.ToList();
        }

        [HttpGet]
        [ResponseType(typeof(PEDIDOS_DETALLE_W))]
        public IHttpActionResult Get(int id)
        {
            PEDIDOS_DETALLE_W pEDIDOS_DETALLE_W = db.PEDIDOS_DETALLE_W.Find(id);
            if (pEDIDOS_DETALLE_W == null)
            {
                return NotFound();
            }

            return Ok(pEDIDOS_DETALLE_W);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, PEDIDOS_DETALLE_W pEDIDOS_DETALLE_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pEDIDOS_DETALLE_W.ID)
            {
                return BadRequest();
            }

            db.Entry(pEDIDOS_DETALLE_W).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PEDIDOS_DETALLE_WExists(id))
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
        [ResponseType(typeof(PEDIDOS_DETALLE_W))]
        public IHttpActionResult Post(PEDIDOS_DETALLE_W pEDIDOS_DETALLE_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PEDIDOS_DETALLE_W.Add(pEDIDOS_DETALLE_W);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pEDIDOS_DETALLE_W.ID }, pEDIDOS_DETALLE_W);
        }

        [HttpDelete]
        [ResponseType(typeof(PEDIDOS_DETALLE_W))]
        public IHttpActionResult Delete(int id)
        {
            PEDIDOS_DETALLE_W pEDIDOS_DETALLE_W = db.PEDIDOS_DETALLE_W.Find(id);
            if (pEDIDOS_DETALLE_W == null)
            {
                return NotFound();
            }

            db.PEDIDOS_DETALLE_W.Remove(pEDIDOS_DETALLE_W);
            db.SaveChanges();

            return Ok(pEDIDOS_DETALLE_W);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PEDIDOS_DETALLE_WExists(int id)
        {
            return db.PEDIDOS_DETALLE_W.Count(e => e.ID == id) > 0;
        }
    }
}