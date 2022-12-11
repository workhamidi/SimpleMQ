using Microsoft.Extensions.Configuration;
using Serilog;

namespace Utility.ConfigurationManager
{
    public class SimpleMQConfigurationManager
    {

        public readonly string HostName = null!;
        public readonly string VirtualHost = null!;
        public readonly string UserName = null!;
        public readonly string Password = null!;

        public SimpleMQConfigurationManager(IConfiguration config)
        {
            try
            {
                config = config.GetSection("RabbitMQConfiguration")
                    .GetSection("RabbitMQConnectionConfig");

                HostName = config.GetSection("HostName").Value!;
                VirtualHost = config.GetSection("VirtualHost").Value!;
                UserName = config.GetSection("UserName").Value!;
                Password = config.GetSection("Password").Value!;
            }
            catch (Exception err)
            {
                Log.Fatal("Configuration cannot reachable with error : {0}",
                    err);
            }
        }

    }
}
