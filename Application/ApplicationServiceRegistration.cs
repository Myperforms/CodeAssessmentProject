using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Reflection;
using MediatR;
namespace Application
{
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// The instance cache.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> InstanceCache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// The service collection.
        /// </summary>
        private static IServiceCollection serviceCollection;

        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The service location entity.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T">The specified type.</typeparam>
        /// <returns>Instance of the service.</returns>
        public static T GetService<T>()
        {
            var instance = serviceCollection.BuildServiceProvider().GetService<T>();
            if (instance != null)
            {
                return instance;
            }

            var type = typeof(T);
            if (type.IsClass)
            {
                instance = GetOptionInstance<T>();
            }

            return instance;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterServices(IServiceCollection services)
        {
          
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
          
            serviceCollection = services;
        }

        /// <summary>
        /// Gets the option instance.
        /// </summary>
        /// <typeparam name="T">The specified Type.</typeparam>
        /// <returns>The instance of T from IOptions</returns>
        private static T GetOptionInstance<T>()
        {
            var type = typeof(T);

            if (InstanceCache.ContainsKey(type))
            {
                return (T)InstanceCache[type];
            }

            var options = typeof(IOptions<>);
            var genericOptions = options.MakeGenericType(type);
            var optionInstance = serviceCollection.BuildServiceProvider().GetService(genericOptions);
            if (optionInstance != null)
            {
                var instance = (T)genericOptions.GetProperty("Value")?.GetValue(optionInstance);
                InstanceCache[type] = instance;
                return instance;
            }

            return default(T);
        }
    }
}
