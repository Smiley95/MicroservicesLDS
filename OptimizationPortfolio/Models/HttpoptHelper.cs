using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using MathNet.Numerics.Statistics;

namespace OptimizationPortfolio.Models
{
    public class HttpOptHelper
    {
        public static JArray getPortfolioAssets(string ID)
        {
            string tokenString;
            JObject tokenContent = new JObject();
            using (var tokenClient = new HttpClient())
            {
                var parameters = new Dictionary<string, string> { { "username", "smiley" }, { "password", "smile" }, { "grant_type", "password" } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                tokenClient.BaseAddress = new Uri("https://localhost:44334");
                var token = tokenClient.PostAsync("/token",encodedContent).Result;
                token.EnsureSuccessStatusCode();
                tokenString = token.Content.ReadAsStringAsync().Result;
                tokenContent = JObject.Parse(tokenString);
            }
            JObject portfolioContent = new JObject();
            using (var portfolioClient = new HttpClient())
            {
                portfolioClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToString(tokenContent["access_token"]));
                portfolioClient.BaseAddress = new Uri("https://localhost:44334");
                var portfolio = portfolioClient.GetAsync("/api/Portfolios/GetPortfolio?id=" + ID).Result;
                portfolio.EnsureSuccessStatusCode();
                string portfolioString = portfolio.Content.ReadAsStringAsync().Result;
                portfolioContent = JObject.Parse(portfolioString);
            }
            return JArray.Parse(portfolioContent["Asset"].ToString());
        }
         public static List<List<double>> getVarCovarMatrix(string id)
        {
            List<List<double>> varCovMat = new List<List<double>>();
            JArray assets = getPortfolioAssets(id);
            List<List<double>> histReturn = getExpReturn(id);
            for (int i =0; i < assets.Count(); i++)
            {
                List<double> temp = new List<double>();

                for (int j = 0; j < assets.Count(); j++)
                {
                    temp.Add(Statistics.Covariance(histReturn.ElementAt(i), histReturn.ElementAt(j)));
                }
                varCovMat.Add(temp);
            }
            return varCovMat;
        }
        public static List<List<double>> getPriceHist(string id)
        {
            List<List<double>> priceHist = new List<List<double>>();
            JArray historical;
            JArray assets = getPortfolioAssets(id);
            for (int i = 0; i < assets.Count(); i++)
            {
                List<double> temp = new List<double>();
                using (var priceHistClient = new HttpClient())
                {

                    priceHistClient.BaseAddress = new Uri("https://financialmodelingprep.com");
                    var priceHistResult = priceHistClient.GetAsync("/api/v3/historical-price-full/"+assets.ElementAt(i)["Company_symbol"] +"?from=2019").Result;
                    priceHistResult.EnsureSuccessStatusCode();
                    string priceHistString = priceHistResult.Content.ReadAsStringAsync().Result;
                    JObject priceHistContent = JObject.Parse(priceHistString);
                    historical = JArray.Parse(priceHistContent["historical"].ToString());
                }
                foreach (JObject h in historical)
                {
                    temp.Add(Convert.ToDouble(h["close"]));
                }
                priceHist.Add(temp);
            }
            return priceHist;
        }
        public static List<List<double>> getExpReturn(string id)
        {
            List<List<double>> expReturn = new List<List<double>>();
            //JArray historical;
            List<List<double>> hist = getPriceHist(id);
            for (int i = 0; i < hist.Count(); i++)
            {
                List<double> temp = new List<double>();
                for (int j = hist.ElementAt(i).Count()-1; j >0; j--)
                {
                    temp.Add((hist.ElementAt(i).ElementAt(j)- hist.ElementAt(i).ElementAt(j-1))/ hist.ElementAt(i).ElementAt(j - 1));
                }
                expReturn.Add(temp);
            }
            return expReturn;
        }

    }
}