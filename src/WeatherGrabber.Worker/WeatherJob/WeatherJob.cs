using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using WeatherGrabber.Services.Weather;

namespace WeatherGrabber.Worker.WeatherJob
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WeatherJob : IJob
    {
        private readonly IWeatherGrabberAppService _weatherGrabberService;
        private readonly ILogger<WeatherJob> _logger;

        public WeatherJob(IWeatherGrabberAppService weatherGrabberService, ILogger<WeatherJob> logger)
        {
            _weatherGrabberService =
                weatherGrabberService ?? throw new ArgumentNullException(nameof(weatherGrabberService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                _weatherGrabberService.UpdateCitiesAsync();
                _weatherGrabberService.UpdateWeatherAsync();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.CompletedTask;
            }
        }
    }
}