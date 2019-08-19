using System;
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
        /*public static async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44310");
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }*/

        public static string GetCompanyFinancialState()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://financialmodelingprep.com");
                var result =  client.GetAsync("/api/v3/company-key-metrics/AAPL").Result;
                result.EnsureSuccessStatusCode();
                string resultContentString = result.Content.ReadAsStringAsync().Result;

                if (isJson(resultContentString))
                {

                    //resultContentString = resultContentString.Replace("[", "\"[").Replace("]", "]\"").Replace("\"","'").Replace(" ", "");
                    
                    return resultContentString;
                    //return "json array ";
                }
                else {
                    return "this is not a json array";
                }
                //string resultContent = JsonConvert.DeserializeObject<string>(resultContentString);
                //return resultContentString;
            }
        }

        static bool isJson(string stringValue)
        {
            try
            {
                var json = JContainer.Parse(stringValue);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}