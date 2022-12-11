namespace Dto.Dto.Queue
{
    public class RabbitMQHttpApiQueueDto
    {

        public IDictionary<string, object> arguments { get; set; }

        public bool auto_delete { get; set; }
        public string name { get; set; }
        public string node { get; set; }
        public string policy { get; set; }
        public string type { get; set; }
        public string vhost { get; set; }
        public int consumers { get; set; }
        public bool durable { get; set; }
    }
}
