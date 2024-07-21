using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Diagnostics.CodeAnalysis;

namespace PracticeTestFoursys.Application
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// Adiciona a autenticação Bearer.
        /// </summary>
        /// <param name="swaggerGenOptions"></param>
        /// <returns></returns>
        public static SwaggerGenOptions AddBearerSecurityDefinition(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Bearer Token.",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                    }
                }, Array.Empty<string>()
                }
            });
            return swaggerGenOptions;
        }
    }
}
