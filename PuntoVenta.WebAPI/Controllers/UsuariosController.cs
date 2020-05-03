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
    public class UsuariosController : ApiController
    {
        private ExamenDatabase db = new ExamenDatabase();

        [HttpGet]
        public IQueryable<USUARIOS_W> Get()
        {
            return db.USUARIOS_W;
        }

        [HttpGet]
        [ResponseType(typeof(USUARIOS_W))]
        public IHttpActionResult Get(string id)
        {
            USUARIOS_W uSUARIOS_W = db.USUARIOS_W.Find(id);
            if (uSUARIOS_W == null)
            {
                return NotFound();
            }

            return Ok(uSUARIOS_W);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(string id, USUARIOS_W uSUARIOS_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIOS_W.USERNAME)
            {
                return BadRequest();
            }

            db.Entry(uSUARIOS_W).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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
        [ResponseType(typeof(USUARIOS_W))]
        public IHttpActionResult Post(USUARIOS_W uSUARIOS_W)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIOS_W.Add(uSUARIOS_W);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(uSUARIOS_W.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = uSUARIOS_W.USERNAME }, uSUARIOS_W);
        }

        [HttpDelete]
        [ResponseType(typeof(USUARIOS_W))]
        public IHttpActionResult Delete(string id)
        {
            USUARIOS_W uSUARIOS_W = db.USUARIOS_W.Find(id);
            if (uSUARIOS_W == null)
            {
                return NotFound();
            }

            db.USUARIOS_W.Remove(uSUARIOS_W);
            db.SaveChanges();

            return Ok(uSUARIOS_W);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(string id)
        {
            return db.USUARIOS_W.Count(e => e.USERNAME == id) > 0;
        }
    }
}