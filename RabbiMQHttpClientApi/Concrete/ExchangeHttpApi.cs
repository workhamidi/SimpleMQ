using Dto.Dto;
using Dto.Dto.Exchange;
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
    public class ExchangeHttpApi : IRabbitMQHttpApi
    {
        private readonly SimpleMQConfigurationManager _configuration;

        public ExchangeHttpApi(SimpleMQConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        public ResultDto<List<T>> GetAll<T>()
        {

            var param = new RequestDto()
            {
                PathsList = new List<string>() { "exchanges" },
                HttpMethod = HttpMethod.Get
            };

            var responseBody = 
                new ApiHttpClient(_configuration).SendRequest(param);


            List<RabbitMQHttpApiExchangeDto> exchange =
                new List<RabbitMQHttpApiExchangeDto>();

            if (responseBody.StatusCode == ResultStatusCodeEnum.Failed)
            {
                return (ResultDto<List<T>>)Convert.ChangeType(new ResultDto<List<RabbitMQHttpApiExchangeDto>>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<List<T>>));
            }

            try
            {
                exchange = JsonConvert
                    .DeserializeObject<List<RabbitMQHttpApiExchangeDto>>
                        (responseBody.Data)!;
            }
            catch (Exception err)
            {
                Log.Error("cannot Deserializing responseBody.Data with error : {0}\n", err);
            }
            
            return (ResultDto<List<T>>)Convert.ChangeType(new ResultDto<List<RabbitMQHttpApiExchangeDto>>
            {
                Data = exchange,
                StatusCode = ResultStatusCodeEnum.Success
            }, typeof(ResultDto<>));

        }

        public ResultDto<T> Get<T>(string exchangeName)
        {

            var param = new RequestDto()
            {
                PathsList = new List<string>() {
                    "exchanges",
                    _configuration.VirtualHost,
                    exchangeName },
                HttpMethod = HttpMethod.Get
            };

            var responseBody = 
                new ApiHttpClient(_configuration).SendRequest(param);

            

            RabbitMQHttpApiExchangeDto exchange = new RabbitMQHttpApiExchangeDto();


            if (responseBody.StatusCode == ResultStatusCodeEnum.Failed)
            {
                return (ResultDto<T>)Convert.ChangeType(new ResultDto<RabbitMQHttpApiExchangeDto>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<T>));
            }

            try
            {
                exchange = JsonConvert.DeserializeObject<RabbitMQHttpApiExchangeDto>
                    (responseBody.Data)!;
            }
            catch (Exception err)
            {
                Log.Error("cannot Deserializing responseBody.Data with error : {0}\n", err);

                return (ResultDto<T>)Convert.ChangeType(new ResultDto<RabbitMQHttpApiExchangeDto>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<T>));

            }


            // the exchange not existed
            if (exchange?.name is null)
            {
                Log.Information("the exchange not existed");


                return (ResultDto<T>)Convert.ChangeType(new ResultDto<RabbitMQHttpApiExchangeDto>
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                }, typeof(ResultDto<T>));
            }
            
            return (ResultDto<T>)Convert.ChangeType(new ResultDto<RabbitMQHttpApiExchangeDto>
            {
                Data = exchange,
                StatusCode = ResultStatusCodeEnum.Success
            }, typeof(ResultDto<T>));
        }

    }
}
