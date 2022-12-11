namespace Dto.Dto.RabbitMQHttp
{
    public class RequestDto
    {
        public List<string> PathsList { get; set; } = new List<string>();
        
        /// <summary>
        /// default value  = HttpMethod.Get
        /// </summary>
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

        public StringQueryPaginationDto StringQueryPaginationDto { get; set; } = null!;

    }
}
