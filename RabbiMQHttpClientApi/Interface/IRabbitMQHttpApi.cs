using Dto.Dto;
using Dto.Dto.Exchange;

namespace RabbiMQHttpClientApi.Interface
{
    public interface IRabbitMQHttpApi
    {
        public ResultDto<List<T>> GetAll<T>();
        public ResultDto<T> Get<T>(string name);
    }
}
