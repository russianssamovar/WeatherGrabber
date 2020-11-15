using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<Weather>> GetCityWeatherAsync(string cityId, int? limit = null);
    }
}