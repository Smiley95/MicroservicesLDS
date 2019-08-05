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
using ReturnService.Models;

namespace ReturnService.Controllers
{
    public class ReturnsController : ApiController
    {
        private optimizerEntities db = new optimizerEntities();

        // GET: api/Returns
        public IQueryable<Returns> GetReturns()
        {
            return db.Returns;
        }

        // GET: api/Returns/5
        [ResponseType(typeof(Returns))]
        public IHttpActionResult GetReturns(string id)
        {
            Returns returns = db.Returns.Find(id);
            if (returns == null)
            {
                return NotFound();
            }

            return Ok(returns);
        }

        // PUT: api/Returns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReturns(string id, Returns returns)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != returns.Return_ID)
            {
                return BadRequest();
            }

            db.Entry(returns).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnsExists(id))
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

        // POST: api/Returns
        [ResponseType(typeof(Returns))]
        public IHttpActionResult PostReturns(Returns returns)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Returns.Add(returns);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReturnsExists(returns.Return_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = returns.Return_ID }, returns);
        }

        // DELETE: api/Returns/5
        [ResponseType(typeof(Returns))]
        public IHttpActionResult DeleteReturns(string id)
        {
            Returns returns = db.Returns.Find(id);
            if (returns == null)
            {
                return NotFound();
            }

            db.Returns.Remove(returns);
            db.SaveChanges();

            return Ok(returns);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReturnsExists(string id)
        {
            return db.Returns.Count(e => e.Return_ID == id) > 0;
        }
    }
}