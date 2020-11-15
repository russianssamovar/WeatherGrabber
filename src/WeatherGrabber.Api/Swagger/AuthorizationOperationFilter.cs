using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherGrabber.Api.Swagger
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        private const string AccessToken = "AccessToken";
        
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) 
                operation.Parameters = new List<OpenApiParameter>();
            
            if (!context.ApiDescription.TryGetMethodInfo(out var methodInfo))
                return;
            
            var attributes = methodInfo.DeclaringType?.CustomAttributes.ToList();
            attributes?.AddRange(methodInfo.CustomAttributes);

            if (attributes != null && attributes.All(attr => attr.AttributeType != typeof(AuthorizeAttribute)))
                return;

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AccessToken,
                In = ParameterLocation.Header,
                Required = true
            });
        }
    }
}