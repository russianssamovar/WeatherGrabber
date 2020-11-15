using MongoDB.Bson;

namespace WeatherGrabber.Domain.Entites
{
    public class City
    {
        
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}