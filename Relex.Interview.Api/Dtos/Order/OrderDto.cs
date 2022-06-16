namespace Relex.Interview.Api.Dtos.Order
{
    public class OrderDto
    {
        public int NumberOfBatches { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string BatchCode { get; set; }
        public int BatchSize { get; set; }
    }
}
