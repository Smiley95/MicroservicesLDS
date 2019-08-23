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
using LDSData.Models;
using LDSData.Repositories;
using MathNet.Numerics.Statistics;
using Newtonsoft.Json.Linq;

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
        // GET: api/Portfolios/GetPortfolio
        public IEnumerable<Portfolio> GetPortfolio()
        {
            return repository.GetAll();
        }


        // GET: api/Portfolios/GetPortfolio?id=5
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

        // GET: api/Portfolios/GetPortfolioReturn?id=5
        public IHttpActionResult GetPortfolioReturn(string id)
        {
            Portfolio portfolio = repository.GetById(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            else
            {
                JArray assetsReturns= new JArray();
                double PReturn=0,summAsset=0;
                using (var clientPortfolioReturn = new HttpClient())
                {
                    clientPortfolioReturn.BaseAddress = new Uri("https://localhost:44322");
                    foreach (Asset asset in portfolio.Asset)
                    {
                        summAsset = +asset.Asset_nbShare;
                        var PortfolioReturn = clientPortfolioReturn.GetAsync("/api/Returns/GetReturns?companySymbol=" + asset.Company_symbol).Result;
                        PortfolioReturn.EnsureSuccessStatusCode();
                        string AssetsReturnString = PortfolioReturn.Content.ReadAsStringAsync().Result;
                        assetsReturns.Add(AssetsReturnString);
                    }
                    for (int i =0; i< portfolio.Asset.Count();i++)
                    {
                        PReturn = PReturn + (Convert.ToDouble(assetsReturns[i]) * (Convert.ToDouble(portfolio.Asset.ElementAt(i).Asset_nbShare) / summAsset));
                    }

                }
                return Ok(PReturn);
            }
        }

        // GET: api/Portfolios/GetPortfolioMarketRisk?id=5
        public IHttpActionResult GetPortfolioMarketRisk(string id)
        {
            Portfolio portfolio = repository.GetById(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            else
            {
                JArray assetsBetaRisk = new JArray();
                double PBetaRisk = 0, summAsset = 0;
                using (var clientPortfolioBetaRisk = new HttpClient())
                {
                    clientPortfolioBetaRisk.BaseAddress = new Uri("https://localhost:44330");
                    foreach (Asset asset in portfolio.Asset)
                    {
                        summAsset = +asset.Asset_nbShare;
                        var assetBetaRisk = clientPortfolioBetaRisk.GetAsync("/api/Risks/GetRiskBeta?companySymbol=" + asset.Company_symbol).Result;
                        assetBetaRisk.EnsureSuccessStatusCode();
                        string assetBetaRiskString = assetBetaRisk.Content.ReadAsStringAsync().Result;
                        assetsBetaRisk.Add(assetBetaRiskString);
                    }
                    for (int i = 0; i < portfolio.Asset.Count(); i++)
                    {
                        PBetaRisk = PBetaRisk + (Convert.ToDouble(assetsBetaRisk[i]) * (Convert.ToDouble(portfolio.Asset.ElementAt(i).Asset_nbShare) / summAsset));
                    }

                }
                return Ok(PBetaRisk);
            }
        }
        // GET: api/Portfolios/GetPortfolioVarianceRisk?id=5
        public IHttpActionResult GetPortfolioVarianceRisk(string id)
        {
            Portfolio portfolio = repository.GetById(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            else
            {
                double summAsset = 0;
                double res = 0;
                List<List<double>> correlation = getCorrelation(portfolio);
                foreach (Asset asset in portfolio.Asset)
                {
                    summAsset = +asset.Asset_nbShare;
                }
                for (int i=0;i<portfolio.Asset.Count;i++) {
                    for (int j= 0; j < portfolio.Asset.Count; j++)
                    {
                        res =+(portfolio.Asset.ElementAt(i).Asset_nbShare / summAsset) * (portfolio.Asset.ElementAt(j).Asset_nbShare / summAsset)* HttpHelper.GetAssetVar(portfolio.Asset.ElementAt(i).Company_symbol)* HttpHelper.GetAssetVar(portfolio.Asset.ElementAt(j).Company_symbol)*correlation.ElementAt(i).ElementAt(j);
                    }
                }
                return Ok(Math.Sqrt(res));
            }
        }

        // GET: api/GetPortfoliosByInvestor
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Portfolio>))]
        public IHttpActionResult GetPortfoliosByInvestor([FromBody]string InvestorID)
        {
            return Ok(repository.GetAll().Where(c => c.Investor_ID.Equals(InvestorID)).Select(e => e));
        }

        // PUT: api/Portfolios/PutPortfolio?id=5
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

        // POST: api/Portfolios/PostPortfolio
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

        // DELETE: api/Portfolios/DeletePortfolio?id=5
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
        private List<List<double>> getCorrelation(Portfolio portfolio)
        {
            List<double> means = new List<double>();
            List<List<double>> pow2 = new List<List<double>>();
            List<List<double>> correlation = new List<List<double>>();
            List<List<double>> history = new List<List<double>>();
            List<List<double>> diff = new List<List<double>>();
            //calculate mean for each asset's price's history
            for (int i = 0;i < portfolio.Asset.Count();i++)
            {
                history.Add(HttpHelper.GetPriceHist(portfolio.Asset.ElementAt(i).Company_symbol));
                means.Add(HttpHelper.GetPriceHist(portfolio.Asset.ElementAt(i).Company_symbol).Mean());
            }
            //calculate each asset^2
            for (int i = 0; i < history.Count(); i++)
            {
                List<double> temp1 = new List<double>();
                List<double> temp2 = new List<double>();
                for (int j=0; j<history.ElementAt(i).Count(); j++)
                {
                    temp1.Add(history.ElementAt(i).ElementAt(j)-means.ElementAt(i));
                    temp2.Add(Math.Pow(history.ElementAt(i).ElementAt(j) - means.ElementAt(i), 2));
                }
                diff.Add(temp1);
                pow2.Add(temp2);
            }

            for (int i = 0; i < portfolio.Asset.Count(); i++)
            {
                List<double> temp = new List<double>();
                for (int j = 0; j < portfolio.Asset.Count(); j++)
                {
                    double nominator = 0;
                    double denominator = 0;
                    for (int ki = 0; ki < diff.ElementAt(j).Count(); ki++)
                    {
                        nominator = +diff.ElementAt(i).ElementAt(ki) * diff.ElementAt(j).ElementAt(ki);
                    }
                    denominator = Math.Sqrt(pow2.ElementAt(i).Sum() * pow2.ElementAt(j).Sum());
                    temp.Add(nominator/denominator);
                }
                correlation.Add(temp);
            }
            return correlation;
        }
    }
}