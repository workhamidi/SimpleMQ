using System.Text;
using Dto.Dto;
using Dto.Dto.RabbitMQHttp;
using Dto.Enums;
using RabbiMQHttpClientApi.Validation;
using Utility.ConfigurationManager;
using Utility.UrlManager;
using Utility.Validation.HttpApi;

namespace RabbiMQHttpClientApi
{
    public class ApiHttpClient
    {
        private readonly SimpleMQConfigurationManager _configuration;

        public ApiHttpClient(SimpleMQConfigurationManager configuration)
        {
            _configuration = configuration;
        }

        public ResultDto<string> SendRequest(RequestDto httpRequest)
        {

            if (httpRequest.RequestDtoIsValid() is false)
            {
                return new ResultDto<string>()
                {
                    Description = "RequestDto not valid",
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }

            var request = new HttpRequestMessage(
                httpRequest.HttpMethod,
                UrlFactory.GetApiUrl(
                    httpRequest.PathsList,
                    httpRequest.StringQueryPaginationDto
                ));


            request.Headers.Add("Authorization",
                "Basic " +
                Convert.ToBase64String(ASCIIEncoding
                    .ASCII.GetBytes(
                        _configuration.UserName + ":" +
                        _configuration.Password
                        )));


            HttpClient httpClient = new HttpClient();


            HttpResponseMessage response = httpClient.SendAsync(request)
                .GetAwaiter().GetResult();


            if (response.HttpResponseIsValid() is false)
            {
                return new ResultDto<string>()
                {
                    Description = "RabbitMQ response not valid",
                    StatusCode = ResultStatusCodeEnum.Failed
                };
            }


            string responseBody = response.Content.ReadAsStringAsync()
                .GetAwaiter().GetResult();

            return new ResultDto<string>()
            {
                Data = responseBody,
                StatusCode = ResultStatusCodeEnum.Success
            };

        }
    }
}
