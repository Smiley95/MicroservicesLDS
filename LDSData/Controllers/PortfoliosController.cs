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
    public class PortfoliosController : ApiController
    {
        private IGenericRepository<Portfolio> repository = null;
        public PortfoliosController()
        {
            this.repository = new GenericRepository<Portfolio>();
        }

        public PortfoliosController(GenericRepository<Portfolio> repository)
        {
            this.repository = repository;
        }
        // GET: api/Portfolios
        public IEnumerable<Portfolio> GetPortfolio()
        {
            return repository.GetAll();
        }

        // GET: api/Portfolios/5
        [ResponseType(typeof(Portfolio))]
        public IHttpActionResult GetPortfolio(string id)
        {
            Portfolio portfolio = repository.GetById(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(portfolio);
        }

        // GET: api/GetPortfoliosByInvestor
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Portfolio>))]
        public IHttpActionResult GetPortfoliosByInvestor([FromBody]string InvestorID)
        {
            return Ok(repository.GetAll().Where(c => c.Investor_ID.Equals(InvestorID)).Select(e => e));
        }

        // PUT: api/Portfolios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPortfolio(string id, Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != portfolio.Portfolio_ID)
            {
                return BadRequest();
            }

            repository.Update(portfolio);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioExists(id))
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
        [ResponseType(typeof(Portfolio))]
        public IHttpActionResult PostPortfolio(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Insert(portfolio);

            try
            {
                repository.Save();
            }
            catch (DbUpdateException)
            {
                if (PortfolioExists(portfolio.Portfolio_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = portfolio.Portfolio_ID }, portfolio);
        }

        // DELETE: api/Portfolios/5
        [ResponseType(typeof(Portfolio))]
        public IHttpActionResult DeletePortfolio(string id)
        {
            Portfolio portfolio = repository.GetById(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            repository.Delete(portfolio);
            repository.Save();

            return Ok(portfolio);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private bool PortfolioExists(string id)
        {
            return repository.GetById(id) != null ? true : false;
        }
    }
}