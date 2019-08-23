using Newtonsoft.Json.Linq;
using OptimizationPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OptimizationPortfolio.Controllers
{
    public class OptimizationController : ApiController
    {
        // GET: api/Optimization
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public IHttpActionResult GetPortfolio(string id)
        {
            List<List<double>> claimTerms = HttpOptHelper.getVarCovarMatrix(id);
            return Ok(claimTerms);
        }

        // GET: api/Optimization/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Optimization
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Optimization/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Optimization/5
        public void Delete(int id)
        {
        }
    }
}
