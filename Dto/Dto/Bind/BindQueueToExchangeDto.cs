namespace Dto.Dto.Bind
{
    public class BindQueueToExchangeDto
    {
        public string QueueName { get; set; } = null!;
        public string ExchangeName { get; set; } = null!;
        public string RoutingKey { get; set; } = null!;
        public IDictionary<string, object> Arguments { get; set; } = null!;

    }
}
