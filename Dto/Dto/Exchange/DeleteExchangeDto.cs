namespace Dto.Dto.Exchange
{
    public class DeleteExchangeDto : ExchangeDto
    {
        /// <summary>
        /// delete a queue only if it is empty
        /// </summary>
        public bool IfEmpty { get; set; } = false;
    }
}
