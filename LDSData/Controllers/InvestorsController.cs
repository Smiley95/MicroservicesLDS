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
using LDSData.DBContext;
using LDSData.Repositories;

namespace LDSData.Controllers
{
    [Authorize(Roles = "expert")]
    public class InvestorsController : ApiController
    {
        private IGenericRepository<Investor> repository = null;
        public InvestorsController()
        {
            this.repository = new GenericRepository<Investor>();
        }

        public InvestorsController(GenericRepository<Investor> repository)
        {
            this.repository = repository;
        }

        // GET: api/Investors
        public IEnumerable<Investor> GetInvestors()
        {
            return repository.GetAll();
        }

        // GET: api/Investors/idExpert
        //[Route("GetInvestorsByExpert")]
        [HttpGet]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetInvestorsByExpert([FromBody]string expertID)
        {
            return Ok(repository.GetAll().Where(c => c.Expert_ID.Equals(expertID)).Select(e => e));
        }

        // GET: api/Investors/5
        [ResponseType(typeof(Investor))]
        public IHttpActionResult GetInvestor(string id)
        {
            Investor investor = repository.GetById(id);
            if (investor == null)
            {
                return NotFound();
            }

            return Ok(investor);
        }

        // PUT: api/Investors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvestor(string id, Investor investor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != investor.Investor_ID)
            {
                return BadRequest();
            }

            repository.Update(investor);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestorExists(id))
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
        [ResponseType(typeof(Investor))]
        public IHttpActionResult PostInvestor(Investor investor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Insert(investor);

            try
            {
                repository.Save();
            }
            catch (DbUpdateException)
            {
                if (InvestorExists(investor.Investor_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = investor.Investor_ID }, investor);
        }

        // DELETE: api/Investors/5
        [ResponseType(typeof(Investor))]
        public IHttpActionResult DeleteInvestor(string id)
        {
            Investor investor = repository.GetById(id);
            if (investor == null)
            {
                return NotFound();
            }

            repository.Delete(investor);
            repository.Save();

            return Ok(investor);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private bool InvestorExists(string id)
        {
            return repository.GetById(id) != null ? true : false;
        }
    }
}