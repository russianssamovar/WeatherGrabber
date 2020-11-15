using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Services;
using WeatherGrabber.Models;

namespace WeatherGrabber.Services.Weather
{
    public class CityAppService:ICityAppService
    {
        private readonly ICityService _cityService;

        public CityAppService(ICityService cityService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        public async Task<IEnumerable<CityModel>> GetCitiesAsync()
        {
            var cities = await _cityService.GetCitiesAsync();
            return cities.Select(c => new CityModel
            {
                Id = c.Id.ToString(),
                Name = c.Name
            }).ToArray();
        }
    }
}