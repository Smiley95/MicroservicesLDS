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
using RiskService.DBContext;
using Newtonsoft.Json.Linq;
using RiskService.Models;

namespace RiskService.Controllers
{
    public class RisksController : ApiController
    {
        private LDSEntities db = new LDSEntities();

        // GET: api/Risks
        public IQueryable<Risk> GetRisk()
        {
            return db.Risk;
        }
        public IHttpActionResult GetRiskBeta(string companySymbol)
        {
            double claimTerms = HttpHelper.GetBetaRisk(companySymbol);
            return Ok(claimTerms);
        }
        public IHttpActionResult GetRiskStandDev(string companySymbol,int nbYears)
        {
            double claimTerms = HttpHelper.GetStandardDeviationRisk(companySymbol, nbYears);
            return Ok(claimTerms);
        }
        
        // GET: api/Risks/5
        [ResponseType(typeof(Risk))]
        public IHttpActionResult GetRisk(string id)
        {
            Risk risk = db.Risk.Find(id);
            if (risk == null)
            {
                return NotFound();
            }

            return Ok(risk);
        }
        //https://www.businessmanagementideas.com/investment/risk-and-return-investment/risk-and-return-on-single-asset-investments-financial-management/16110

        // PUT: api/Risks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRisk(string id, Risk risk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != risk.Risk_ID)
            {
                return BadRequest();
            }

            db.Entry(risk).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskExists(id))
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

        // POST: api/Risks
        [ResponseType(typeof(Risk))]
        public IHttpActionResult PostRisk(Risk risk)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Risk.Add(risk);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RiskExists(risk.Risk_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = risk.Risk_ID }, risk);
        }

        // DELETE: api/Risks/5
        [ResponseType(typeof(Risk))]
        public IHttpActionResult DeleteRisk(string id)
        {
            Risk risk = db.Risk.Find(id);
            if (risk == null)
            {
                return NotFound();
            }

            db.Risk.Remove(risk);
            db.SaveChanges();

            return Ok(risk);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RiskExists(string id)
        {
            return db.Risk.Count(e => e.Risk_ID == id) > 0;
        }
    }
}