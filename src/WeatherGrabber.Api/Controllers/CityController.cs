using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherGrabber.Models;
using WeatherGrabber.Services.Weather;


namespace WeatherGrabber.Api.Controllers
{
    /// <summary>
    /// Города
    /// </summary>
    [Route("api/v{version:apiVersion}")]
    [ApiController, ApiVersion("1.0")]
    public class CityController : ControllerBase
    {
        private readonly ICityAppService _cityAppService;

        public CityController(ICityAppService cityAppService)
        {
            _cityAppService = cityAppService ?? throw new ArgumentNullException(nameof(cityAppService));
        }

        /// <summary>
        /// Получение городов 
        /// </summary>
        [HttpGet]
        [Route("cities")]
        public async Task<IEnumerable<CityModel>> GetCities()
        {
            return await _cityAppService.GetCitiesAsync();
        }
    }
}