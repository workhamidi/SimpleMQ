namespace Dto.Dto.Message
{
    public class PublishMessageDto
    {

        public string ExchangeName { get; set; } = null!;
        public string RoutingKey { get; set; } = null!;

        public IDictionary<string,object> PropertiesHeaders { get; set; } = null!;

        public string Message { get; set; } = null!;

    }
}
