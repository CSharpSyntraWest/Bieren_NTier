using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bieren.BusinessLayer.Services
{
    public class MyHttpService : IMyHttpService
    {
        private readonly ILogger _logger;

        private IHttpClientFactory _httpFactory { get; set; }
        public MyHttpService(ILogger<MyHttpService> logger,
            IHttpClientFactory httpFactory)
        {
            _logger = logger;

            _httpFactory = httpFactory;
        }

        public async Task<string> Get(string url)
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            //url = "https://financialmodelingprep.com/api/v3/quote/ABI.BR?apikey=8e5b68b6bac6e3fe5c98c5781306f694";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _httpFactory.CreateClient("stockMarket");

            var response = await client.SendAsync(request);

          

            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
    }
}
