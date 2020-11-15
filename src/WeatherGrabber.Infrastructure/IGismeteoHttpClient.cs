using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Entites;
using WeatherGrabber.Models.Dtos.Weather;

namespace WeatherGrabber.Infrastructure
{
    public interface IGismeteoHttpClient
    {
        Task<City[]> GetCitiesAsync();
        Task<IEnumerable<WeatherDto>> GetWeatherForTenDaysAsync(string url);
    }
}