using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WeatherGrabber.Domain.Entites;

namespace WeatherGrabber.Domain.Services
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IMongoDatabase _database;

        public DatabaseFactory(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("Mongo")["ConnectionString"]);
            _database = client.GetDatabase(configuration.GetSection("Mongo")["DefaultDatabase"]);
        }
        
        public IMongoCollection<Weather> GetWeatherCollection()
        {
            return _database.GetCollection<Weather>("weather");
        }

        public IMongoCollection<City> GetCitiesCollection()
        {
            return _database.GetCollection<City>("cities");
        }
    }
}