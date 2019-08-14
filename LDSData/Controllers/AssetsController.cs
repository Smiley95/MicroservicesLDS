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
    public class AssetsController : ApiController
    {
        private IGenericRepository<Asset> repository = null;
        public AssetsController()
        {
            this.repository = new GenericRepository<Asset>();
        }

        public AssetsController(GenericRepository<Asset> repository)
        {
            this.repository = repository;
        }
        // GET: api/Assets
        public IEnumerable<Asset> GetAsset()
        {
            return repository.GetAll();
        }

        // GET: api/Assets/5
        [ResponseType(typeof(Asset))]
        public IHttpActionResult GetAsset(string id)
        {
            Asset asset = repository.GetById(id);
            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        // GET: api/GetAssetsByPortfolio
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Asset>))]
        public IHttpActionResult GetAssetsByPortfolio([FromBody]string PortfolioID)
        {
            return Ok(repository.GetAll().Where(c => c.PortfolioID.Equals(PortfolioID)).Select(e => e));
        }


        // PUT: api/Assets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsset(string id, Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset.Asset_ID)
            {
                return BadRequest();
            }

            repository.Update(asset);

            try
            {
                repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // POST: api/Assets
        [ResponseType(typeof(Asset))]
        public IHttpActionResult PostAsset(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Insert(asset);

            try
            {
                repository.Save();
            }
            catch (DbUpdateException)
            {
                if (AssetExists(asset.Asset_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = asset.Asset_ID }, asset);
        }

        // DELETE: api/Assets/5
        [ResponseType(typeof(Asset))]
        public IHttpActionResult DeleteAsset(string id)
        {
            Asset asset = repository.GetById(id);
            if (asset == null)
            {
                return NotFound();
            }

            repository.Delete(asset);
            repository.Save();

            return Ok(asset);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private bool AssetExists(string id)
        {
            return repository.GetById(id) != null ? true : false;
        }
    }
}