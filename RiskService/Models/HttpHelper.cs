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
        public static double GetStandardDeviationRisk(string companySymbol,int nbYears)
        {
            List<double> AROR= GetAnnualRateOfReturn(companySymbol);
            double avgROR = 0;
            for (int i =0; i < nbYears;i++)
            {
                avgROR = +AROR[i];
            }
            avgROR = avgROR / nbYears;
            double variance = 0;
            for (int i = 0; i < nbYears; i++)
            {
                variance = Math.Pow(variance + (AROR[i] - avgROR), 2);
            }
            variance = variance / nbYears;
            return Math.Sqrt(variance);
            //return GetAnnualRateOfReturn(companySymbol);
        }
        public static List<double> GetAnnualRateOfReturn(string companySymbol)
        {
            List<double> AnnualRateOfReturn = new List<double>();
            JArray divYield, AssetGrowth;
            using (var clientDivYield = new HttpClient())
            {
                clientDivYield.BaseAddress = new Uri("https://financialmodelingprep.com");
                var resultDivYield = clientDivYield.GetAsync("/api/v3/company-key-metrics/" + companySymbol).Result;
                resultDivYield.EnsureSuccessStatusCode();
                string resultDivYieldString = resultDivYield.Content.ReadAsStringAsync().Result;
                JObject resultDivYieldContent = JObject.Parse(resultDivYieldString);
                divYield = JArray.Parse(resultDivYieldContent["metrics"].ToString());

            }
            using (var clientGrowth = new HttpClient())
            {
                clientGrowth.BaseAddress = new Uri("https://financialmodelingprep.com");
                var resultGrowth = clientGrowth.GetAsync("/api/v3/financial-statement-growth/" + companySymbol).Result;
                resultGrowth.EnsureSuccessStatusCode();
                string resultGrowthString = resultGrowth.Content.ReadAsStringAsync().Result;
                JObject resultGrowthContent = JObject.Parse(resultGrowthString);
                AssetGrowth = JArray.Parse(resultGrowthContent["growth"].ToString());
            }
            for (int i = 0; i < (AssetGrowth.Count-1); i++)
            {
                AnnualRateOfReturn.Add(Convert.ToDouble(divYield[i]["Dividend Yield"]) + Convert.ToDouble(AssetGrowth[i]["Asset Growth"]));
            }
            return AnnualRateOfReturn;
        }
    }
}