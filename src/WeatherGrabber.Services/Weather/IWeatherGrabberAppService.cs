using System.Threading.Tasks;

namespace WeatherGrabber.Services.Weather
{
    public interface IWeatherGrabberAppService
    {
        Task UpdateCitiesAsync();
        Task UpdateWeatherAsync();
    }
}