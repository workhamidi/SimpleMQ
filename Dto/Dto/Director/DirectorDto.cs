namespace Dto.Dto.Director
{
    public class DirectorDto
    {
        public string QueueName { get; set; } = null!;
        public bool QueueDurable { get; set; } = true;
        public bool QueueExclusive { get; set; } = false;
        public bool QueueAutoDelete { get; set; } = false;
        public IDictionary<string, object> QueueArguments { get; set; } = null!;

        
        public string ExchangeName { get; set; } = null!;
        public string ExchangeType { get; set; } = null!;
        public IDictionary<string, object> ExchangeArguments { get; set; } = null!;



        public string BindRoutingKey { get; set; } = null!;
        public IDictionary<string, object> BindArguments { get; set; } = null!;

    }
}
