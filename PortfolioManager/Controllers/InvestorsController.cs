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
    [Authorize(Roles = "Expert")]
    public class InvestorsController : ApiController
    {
        private optimizerEntities db = new optimizerEntities();

        // GET: api/Investors
        public IQueryable<Investors> GetInvestors()
        {
            return db.Investors;
        }

        // GET: api/Investors/5
        [ResponseType(typeof(Investors))]
        public IHttpActionResult GetInvestors(int id)
        {
            Investors investors = db.Investors.Find(id);
            if (investors == null)
            {
                return NotFound();
            }

            return Ok(investors);
        }

        // PUT: api/Investors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvestors(string id, Investors investors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != investors.client_email)
            {
                return BadRequest();
            }

            db.Entry(investors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestorsExists(id))
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

        // POST: api/Investors
        [ResponseType(typeof(Investors))]
        public IHttpActionResult PostInvestors(Investors investors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Investors.Add(investors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = investors.client_email }, investors);
        }

        // DELETE: api/Investors/5
        [ResponseType(typeof(Investors))]
        public IHttpActionResult DeleteInvestors(int id)
        {
            Investors investors = db.Investors.Find(id);
            if (investors == null)
            {
                return NotFound();
            }

            db.Investors.Remove(investors);
            db.SaveChanges();

            return Ok(investors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvestorsExists(string id)
        {
            return db.Investors.Count(e => e.client_email == id) > 0;
        }
    }
}