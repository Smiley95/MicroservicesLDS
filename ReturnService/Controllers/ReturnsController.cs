using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using ReturnService.DBContext;
using ReturnService.Models;

namespace ReturnService.Controllers
{
    public class ReturnsController : ApiController
    {
        private LDSEntities db = new LDSEntities();

        // GET: api/Returns
        public IHttpActionResult GetReturns(string companySymbol)
        {
            double claimTerms = HttpHelper.GetCompanyFinancialState(companySymbol);
            return Ok(claimTerms);
        }

        // GET: api/Returns/5
        [ResponseType(typeof(Return))]
        public IHttpActionResult GetReturn(string id)
        {
            Return @return = db.Return.Find(id);
            if (@return == null)
            {
                return NotFound();
            }

            return Ok(@return);
        }

        // PUT: api/Returns/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReturn(string id, Return @return)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @return.return_ID)
            {
                return BadRequest();
            }

            db.Entry(@return).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReturnExists(id))
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
        [ResponseType(typeof(Return))]
        public IHttpActionResult PostReturn(Return @return)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Return.Add(@return);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReturnExists(@return.return_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = @return.return_ID }, @return);
        }

        // DELETE: api/Returns/5
        [ResponseType(typeof(Return))]
        public IHttpActionResult DeleteReturn(string id)
        {
            Return @return = db.Return.Find(id);
            if (@return == null)
            {
                return NotFound();
            }

            db.Return.Remove(@return);
            db.SaveChanges();

            return Ok(@return);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReturnExists(string id)
        {
            return db.Return.Count(e => e.return_ID == id) > 0;
        }
    }
}