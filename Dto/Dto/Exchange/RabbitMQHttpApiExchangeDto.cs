namespace Dto.Dto.Exchange
{
    public class RabbitMQHttpApiExchangeDto
    {
        public IDictionary<string, object> arguments { get; set; }

        public string vhost { get; set; }
        public string user_who_performed_action { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public bool @internal { get; set; }
        public bool durable { get; set; }
        public bool auto_delete { get; set; }
        
        }
    }
