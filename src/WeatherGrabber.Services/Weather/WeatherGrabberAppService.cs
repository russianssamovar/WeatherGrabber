using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Services;
using WeatherGrabber.Infrastructure;

namespace WeatherGrabber.Services.Weather
{
    public class WeatherGrabberAppService : IWeatherGrabberAppService
    {
        private readonly IGismeteoHttpClient _httpClient;
        private readonly IWeatherGrabberService _weatherGrabberService;
        private readonly ICityService _cityService;

        public WeatherGrabberAppService(
            IGismeteoHttpClient httpClient, IWeatherGrabberService weatherGrabberService, ICityService cityService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _weatherGrabberService =
                weatherGrabberService ?? throw new ArgumentNullException(nameof(weatherGrabberService));
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        public async Task UpdateCitiesAsync()
        {
            var cities = await _httpClient.GetCitiesAsync();
            await _weatherGrabberService.UpdateCitiesAsync(cities.ToList());
        }

        public async Task UpdateWeatherAsync()
        {
            var cities = await _cityService.GetCitiesAsync();
            Parallel.ForEach(cities, c =>
            {
                var weathers = _httpClient.GetWeatherForTenDaysAsync(c.Link);
                _weatherGrabberService.UpdateWeatherAsync(weathers.Result.Select(w => new Domain.Entites.Weather
                {
                    CityId = c.Id.ToString(),
                    MaxTemp = w.MaxTemp,
                    MinTemp = w.MinTemp,
                    Date = GetCorrectDate(w.Date)
                }));
            });
        }

        private static string GetCorrectDate(string date)
        {
            var now = DateTime.UtcNow;
            if (int.Parse(date) < now.Day && now.Month == 12)
            {
                return $"{date}-{now.AddMonths(1).Month}-{now.AddYears(1).Year}";
            }

            if (int.Parse(date) < now.Day && now.Month > 1)
            {
                return $"{date}-{now.AddMonths(1).Month}-{now.Year}";
            }

            return $"{date}-{now.Month}-{now.Year}";
        }
    }
}