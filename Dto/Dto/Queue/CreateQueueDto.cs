namespace Dto.Dto.Queue
{
    public class CreateQueueDto : QueueDto
    {
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public IDictionary<string, object> Arguments { get; set; } = null!;
    }
}
