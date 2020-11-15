using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public interface IWeatherGrabberService
    {
        Task UpdateCitiesAsync(IEnumerable<City> cities);
        Task UpdateWeatherAsync(IEnumerable<Weather> weathers);
    }
}