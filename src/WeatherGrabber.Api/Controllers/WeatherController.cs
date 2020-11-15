using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WeatherGrabber.Models;
using WeatherGrabber.Services.Weather;

namespace WeatherGrabber.Api.Controllers
{
    /// <summary>
    /// Погода
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ApiController, ApiVersion("1.0")]
    public class WeatherController: Controller
    {
        private readonly IWeatherAppService _weatherAppService;

        public WeatherController(IWeatherAppService weatherAppService)
        {
            _weatherAppService = weatherAppService ?? throw new ArgumentNullException(nameof(weatherAppService));
        }
        /// <summary>
        /// Получение погоды по Id 
        /// </summary>
        [HttpGet]
        [Route("cities/{cityId}/weather")]
        public async Task<IEnumerable<WeatherModel>> GetWeather(string cityId, int? limit = null)
        {
            return await _weatherAppService.GetCityWeatherAsync(cityId, limit);
        }
    }
}