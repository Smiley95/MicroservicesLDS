using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Web;

namespace RiskService.Models
{
    public class HttpHelper
    {
        public static double GetBetaRisk(string companySymbol)
        {
            double BetaRisk;
            using (var clientBeta = new HttpClient())
            {
                clientBeta.BaseAddress = new Uri("https://financialmodelingprep.com");
                var resultBeta = clientBeta.GetAsync("/api/v3/company/profile/" + companySymbol).Result;
                resultBeta.EnsureSuccessStatusCode();
                string resultBetaString = resultBeta.Content.ReadAsStringAsync().Result;
                JObject resultBetaContent = JObject.Parse(resultBetaString);
                BetaRisk = Convert.ToDouble(resultBetaContent["profile"]["beta"]);
            }
            return BetaRisk;
        }
    }
}