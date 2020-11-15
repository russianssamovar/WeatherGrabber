using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherGrabber.Api.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerDefaultValues>();
            options.OperationFilter<AuthorizationOperationFilter>();
            
            var xmlDocsPath = GetXmlDocsPath();

            foreach (var docPath in xmlDocsPath)
            {
                options.IncludeXmlComments(docPath, includeControllerXmlComments: true);
            }

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Title = $"Dnevnik.Sport.Api API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                    });
            }
        }

        private static IEnumerable<string> GetXmlDocsPath()
        {
            var currentAssembly = typeof(Startup).Assembly;

            return currentAssembly
                .GetReferencedAssemblies()
                .Union(new[] {currentAssembly.GetName()})
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(File.Exists)
                .ToArray();
        }
    }
}