using MongoDB.Bson;

namespace WeatherGrabber.Domain.Entites
{
    public class Weather
    {
        public ObjectId Id { get; set; }
        public string CityId { get; set; }
        public string MaxTemp { get; set; }
        public string MinTemp { get; set; }
        public string Date { get; set; }
    }
}