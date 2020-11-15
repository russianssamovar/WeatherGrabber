using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeatherGrabber.Api.Swagger;
using WeatherGrabber.Domain.Services;
using WeatherGrabber.Services.Weather;

namespace WeatherGrabber.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDatabaseFactory, DatabaseFactory>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICityAppService, CityAppService>();
            services.AddTransient<IWeatherAppService, WeatherAppService>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddControllers();
            services.AddApiVersioning()
                .AddVersionedApiExplorer(options => options.SubstituteApiVersionInUrl = true)
                .AddSwaggerGen()
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()); 
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.DisplayOperationId();
                    c.DisplayRequestDuration();
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    c.RoutePrefix = string.Empty;
                });
        }
    }
}