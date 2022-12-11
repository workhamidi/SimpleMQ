namespace Dto.Dto.Bind
{
    public class BindExchangeToExchangeDto
    {
        public string SourceExchangeName { get; set; } = null!;
        public string DestinationExchangeName { get; set; } = null!;
        public string RoutingKey { get; set; } = null!;
        public IDictionary<string, object> Arguments { get; set; } = null!;

    }
}
