using Microsoft.Extensions.DependencyInjection;
using RabbiMQHttpClientApi.Concrete;
using RabbiMQHttpClientApi.Interface;
using RabbiMQHttpClientApi.Validation;
using RabbitMQPackageApi;
using SimpleMQ;
using Utility.ConfigurationManager;
using Utility.Validation.HttpApi;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection DependencyInjectionHandler(this IServiceCollection services)
        {
            
            /* RabbiMQHttpClientApi */
            


            // services

            services.AddScoped(typeof(IRabbitMQHttpApi),
                typeof(ExchangeHttpApi));

            services.AddScoped(typeof(IRabbitMQHttpApi),
                typeof(QueueHttpApi));
            


            /* RabbitMQPackageApi */
            

            // services
            services.AddScoped(typeof(Communication));
            services.AddScoped(typeof(ExchangePackageApi));
            services.AddScoped(typeof(MessagePackageApi));
            services.AddScoped(typeof(QueuePackageApi));


            /*  ***  */

            services.AddSingleton(typeof(SimpleMQConfigurationManager));


            /*  manager  */

            services.AddScoped(typeof(Director));
            services.AddScoped(typeof(ExchangeManager));
            services.AddScoped(typeof(QueueManager));
            services.AddScoped(typeof(MessageManager));



            return services;
        }

    }
}