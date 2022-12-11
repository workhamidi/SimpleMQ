namespace Dto.Dto.Message
{
    public class GetMessageDto
    {
        public string QueueName { get; set; } = null!;
        public bool AutoAck { get; set; } = true;
    }
}
