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
    public class CurrenciesController : ApiController
    {
        private optimizerEntities db = new optimizerEntities();

        // GET: api/Currencies
        public IQueryable<Currencies> GetCurrencies()
        {
            return db.Currencies;
        }

        // GET: api/Currencies/5
        [ResponseType(typeof(Currencies))]
        public IHttpActionResult GetCurrencies(int id)
        {
            Currencies currencies = db.Currencies.Find(id);
            if (currencies == null)
            {
                return NotFound();
            }

            return Ok(currencies);
        }

        // PUT: api/Currencies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCurrencies(int id, Currencies currencies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currencies.Currency_symbol)
            {
                return BadRequest();
            }

            db.Entry(currencies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrenciesExists(id))
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

        // POST: api/Currencies
        [ResponseType(typeof(Currencies))]
        public IHttpActionResult PostCurrencies(Currencies currencies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Currencies.Add(currencies);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = currencies.Currency_symbol }, currencies);
        }

        // DELETE: api/Currencies/5
        [ResponseType(typeof(Currencies))]
        public IHttpActionResult DeleteCurrencies(int id)
        {
            Currencies currencies = db.Currencies.Find(id);
            if (currencies == null)
            {
                return NotFound();
            }

            db.Currencies.Remove(currencies);
            db.SaveChanges();

            return Ok(currencies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurrenciesExists(int id)
        {
            return db.Currencies.Count(e => e.Currency_symbol == id) > 0;
        }
    }
}