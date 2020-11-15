using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IDatabaseFactory _databaseFactory;

        public WeatherService(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        }

        public async Task<IEnumerable<Weather>> GetCityWeatherAsync(string cityId, int? limit = null)
        {
            var collection = _databaseFactory.GetWeatherCollection();

            return await collection.Find(w => w.CityId == cityId).Limit(limit).ToListAsync();
        }
    }
}