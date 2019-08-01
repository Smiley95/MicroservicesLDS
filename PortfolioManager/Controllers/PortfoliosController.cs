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
using PortfolioManager.Models;

namespace PortfolioManager.Controllers
{
    public class PortfoliosController : ApiController
    {
        private optimizerEntities db = new optimizerEntities();

        // GET: api/Portfolios
        public IQueryable<Portfolios> GetPortfolios()
        {
            return db.Portfolios;
        }

        // GET: api/Portfolios/5
        [ResponseType(typeof(Portfolios))]
        public IHttpActionResult GetPortfolios(int id)
        {
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return NotFound();
            }

            return Ok(portfolios);
        }

        // PUT: api/Portfolios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPortfolios(int id, Portfolios portfolios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != portfolios.Portfolio_ID)
            {
                return BadRequest();
            }

            db.Entry(portfolios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfoliosExists(id))
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

        // POST: api/Portfolios
        [ResponseType(typeof(Portfolios))]
        public IHttpActionResult PostPortfolios(Portfolios portfolios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Portfolios.Add(portfolios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = portfolios.Portfolio_ID }, portfolios);
        }

        // DELETE: api/Portfolios/5
        [ResponseType(typeof(Portfolios))]
        public IHttpActionResult DeletePortfolios(int id)
        {
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return NotFound();
            }

            db.Portfolios.Remove(portfolios);
            db.SaveChanges();

            return Ok(portfolios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PortfoliosExists(int id)
        {
            return db.Portfolios.Count(e => e.Portfolio_ID == id) > 0;
        }
    }
}