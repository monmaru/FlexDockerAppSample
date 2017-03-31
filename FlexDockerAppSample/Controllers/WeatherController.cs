using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlexDockerAppSample.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private const string BaseUrl = "http://weather.livedoor.com/forecast/webservice/json/v1";

        [HttpGet("{cityCode}")]
        public async Task<Weather[]> Get(string cityCode)
        {
            return await GetWeatherAsync(cityCode);
        }

        public async Task<Weather[]> GetWeatherAsync(string cityCode)
        {
            var url = $"{BaseUrl}?city={cityCode}";
            var result = await HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            var jsonString = await result.Content.ReadAsStringAsync();
            var jsonObj = JObject.Parse(jsonString);
            return jsonObj["forecasts"].Select(x => new Weather
            {
                Date = DateTime.ParseExact(x["date"].ToString(), "yyyy-MM-dd", null), Telop = x["telop"].ToString()
            }).ToArray();
        }
    }

    public class Weather
    {
        public DateTime Date { get; set; }
        public string Telop { get; set; }
    }
}
