using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeAssessment.Test.Common
{
    public class ApplicationService
    {
        private readonly IServiceProvider serviceProvider;

        private Dictionary<Type, object> Services = new Dictionary<Type, object>();

        public ApplicationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
     
        public T GetService<T>()
        {
            if (!Services.ContainsKey(typeof(T)))
            {
                Services[typeof(T)] = this.serviceProvider.GetService<T>();

            }

            return (T)Services[typeof(T)];
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);
            //Do the service register here and extra stuff you want
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
                //etc
            });

        }
    }
}
