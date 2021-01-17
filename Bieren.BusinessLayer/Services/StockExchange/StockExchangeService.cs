using Bieren.BusinessLayer.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bieren.BusinessLayer.Services
{
    public class StockExchangeService : IStockExchangeService
    {
        private readonly string _baseUrl;
        private readonly string _financeAPIKey;
        private readonly IMyHttpService _httpService;
        public StockExchangeService(IMyHttpService httpService)
        {
            _httpService = httpService;
            // _baseUrl =
            // _financeAPIKey= 
        }
        public async Task<BO_Aandeel> GeefAandeelInfo(string aandeelTicker)
        {
            string url = "https://financialmodelingprep.com/api/v3/quote/ABI.BR?apikey=8e5b68b6bac6e3fe5c98c5781306f694";

            List<BO_Aandeel> aandelen = await GetAsync<List<BO_Aandeel>>(url);
            BO_Aandeel aandeel =null;
            if(aandelen !=null)
                aandeel = aandelen[0];
            return aandeel;
        }
        private async Task<T> GetAsync<T>(string url)
        {
            var jsonResponse = await _httpService.Get(url);
            //string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
