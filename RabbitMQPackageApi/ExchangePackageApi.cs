using Dto.Dto;
using Dto.Dto.Bind;
using Dto.Dto.Exchange;
using Dto.Enums;
using RabbiMQHttpClientApi.Concrete;
using RabbiMQHttpClientApi.Interface;
using Serilog;
using Utility.ConfigurationManager;

namespace RabbitMQPackageApi
{
    public class ExchangePackageApi : Communication
    {
        private readonly IRabbitMQHttpApi _rabbitMqHttpApi;

        public ExchangePackageApi(
            IRabbitMQHttpApi rabbitMQHttpApi,
            SimpleMQConfigurationManager configuration)
            : base(configuration)
        {
            _rabbitMqHttpApi = rabbitMQHttpApi;
        }

        public ResultDto<CreateExchangeDto> CreateExchange(CreateExchangeDto exchange)
        {
            
            if (_rabbitMqHttpApi.Get<ExchangeHttpApi>(exchange.Name).StatusCode ==
                ResultStatusCodeEnum.Success)
            {
                Log.Warning("the exchange already exists");

                return new ResultDto<CreateExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }


            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {
                channel.Data.ExchangeDeclare(
                    exchange.Name,
                    exchange.Type,
                    exchange.Durable,
                    exchange.AutoDelete,
                    exchange.Arguments
                        );

                base.DisposingCommunication();

                return new ResultDto<CreateExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                }; 

            }

            Log.Error("can not connect to broker");

            return new ResultDto<CreateExchangeDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }

        public ResultDto<DeleteExchangeDto> DeleteExchange(DeleteExchangeDto exchange)
        {

            // If the exchange.Name not exists

            if (_rabbitMqHttpApi.Get<ExchangeHttpApi>(exchange.Name).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {

                Log.Warning("the exchange.Name not exists");

                return new ResultDto<DeleteExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {
                channel.Data.ExchangeDelete(
                    exchange.Name,
                    exchange.IfEmpty
                    );

                base.DisposingCommunication();

                return new ResultDto<DeleteExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }

            Log.Error("can not connect to broker");


            return new ResultDto<DeleteExchangeDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }

        public ResultDto<BindExchangeToExchangeDto> BindExchangeToExchange(BindExchangeToExchangeDto bindExchangeToExchangeDto)
        {


            if (_rabbitMqHttpApi.Get<ExchangeHttpApi>(bindExchangeToExchangeDto.SourceExchangeName).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {
                Log.Warning("the source exchange not exists");

                return new ResultDto<BindExchangeToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            if (_rabbitMqHttpApi.Get<ExchangeHttpApi>(bindExchangeToExchangeDto.DestinationExchangeName).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {
                Log.Warning("the source exchange not exists");

                return new ResultDto<BindExchangeToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }


            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {

                channel.Data.ExchangeBind(
                    bindExchangeToExchangeDto.DestinationExchangeName,
                    bindExchangeToExchangeDto.SourceExchangeName,
                    bindExchangeToExchangeDto.RoutingKey,
                    bindExchangeToExchangeDto.Arguments);

                Log.Information("Bind process Exchange To Exchange was successfully");

                base.DisposingCommunication();

                return new ResultDto<BindExchangeToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }


            Log.Error("can not connect to broker");

            return new ResultDto<BindExchangeToExchangeDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };


        }


    }
}
