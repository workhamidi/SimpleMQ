namespace Dto.Dto.Queue
{
    public class RemoveQueueDto : QueueDto
    {

        /// <summary> 
        /// if it is not used (does not have any consumers)
        /// </summary>
        public bool IfUnused { get; set; } = false;


        /// <summary>
        /// delete a queue only if it is empty
        /// </summary>
        public bool IfEmpty { get; set; } = false;
    }
}
