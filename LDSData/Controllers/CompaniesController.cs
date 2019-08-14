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

    [Authorize]
    public class CompaniesController : ApiController
    {
        private IGenericRepository<Company> repository = null;
        public CompaniesController()
        {
            this.repository = new GenericRepository<Company>();
        }

        public CompaniesController(GenericRepository<Company> repository)
        {
            this.repository = repository;
        }
        // GET: api/Companies
        public IEnumerable<Company> GetCompany()
        {
            return repository.GetAll();
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(string id)
        {
            Company company = repository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(string id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.Company_symbol)
            {
                return BadRequest();
            }

            repository.Update(company);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Insert(company);

            try
            {
                repository.Save();
            }
            catch (DbUpdateException)
            {
                if (CompanyExists(company.Company_symbol))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = company.Company_symbol }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(string id)
        {
            Company company = repository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            repository.Delete(company);
            repository.Save();

            return Ok(company);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private bool CompanyExists(string id)
        {
            return repository.GetById(id) != null ? true : false;
        }
    }
}