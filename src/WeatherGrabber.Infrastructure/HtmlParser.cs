using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WeatherGrabber.Domain.Entites;
using WeatherGrabber.Models.Dtos.Weather;

namespace WeatherGrabber.Infrastructure
{
    public static class HtmlParser
    {
        private const string HrefAttribute = "href";
        private const string NameAttribute = "data-name";
        private const string XPathCitiesExpression = "//noscript[@id='noscript']";
        private const string XPathMaxTempExpression = "//div[@class='maxt']/span[@class='unit unit_temperature_c']";
        private const string XPathMinTempExpression = "//div[@class='mint']/span[@class='unit unit_temperature_c']";
        private const string XPathDateExpression = "//div[@class='widget__row widget__row_date']/div/div[@class='w_date']/a/span";
        
        private const string MinusChar = "&minus;";
        public static IEnumerable<City> ParseCities(string html)
        {
            var htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);
            var nodes = htmlSnippet.DocumentNode.SelectNodes(XPathCitiesExpression);
            foreach (var node in nodes.Nodes())
            {
                var link = node.GetAttributeValue(HrefAttribute, string.Empty);
                var name = node.GetAttributeValue(NameAttribute, string.Empty);
                if (string.IsNullOrEmpty(link) || string.IsNullOrEmpty(name))
                {
                    continue;
                }
                yield return new City
                {
                    Link = $"{link}",
                    Name = name
                };
            }
        }
        
        public static IEnumerable<WeatherDto> ParseWeather(string html)
        {
            var htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);
            var maxTempNodes = htmlSnippet.DocumentNode.SelectNodes(XPathMaxTempExpression);
            var minTempNodes = htmlSnippet.DocumentNode.SelectNodes(XPathMinTempExpression);
            var dateNodes = htmlSnippet.DocumentNode.SelectNodes(XPathDateExpression);

            for (var i = 0; i < maxTempNodes.Count; i++)
            {
                if (minTempNodes.ElementAtOrDefault(i) == null || maxTempNodes.ElementAtOrDefault(i) == null || dateNodes.ElementAtOrDefault(i) == null)
                {
                    continue;
                }

                var maxTemp = maxTempNodes[i].InnerText.Replace(MinusChar, "-").Trim();
                var minTemp = minTempNodes[i].InnerText.Replace(MinusChar, "-").Trim();
                var date = Regex.Match(dateNodes[i].InnerText, @"\d+").Value;
                yield return new WeatherDto
                {
                    MaxTemp = maxTemp,
                    MinTemp = minTemp,
                    Date = date
                };
            }
        }
    }
}