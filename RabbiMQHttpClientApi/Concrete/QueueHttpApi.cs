using Dto.Dto;
using Dto.Dto.Exchange;
using Dto.Dto.Queue;
using Dto.Dto.RabbitMQHttp;
using Dto.Enums;
using Newtonsoft.Json;
using RabbiMQHttpClientApi.Interface;
using RabbiMQHttpClientApi.Validation;
using Serilog;
using Utility.ConfigurationManager;
using Utility.Validation.HttpApi;

namespace RabbiMQHttpClientApi.Concrete
{
    public class QueueHttpApi : IRabbitMQHttpApi
    {
        private readonly SimpleMQConfigurationManager _configuration;
        public QueueHttpApi(SimpleMQConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        public ResultDto<List<T>> GetAll<T>()
        {

            var param = new RequestDto()
            {
                PathsList = new List<string>() { "queues" },
                HttpMethod = HttpMethod.Get
            };

            var responseBody =
                new ApiHttpClient(_configuration).SendRequest(param);


            List<RabbitMQHttpApiQueueDto> queue = new List<RabbitMQHttpApiQueueDto>();



            if (responseBody.StatusCode == ResultStatusCodeEnum.Failed)
            {
                return (ResultDto<List<T>>)Convert.ChangeType(new ResultDto<List<RabbitMQHttpApiQueueDto>>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<List<T>>));
            }

            try
            {
                queue = JsonConvert
                    .DeserializeObject<List<RabbitMQHttpApiQueueDto>>
                        (responseBody.Data)!;
            }
            catch (Exception err)
            {
                Log.Error("cannot Deserializing responseBody.Data with error : {0}\n", err);

                return (ResultDto<List<T>>)Convert.ChangeType(new ResultDto<List<RabbitMQHttpApiQueueDto>>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<List<T>>));

            }
            
            return (ResultDto<List<T>>)Convert.ChangeType(new ResultDto<List<RabbitMQHttpApiQueueDto>>
            {
                Data = queue,
                StatusCode = ResultStatusCodeEnum.Success
            }, typeof(ResultDto<List<T>>));

        }

        public ResultDto<T> Get<T>(string queueName)
        {
            
            var param = new RequestDto()
            {
                PathsList = new List<string>() {
                    "queues",
                    _configuration.VirtualHost,
                    queueName },
                HttpMethod = HttpMethod.Get
            };

            var responseBody = 
                new ApiHttpClient(_configuration).SendRequest(param);


            RabbitMQHttpApiQueueDto queue = new RabbitMQHttpApiQueueDto();

            // این جا مشکل داریم 
            if (responseBody.StatusCode == ResultStatusCodeEnum.Failed)
            {
                // ????????????RabbitMQHttpApiQueueDto => T
                return (ResultDto<T>)Convert.ChangeType(new ResultDto<T>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<T>));
            }

            try
            {
                queue = JsonConvert.DeserializeObject<RabbitMQHttpApiQueueDto>
                (responseBody.Data)!;
            }
            catch (Exception err)
            {
               
                Log.Error("cannot Deserializing responseBody.Data with error : {0}\n", err);

                return (ResultDto<T>)Convert.ChangeType(new ResultDto<RabbitMQHttpApiQueueDto>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<T>));
            }


            // the Queue not existed
            if (queue?.name is null)
            {

                Log.Information("the Queue not existed");

                return (ResultDto<T>)Convert.ChangeType(new ResultDto<RabbitMQHttpApiQueueDto>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<T>));
            }


            return (ResultDto<T>)Convert.ChangeType(new ResultDto<T>
            {
                //Data = queue,
                StatusCode = ResultStatusCodeEnum.Success
            }, typeof(ResultDto<T>));
        }

    }
}


