using Dto.Dto;
using Dto.Dto.Bind;
using Dto.Dto.Queue;
using Dto.Enums;
using RabbiMQHttpClientApi.Interface;
using RabbitMQPackageApi;
using SimpleMQ.Validation;

namespace SimpleMQ
{
    public class QueueManager
    {
        private readonly QueuePackageApi _queuePackageApi;
        private readonly IRabbitMQHttpApi _queueHttpApi;

        public QueueManager(
            QueuePackageApi queuePackageApi,
            IRabbitMQHttpApi queueHttpApi
            )
        {
            _queuePackageApi = queuePackageApi;
            _queueHttpApi = queueHttpApi;
        }


        public ResultDto<CreateQueueDto> CreateQueue(CreateQueueDto queueProp)
        {

            if (queueProp.CreateQueueDtoIsValid() is false)
            {
                return new ResultDto<CreateQueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _queuePackageApi.CreateQueue(queueProp);
        }

        public ResultDto<RemoveQueueDto> RemoveQueue(RemoveQueueDto removeQueue)
        {
            if (removeQueue.DeleteQueueDtoIsValid() is false)
            {
                return new ResultDto<RemoveQueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _queuePackageApi.RemoveQueue(removeQueue);
        }

        public ResultDto<QueueDto> QueuePurge(QueueDto queue)
        {

            if (queue.QueueDtoIsValid() is false)
            {
                return new ResultDto<QueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _queuePackageApi.QueuePurge(queue);
        }


        public ResultDto<List<RabbitMQHttpApiQueueDto>> GetAllQueuesList()
        {
            return _queueHttpApi.GetAll<RabbitMQHttpApiQueueDto>();
        }

        public ResultDto<QueueDto> GetQueue(QueueDto queueDto)
        {
            
            if (queueDto.QueueDtoIsValid() is false)
            {
                return new ResultDto<QueueDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }
            
            return _queueHttpApi.Get<QueueDto>(queueDto.Name);
        }
        
        public ResultDto<BindQueueToExchangeDto> BindQueueToExchange(BindQueueToExchangeDto bindQueueToExchangeDto)
        {
            

            if (bindQueueToExchangeDto.BindQueueToExchangeDtoIsValid() is false)
            {
                return new ResultDto<BindQueueToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _queuePackageApi.BindQueueToExchange(bindQueueToExchangeDto);
        }
        

    }
}
