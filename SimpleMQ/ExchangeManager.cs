using Dto.Dto;
using Dto.Dto.Bind;
using Dto.Dto.Exchange;
using Dto.Enums;
using RabbiMQHttpClientApi.Interface;
using RabbitMQPackageApi;
using SimpleMQ.Validation;

namespace SimpleMQ
{
    public class ExchangeManager
    {
        private readonly ExchangePackageApi _exchangePackageApi;
        private readonly IRabbitMQHttpApi _exchangeHttpApi;

        public ExchangeManager(
            ExchangePackageApi exchangePackageApi,
            IRabbitMQHttpApi exchangeHttpApi
        )
        {
            _exchangePackageApi = exchangePackageApi;
            _exchangeHttpApi = exchangeHttpApi;
        }

        public ResultDto<List<RabbitMQHttpApiExchangeDto>> GetAllExchangeList()
        {
            return _exchangeHttpApi.GetAll<RabbitMQHttpApiExchangeDto>();
        }

        public ResultDto<ExchangeDto> GetExchange(ExchangeDto exchangeDto)
        {

            if (exchangeDto.ExchangeDtoIsValid() is false)
            {
                return new ResultDto<ExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _exchangeHttpApi.Get<ExchangeDto>(exchangeDto.Name);
        }


        public ResultDto<CreateExchangeDto> CreateExchange(CreateExchangeDto exchange)
        {

            if (exchange.CreateExchangeDtoIsValid() is false)
            {
                return new ResultDto<CreateExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _exchangePackageApi.CreateExchange(exchange);
        }


        public ResultDto<DeleteExchangeDto> DeleteExchange(DeleteExchangeDto exchange)
        {
            if (exchange.DeleteExchangeDtoIsValid() is false)
            {
                return new ResultDto<DeleteExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _exchangePackageApi.DeleteExchange(exchange);
        }


        public ResultDto<BindExchangeToExchangeDto> BindExchangeToExchange(BindExchangeToExchangeDto bindExchangeToExchangeDto)
        {
            if (bindExchangeToExchangeDto.BindExchangeToExchangeDtoIsValid() is false)
            {
                return new ResultDto<BindExchangeToExchangeDto>()
                {
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            return _exchangePackageApi.BindExchangeToExchange(bindExchangeToExchangeDto);
        }
    }
}
