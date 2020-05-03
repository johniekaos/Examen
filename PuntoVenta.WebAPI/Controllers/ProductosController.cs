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
    public class ProductosController : ApiController
    {
        private ExamenDatabase db = new ExamenDatabase();

        [HttpGet]
        public IQueryable<PRODUCTO_W> Get()
        {
            return db.PRODUCTO_W;
        }

        [HttpGet]
        [ResponseType(typeof(PRODUCTO_W))]
        public IHttpActionResult Get(string id)
        {
            PRODUCTO_W pRODUCTO_W = db.PRODUCTO_W.Find(id);
            if (pRODUCTO_W == null)
            {
                return NotFound();
            }

            return Ok(pRODUCTO_W);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(string id, PRODUCTO_W pRODUCTO_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pRODUCTO_W.SKU)
            {
                return BadRequest();
            }

            db.Entry(pRODUCTO_W).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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
        [ResponseType(typeof(PRODUCTO_W))]
        public IHttpActionResult Post(PRODUCTO_W pRODUCTO_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PRODUCTO_W.Add(pRODUCTO_W);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductoExists(pRODUCTO_W.SKU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pRODUCTO_W.SKU }, pRODUCTO_W);
        }

        [HttpDelete]
        [ResponseType(typeof(PRODUCTO_W))]
        public IHttpActionResult Delete(string id)
        {
            PRODUCTO_W pRODUCTO_W = db.PRODUCTO_W.Find(id);
            if (pRODUCTO_W == null)
            {
                return NotFound();
            }

            db.PRODUCTO_W.Remove(pRODUCTO_W);
            db.SaveChanges();

            return Ok(pRODUCTO_W);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(string id)
        {
            return db.PRODUCTO_W.Count(e => e.SKU == id) > 0;
        }
    }
}