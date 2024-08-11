using FluentValidation;
using FluentValidation.AspNetCore;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Infrastructure.Handlers;
using Intelligent.Solution.Infrastructure.MappingProfiles;
using Intelligent.Solution.Repository.Implementations;
using Intelligent.Solution.Repository.Interfaces;
using System.Reflection;

namespace Intelligent.Solution.API.Extensions
{
    /// <summary>
    /// Defines the <see cref="ServiceExtensions"/> class
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Defines the extension method for registering dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddMediatR(option =>
            {
                option.Lifetime = ServiceLifetime.Transient;
                option.RegisterServicesFromAssemblyContaining(typeof(ProductHandler));
                option.RequestExceptionActionProcessorStrategy = RequestExceptionActionProcessorStrategy.ApplyForAllExceptions;
            });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProductMap)));

            services.AddValidatorsFromAssemblyContaining(typeof(ProductAddRequest), ServiceLifetime.Scoped);

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
