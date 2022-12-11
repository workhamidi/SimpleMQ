using Dto.Dto;
using Dto.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog;
using Utility.ConfigurationManager;

namespace RabbitMQPackageApi
{
    public class Communication
    {
        private readonly SimpleMQConfigurationManager _configuration;

        public Communication(SimpleMQConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        private ConnectionFactory _factory = new ConnectionFactory();

        private IConnection _connection = null!;

        private IModel _channel = null!;

        // template method pattern
        public ResultDto<IModel> CreateCommunication()
        {

            if (
                !(this.ConnectionFactory() &&
                  this.Connection() &&
                  this.Channel())
                )
            {

                Log.Error("Connection failed, check HostName or " +
                          "VirtualHost or UserName or Password" +
                          "or Broker service is down");

                return new ResultDto<IModel>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return new ResultDto<IModel>()
            {
                Data = _channel,
                StatusCode = ResultStatusCodeEnum.Success
            };

        }

        private bool ConnectionFactory()
        {
            
            try
            {
                _factory = new ConnectionFactory()
                {
                    HostName = _configuration.HostName,
                    VirtualHost = _configuration.VirtualHost,
                    UserName = _configuration.UserName,
                    Password = _configuration.Password
                };

            }
            catch (Exception err)
            {
                
                Log.Error("failed to create ConnectionFactory object with " +
                          "error : {0}", err);
                return false;
            }
            return true;

        }

        private bool Connection()
        {
            try
            {
                _connection = _factory.CreateConnection();
            }
            catch (Exception err)
            {
                Log.Error("failed to create CreateConnection object with " +
                          "error : {0}", err);

                return false;
            }

            return true;

        }

        private bool Channel()
        {
            try
            {
                _channel = _connection.CreateModel();
            }
            catch (Exception err)
            {
                Log.Error("failed to create CreateModel object with " +
                          "error : {0}", err);

                return false;
            }

            return true;
        }

        public void DisposingCommunication()
        {
            try
            {
                _channel.Close();
                _connection.Close();
            }
            catch (Exception err)
            {
                Log.Fatal("failed to close channel or connection" +
                             "error : {0}", err);
            }
        }

    }
}
