using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace LDSData.Models
{
    public class HttpHelper
    {
        public static double GetAssetVar(string companySymbol)
        {
            using (var clientAssetRisk = new HttpClient())
            {
                clientAssetRisk.BaseAddress = new Uri("https://localhost:44330");
                var AssetRisk = clientAssetRisk.GetAsync("/api/Risks/GetMinRiskStandDev?companySymbol=" + companySymbol).Result;
                AssetRisk.EnsureSuccessStatusCode();
                return AssetRisk.Content.ReadAsAsync<double>().Result;
            }
        }
        public static List<double> GetPriceHist(string companySymbol)
        {
            List<double> histPriceList = new List<double>();
            JArray priceHist = new JArray();
            using (var clientPriceHist = new HttpClient())
            {
                clientPriceHist.BaseAddress = new Uri("https://financialmodelingprep.com");
                var PriceHist = clientPriceHist.GetAsync("/api/v3/enterprise-value/" + companySymbol).Result;
                PriceHist.EnsureSuccessStatusCode();
                string resultPriceHistString = PriceHist.Content.ReadAsStringAsync().Result;
                JObject resultPriceHistContent = JObject.Parse(resultPriceHistString);
                priceHist = JArray.Parse(resultPriceHistContent["enterpriseValues"].ToString());
                for (int i = 0; i < (priceHist.Count); i++)
                {
                    histPriceList.Add(Convert.ToDouble(priceHist[i]["Stock Price"]));
                }
                return histPriceList;
            }
        }
    }
}