using System.Collections.Generic;
using WeatherGrabber.Domain.Entites;
using WeatherGrabber.Models.Dtos.Weather;

namespace WeatherGrabber.Infrastructure
{
    public interface IHtmlParser
    {
        IEnumerable<City> ParseCities(string html);
        IEnumerable<WeatherDto> ParseWeather(string html);
    }
}

