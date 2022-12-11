using Dto.Dto;
using Dto.Dto.Bind;
using Dto.Dto.Director;
using Dto.Dto.Exchange;
using Dto.Dto.Queue;
using Dto.Enums;
using RabbitMQPackageApi;
using SimpleMQ.Validation;

namespace SimpleMQ
{
    public class Director
    {
        private readonly QueuePackageApi _queuePackageApi;
        private readonly ExchangePackageApi _exchangePackageApi;

        public Director(
            QueuePackageApi queuePackageApi,
            ExchangePackageApi exchangePackageApi
        )
        {
            _queuePackageApi = queuePackageApi;
            _exchangePackageApi = exchangePackageApi;
        }

        public ResultDto<DirectorDto> AllInOne(DirectorDto director)
        {

            if (director.DirectorDtoIsValid() is false)
            {
                return new ResultDto<DirectorDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            var creationQueueResult = _queuePackageApi.CreateQueue(new CreateQueueDto()
            {
                Name = director.QueueName
            });



            var resultNewExchange = _exchangePackageApi.CreateExchange(
                new CreateExchangeDto()
                {
                    Name = director.ExchangeName,
                    Type = director.ExchangeType
                });



            var bindResult = _queuePackageApi.BindQueueToExchange(new BindQueueToExchangeDto()
            {
                QueueName = director.QueueName,
                ExchangeName = director.ExchangeName,
                RoutingKey = director.BindRoutingKey
            });

            if (
                creationQueueResult.StatusCode == ResultStatusCodeEnum.Success &&
                resultNewExchange.StatusCode == ResultStatusCodeEnum.Success &&
                bindResult.StatusCode == ResultStatusCodeEnum.Success
                    )
            {
                return new ResultDto<DirectorDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Success
                };
            }

            return new ResultDto<DirectorDto>()
            {
                StatusCode = ResultStatusCodeEnum.Failed
            };

        }
    }
}
