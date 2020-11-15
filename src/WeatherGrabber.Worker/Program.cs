using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using WeatherGrabber.Domain.Services;
using WeatherGrabber.Infrastructure;
using WeatherGrabber.Services.Weather;

namespace WeatherGrabber.Worker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var jobKey = new JobKey("weatherJob");
                    services.AddQuartz(q =>
                    {
                        q.SchedulerId = "JobScheduler";
                        q.SchedulerName = "Job Scheduler";
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();
                        q.AddJob<WeatherJob.WeatherJob>(j => j.WithIdentity(jobKey));
                        q.AddTrigger(t => t
                            .WithIdentity("weatherJobTriger")
                            .ForJob(jobKey)
                            .StartNow().WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever()));
                    });
                    services.AddHttpClient<IGismeteoHttpClient, GismeteoHttpClient>(client =>
                    {
                        client.BaseAddress = new Uri(hostContext.Configuration.GetSection("gismeteo")["url"]);
                        client.Timeout = TimeSpan.FromSeconds(30);
                    });
                    services.AddTransient<ICityService, CityService>();
                    services.AddTransient<IWeatherGrabberService, WeatherGrabberService>();
                    services.AddTransient<IWeatherGrabberAppService, WeatherGrabberAppService>();
                    services.AddTransient<IDatabaseFactory, DatabaseFactory>();
                    services.AddQuartzServer(options => { options.WaitForJobsToComplete = true; });
                });
    }
}