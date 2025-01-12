using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CodeAssessment.Infrastructure;
using Application;
using CodeAssessment.Infrastructure.Context;


namespace CodeAssessment.Test.Common
{
    public class ServiceLocationSetup
    {
        private IConfigurationRoot Configuration;

        public string ConnectionString { get; private set; }

        public ApplicationService Services { get; private set; }

        public ServiceLocationSetup()
        {
            this.Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddAutoMapper(s => s.AddProfile(new CodeAssessment.Application.AutoMapper.AutoMapperProfile()));
            RegisterServices(services);
            services.AddApplicationServices();
            ApplicationServiceRegistration.RegisterServices(services);

            this.ConnectionString = this.Configuration.GetConnectionString("CodeAssessmentConnection");
            services.AddDbContext<CodeAssessment.Infrastructure.Context.CodeAssessmentDBContext>(options =>
            {
                options.UseSqlServer(this.ConnectionString).EnableSensitiveDataLogging();
            });        

            var serviceProvider = services.BuildServiceProvider();
            this.Services = new ApplicationService(serviceProvider);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        public T GetService<T>()
        {
            return this.Services.GetService<T>();
        }
    }
}
