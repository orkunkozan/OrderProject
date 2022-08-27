namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Response
{
    public class GetOrderRes
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } 
        public string OrderAddress { get; set; } 
        public int ItemCount => OrderItems.Count;
        public decimal TotalAmount => OrderItems.Sum(s => s.TotalAmount);
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}
