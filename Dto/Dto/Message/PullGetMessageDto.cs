namespace Dto.Dto.Message
{
    public class PullGetMessageDto : GetMessageDto
    {
        /// <summary>
        /// number of message that have gevien it
        /// </summary>
        public int MessageNo { get; set; } = 1;
    }
}
