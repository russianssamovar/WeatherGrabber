using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGrabber.Models;

namespace WeatherGrabber.Services.Weather
{
    public interface ICityAppService
    {
        Task<IEnumerable<CityModel>> GetCitiesAsync();
    }
}