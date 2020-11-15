using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public class CityService : ICityService
    {
        private readonly IDatabaseFactory _databaseFactory;

        public CityService(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            var collection = _databaseFactory.GetCitiesCollection();
            return await collection.Find(_ => true).ToListAsync();
        }
    }
}