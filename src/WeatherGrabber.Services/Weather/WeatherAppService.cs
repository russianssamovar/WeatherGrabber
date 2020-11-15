using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Services;
using WeatherGrabber.Models;

namespace WeatherGrabber.Services.Weather
{
    public class WeatherAppService : IWeatherAppService
    {
        private readonly IWeatherService _weatherService;

        public WeatherAppService(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        public async Task<IEnumerable<WeatherModel>> GetCityWeatherAsync(string cityId, int? limit = null)
        {
            var weathers = await _weatherService.GetCityWeatherAsync(cityId, limit);

            return weathers.Select(w => new WeatherModel
            {
                Date = w.Date,
                Id = w.Id.ToString(),
                CityId = w.CityId,
                MaxTemp = w.MaxTemp,
                MinTemp = w.MinTemp
            });
        }
    }
}