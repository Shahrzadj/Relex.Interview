namespace Relex.Interview.Api.Dtos.Order
{
    public class CreateOrderDto
    {
        public int NumberOfBatches { get; set; }
        public int ProductId { get; set; }
        public bool IsBatchMaxSize { get; set; }
    }
}
