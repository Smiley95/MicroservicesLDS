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
    public class CompaniesController : ApiController
    {
        private optimizerEntities db = new optimizerEntities();

        // GET: api/Companies
        public IQueryable<Companies> GetCompanies()
        {
            return db.Companies;
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Companies))]
        public IHttpActionResult GetCompanies(int id)
        {
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return NotFound();
            }

            return Ok(companies);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompanies(int id, Companies companies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companies.Company_ID)
            {
                return BadRequest();
            }

            db.Entry(companies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompaniesExists(id))
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

        // POST: api/Companies
        [ResponseType(typeof(Companies))]
        public IHttpActionResult PostCompanies(Companies companies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Companies.Add(companies);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companies.Company_ID }, companies);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Companies))]
        public IHttpActionResult DeleteCompanies(int id)
        {
            Companies companies = db.Companies.Find(id);
            if (companies == null)
            {
                return NotFound();
            }

            db.Companies.Remove(companies);
            db.SaveChanges();

            return Ok(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompaniesExists(int id)
        {
            return db.Companies.Count(e => e.Company_ID == id) > 0;
        }
    }
}