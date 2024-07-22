using Microsoft.Extensions.DependencyInjection;
using PracticeTestFoursys.Application.DependenciesInjections;
using PracticeTestFoursys.Application.Mediator;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PracticeTestFoursys.Application
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {


            var assembly = Assembly.GetAssembly(typeof(DependencyInjection));
            services.AddDependencies(assembly)
                .AddAutoMapper(assembly)
                .AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));

            return services;
        }
    }
}
