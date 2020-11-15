using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Entites;
using WeatherGrabber.Models.Dtos.Weather;

namespace WeatherGrabber.Infrastructure
{
    public class GismeteoHttpClient : IGismeteoHttpClient
    {
        private readonly HttpClient _client;

        public GismeteoHttpClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<City[]> GetCitiesAsync()
        {
            var response = await SendGetAsync(string.Empty);
            return HtmlParser.ParseCities(response).ToArray();
        }

        public async Task<IEnumerable<WeatherDto>> GetWeatherForTenDaysAsync(string url)
        {
            var response = await SendGetAsync($"{url}10-days/");
            return HtmlParser.ParseWeather(response).ToArray();
        }
        
        private async Task<string> SendGetAsync(string url)
        {
            var response = await _client.SendAsync(CreateGetRequestMessage(url));
            return await response.Content.ReadAsStringAsync();
        }

        private HttpRequestMessage CreateGetRequestMessage(string url)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_client.BaseAddress + url),
                Method = HttpMethod.Get,
            };

            return request;
        }
    }
}