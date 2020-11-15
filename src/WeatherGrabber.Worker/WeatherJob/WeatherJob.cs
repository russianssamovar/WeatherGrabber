using System;
using System.Threading.Tasks;
using Quartz;
using WeatherGrabber.Services.Weather;

namespace WeatherGrabber.Worker.WeatherJob
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WeatherJob : IJob
    {
        private readonly IWeatherGrabberAppService _weatherGrabberService;

        public WeatherJob(IWeatherGrabberAppService weatherGrabberService)
        {
            _weatherGrabberService =
                weatherGrabberService ?? throw new ArgumentNullException(nameof(weatherGrabberService));
        }

        public Task Execute(IJobExecutionContext context)
        {
            _weatherGrabberService.UpdateCitiesAsync();
            _weatherGrabberService.UpdateWeatherAsync();
            return Task.CompletedTask;
        }
    }
}