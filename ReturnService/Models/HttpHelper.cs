﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using ReturnService.Models;
using System.Web;

namespace ReturnService.Models
{
    public class HttpHelper
    {
        public static double getROA(string companySymbol)
        {
            double netIncome, totalAsset;
            using (var clientIncome = new HttpClient())
            {
                clientIncome.BaseAddress = new Uri("https://financialmodelingprep.com");
                var resultIncome = clientIncome.GetAsync("/api/v3/financials/income-statement/"+ companySymbol).Result;
                resultIncome.EnsureSuccessStatusCode();
                string resultIncomeString = resultIncome.Content.ReadAsStringAsync().Result;
                JObject resultIncomeContent = JObject.Parse(resultIncomeString);
                netIncome = Convert.ToDouble(resultIncomeContent["financials"][0]["Net Income"]);
            }
            using (var clientAsset = new HttpClient())
            {
                clientAsset.BaseAddress = new Uri("https://financialmodelingprep.com");
                var resultAsset = clientAsset.GetAsync("api/v3/financials/balance-sheet-statement/"+ companySymbol).Result;
                resultAsset.EnsureSuccessStatusCode();
                string resultAssetString = resultAsset.Content.ReadAsStringAsync().Result;
                JObject resultAssetContent = JObject.Parse(resultAssetString);
                totalAsset = Convert.ToDouble(resultAssetContent["financials"][0]["Total assets"]);
            }
            return netIncome / totalAsset;
        }
    }
}