using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioManager.DBContext;
using PortfolioManager.Models;
using PortfolioManager.Repositories;

namespace PortfolioManager.Controllers
{
    [Route("api/Assets")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly DBPortfolioManager _context;
        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var assets = _assetRepository.GetAllAsset();
            return new OkObjectResult(assets);
        }

        /*[HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
        {
            var product = _assetRepository.GetProductByID(id);
            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }*/

    }
}