using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCitiesAsync();
    }
}