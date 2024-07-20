using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PracticeTestFoursys.Application.DependenciesInjections {
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions {
        /// <summary>
        /// Registra as interfaces e implementações para a injeção de dependência.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                services.AddDependencies(assembly);
            }
            return services;
        }

        /// <summary>
        /// Registra as interfaces e implementações para a injeção de dependência.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
        {
            services.AddDependencies<TransientLifetimeAttribute>(assembly, (service, allowMultipleInjection, serviceType, implementationType) =>
            {
                return AddTransient(service, allowMultipleInjection, serviceType, implementationType);
            });

            services.AddDependencies<ScopedLifetimeAttribute>(assembly, (service, allowMultipleInjection, serviceType, implementationType) =>
            {
                return AddScoped(service, allowMultipleInjection, serviceType, implementationType);
            });

            services.AddDependencies<SingletonLifetimeAttribute>(assembly, (service, allowMultipleInjection, serviceType, implementationType) =>
            {
                return AddSingleton(service, allowMultipleInjection, serviceType, implementationType);
            });

            return services;
        }

        private static IServiceCollection AddDependencies<T>(this IServiceCollection services, Assembly assembly, Func<IServiceCollection, bool, Type, Type, IServiceCollection> action) where T : Attribute
        {
            var implementations = assembly.GetTypes()
                .Where(type => type.GetCustomAttributes<T>().Any())
                .ToArray();

            foreach (var implementation in implementations)
            {
                var typeInterfaces = implementation.GetInterfaces()
                .Where(x => x.GetCustomAttributes<InjectableAttribute>().Any())
                .ToArray();

                var allowMultipleInjection = typeInterfaces
                .Any(x => x.GetCustomAttributes<AllowMultipleInjectionAttribute>().Any());

                foreach (var typeInterface in typeInterfaces)
                {
                    services = action.Invoke(services, allowMultipleInjection, typeInterface, implementation);
                }
            }

            return services;
        }

        private static IServiceCollection AddTransient(IServiceCollection services, bool allowMultipleInjection, Type serviceType, Type implementationType)
        {
            if (allowMultipleInjection)
            {
                services.AddTransient(serviceType, implementationType);
            }
            else
            {
                services.TryAddTransient(serviceType, implementationType);
            }
            return services;
        }

        private static IServiceCollection AddScoped(IServiceCollection services, bool allowMultipleInjection, Type serviceType, Type implementationType)
        {
            if (allowMultipleInjection)
            {
                services.AddScoped(serviceType, implementationType);
            }
            else
            {
                services.TryAddScoped(serviceType, implementationType);
            }
            return services;
        }

        private static IServiceCollection AddSingleton(IServiceCollection services, bool allowMultipleInjection, Type serviceType, Type implementationType)
        {
            if (allowMultipleInjection)
            {
                services.AddSingleton(serviceType, implementationType);
            }
            else
            {
                services.TryAddSingleton(serviceType, implementationType);
            }
            return services;
        }

    }
}
