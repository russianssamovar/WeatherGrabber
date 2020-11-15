using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGrabber.Models;

namespace WeatherGrabber.Services.Weather
{
    public interface IWeatherAppService
    {
        Task<IEnumerable<WeatherModel>> GetCityWeatherAsync(string cityId, int? limit = null);
    }
}