using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public class WeatherGrabberService : IWeatherGrabberService
    {
        private readonly IDatabaseFactory _databaseFactory;

        public WeatherGrabberService(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        }

        public async Task UpdateCitiesAsync(IEnumerable<City> cities)
        {
            var collection = _databaseFactory.GetCitiesCollection();
            foreach (var city in cities)
            {
                var existCity = await collection.FindAsync(c => c.Name == city.Name);
                if (await existCity.AnyAsync())
                {
                    continue;
                }

                await collection.InsertOneAsync(city);
            }
        }

        public async Task UpdateWeatherAsync(IEnumerable<Weather> weathers)
        {
            var weatherCollection = _databaseFactory.GetWeatherCollection();

            foreach (var weather in weathers)
            {
                var exist = await weatherCollection.FindAsync(w =>
                    w.CityId == weather.CityId && w.Date == weather.Date);
                if (await exist.AnyAsync())
                {
                    continue;
                }

                await weatherCollection.InsertOneAsync(weather);
            }
        }
    }
}