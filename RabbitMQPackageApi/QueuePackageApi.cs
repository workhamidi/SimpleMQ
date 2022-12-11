using Dto.Dto;
using Dto.Dto.Bind;
using Dto.Dto.Queue;
using Dto.Enums;
using RabbiMQHttpClientApi.Concrete;
using RabbiMQHttpClientApi.Interface;
using Serilog;
using Utility.ConfigurationManager;

namespace RabbitMQPackageApi
{
    public class QueuePackageApi : Communication
    {
        private readonly IRabbitMQHttpApi _rabbitMqHttpApi;

        public QueuePackageApi(
            IRabbitMQHttpApi rabbitMQHttpApi,
            SimpleMQConfigurationManager configuration)
            : base(configuration)
        {
            _rabbitMqHttpApi = rabbitMQHttpApi;
        }


        public ResultDto<CreateQueueDto> CreateQueue(CreateQueueDto queue)
        {

            if (_rabbitMqHttpApi.Get<QueueHttpApi>(queue.Name).StatusCode ==
                ResultStatusCodeEnum.Success)
            {
                Log.Warning("the queue already exists");

                return new ResultDto<CreateQueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            

            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {
                channel.Data.QueueDeclare(
                    queue: queue.Name,
                    durable: queue.Durable,
                    exclusive: queue.Exclusive,
                    autoDelete: queue.AutoDelete,
                    arguments: queue.Arguments);

                

                base.DisposingCommunication();

                return new ResultDto<CreateQueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }

            Log.Error("can not connect to broker");

            return new ResultDto<CreateQueueDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }

        public ResultDto<RemoveQueueDto> RemoveQueue(RemoveQueueDto removeQueue)
        {

            if (_rabbitMqHttpApi.Get<QueueHttpApi>(removeQueue.Name).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {
                Log.Warning("the queue.Name not exists");

                return new ResultDto<RemoveQueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {
                channel.Data.QueueDelete(
                    queue: removeQueue.Name,
                    removeQueue.IfUnused,
                    removeQueue.IfEmpty
                    );

                base.DisposingCommunication();

                return new ResultDto<RemoveQueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }

            Log.Error("can not connect to broker");

            return new ResultDto<RemoveQueueDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }

        public ResultDto<QueueDto> QueuePurge(QueueDto queue)
        {

            if (_rabbitMqHttpApi.Get<QueueHttpApi>(queue.Name).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {
                Log.Warning("the queue.Name not exists");

                return new ResultDto<QueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {

                channel.Data.QueuePurge(
                    queue: queue.Name
                );

                Log.Information("Purging the Queue was successfully  ");

                base.DisposingCommunication();

                return new ResultDto<QueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }


            Log.Error("can not connect to broker");

            return new ResultDto<QueueDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };


        }

        public ResultDto<BindQueueToExchangeDto> BindQueueToExchange(BindQueueToExchangeDto bindDto)
        {

            if (_rabbitMqHttpApi.Get<QueueHttpApi>(bindDto.QueueName).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {
                Log.Warning("the queue.Name not exists");

                return new ResultDto<BindQueueToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            if (_rabbitMqHttpApi.Get<ExchangeHttpApi>(bindDto.ExchangeName).StatusCode ==
                ResultStatusCodeEnum.Failed)
            {
                Log.Warning("the bindDto.ExchangeName not exists");

                return new ResultDto<BindQueueToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }


            var channel = base.CreateCommunication();

            if (channel.StatusCode == ResultStatusCodeEnum.Success)
            {

                channel.Data.QueueBind(
                     bindDto.QueueName,
                     bindDto.ExchangeName,
                     bindDto.RoutingKey,
                     bindDto.Arguments
                );

                Log.Information("Bind process Queue To Exchange was successfully");

                base.DisposingCommunication();

                return new ResultDto<BindQueueToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };

            }


            Log.Error("can not connect to broker");

            return new ResultDto<BindQueueToExchangeDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };


        }
        

    }
}
