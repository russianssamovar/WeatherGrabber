using MongoDB.Driver;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public interface IDatabaseFactory
    {
        IMongoCollection<Weather> GetWeatherCollection();
        IMongoCollection<City> GetCitiesCollection();

    }
}