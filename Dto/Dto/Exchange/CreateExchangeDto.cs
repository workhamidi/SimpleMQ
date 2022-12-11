namespace Dto.Dto.Exchange
{
    public class CreateExchangeDto : ExchangeDto
    {
        public string Type { get; set; } = null!;

        public bool Durable { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public IDictionary<string, object> Arguments { get; set; } = null!;

    }
}
