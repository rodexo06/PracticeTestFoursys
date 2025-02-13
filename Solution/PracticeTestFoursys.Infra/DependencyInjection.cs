﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using PracticeTestFoursys.Application.DependenciesInjections;
using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Infra.Context;
using PracticeTestFoursys.Infra.UOW;


namespace PracticeTestFoursys.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Scan(scan => scan.FromAssemblyOf<PositionContext>()
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());


            //services.Scan(scan =>
            //    scan.FromAssemblyOf<ResponseState>()
            //    .AddClasses(classes => classes.AssignableTo<IResponseState>())
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDependencies(Assembly.GetAssembly(typeof(DependencyInjection)));

            return services;
        }
    }
}
